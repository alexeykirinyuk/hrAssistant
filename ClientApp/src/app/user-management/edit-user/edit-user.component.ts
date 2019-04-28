import { HttpClient } from "@angular/common/http";
import { Inject, OnInit, Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { User } from "../models/user";

@Component({
    selector: 'app-edit-user',
    templateUrl: './edit-user.component.html'
})
export class EditUserComponent implements OnInit {
    public user: User;

    constructor(
        private _http: HttpClient,
        @Inject('BASE_URL') private _baseUrl: string,
        private _route: ActivatedRoute,
        private _router: Router) {
    }

    async ngOnInit(): Promise<void> {
        this._route.paramMap.subscribe(async paramsMap => {
            this.user = (await this._http.get<GetUserResult>(`${this._baseUrl}/api/user/${paramsMap.get("id")}`).toPromise())
                .user;
        });
    }

    public async update(): Promise<void> {
        await this._http.put(`${this._baseUrl}/api/user`, { user: this.user } as UpdateUser).toPromise();
        await this._router.navigate(["users"]);
    }
}

interface GetUserResult {
    user: User;
}

interface UpdateUser {
    user: User;
}