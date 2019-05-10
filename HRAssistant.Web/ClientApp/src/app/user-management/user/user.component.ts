import { HttpClient } from "@angular/common/http";
import { Inject, OnInit, Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { User } from "./user";

@Component({
    selector: 'app-user',
    templateUrl: './user.component.html',
    styleUrls: ["/user.component.scss"]
})
export class UserComponent implements OnInit {
    public user: User;
    private isEditing: boolean;

    constructor(
        private _http: HttpClient,
        @Inject('BASE_URL') private _baseUrl: string,
        private _route: ActivatedRoute,
        private _router: Router) {
    }

    async ngOnInit(): Promise<void> {
        this._route.paramMap.subscribe(async paramsMap => {
            var userId = paramsMap.get("id");
            if (userId != null) {
                this.user = (await this._http.get<GetUserResult>(`${this._baseUrl}/api/user/${userId}`).toPromise())
                    .user;
                this.isEditing = true;
            } else {
                this.user = { role: "HR", isBlocked: false } as User;
                this.isEditing = false;
            }
        });
    }

    public get buttonText(): string {
        return this.isEditing ? "Обновить" : "Создать";
    }

    public get title(): string {
        return this.isEditing ? this.user.firstName + " " + this.user.lastName : "Новый сотрудник";
    }

    public async submit(): Promise<void> {
        if (this.isEditing) {
            await this._http.put(`${this._baseUrl}/api/user`, { user: this.user } as UpdateUser).toPromise();
        } else {
            await this._http.post(`${this._baseUrl}/api/user`, { user: this.user } as AddUser).toPromise();
        }
        await this._router.navigate(["users"]);
    }

    public async cancel(): Promise<void> {
        await this._router.navigate(["users"]);
    }
}

interface GetUserResult {
    user: User;
}

interface UpdateUser {
    user: User;
}

interface AddUser {
    user: User;
}