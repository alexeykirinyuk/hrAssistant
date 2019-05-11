import { HttpClient } from "@angular/common/http";
import { Inject, OnInit, Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { SearchUserItem } from 'src/app/user-management/users/SearchUserItem';
import { City } from 'src/app/city-management/City';
import { SearchResults } from 'src/app/libs/search-results';

@Component({
    selector: 'app-team',
    templateUrl: './team.component.html',
    styleUrls: ["/team.component.scss"]
})
export class TeamComponent implements OnInit {
    public model: Team;
    public users: SearchUserItem[];
    public cities: City[];

    private isEditing: boolean;
    private loaded = false;

    constructor(
        private _http: HttpClient,
        @Inject('BASE_URL') private _baseUrl: string,
        private _route: ActivatedRoute,
        private _router: Router) {
    }

    async ngOnInit(): Promise<void> {
        this._route.paramMap.subscribe(async paramsMap => {
            var id = paramsMap.get("id");
            let getUsersPromise = this._http.get<SearchResults<SearchUserItem>>(`${this._baseUrl}api/user`)
                .toPromise();
            let getCitiesPromise = this._http.get<SearchResults<City>>(`${this._baseUrl}api/city`)
                .toPromise();

            if (id != null) {
                this.model = (await this._http.get<GetTeamResult>(`${this._baseUrl}api/team/${id}`).toPromise())
                    .team;
                this.isEditing = true;
            } else {
                this.model = { isBlocked: false } as Team;
                this.isEditing = false;
            }

            await Promise.all([getUsersPromise, getCitiesPromise]);

            this.users = (await getUsersPromise).items;
            this.cities = (await getCitiesPromise).items;

            this.loaded = true;
        });
    }

    public get buttonText(): string {
        return this.isEditing ? "Обновить" : "Создать";
    }

    public get title(): string {
        return this.isEditing ? this.model.title : "Новая команда";
    }

    public async submit(): Promise<void> {
        if (this.isEditing) {
            await this._http.put(`${this._baseUrl}api/team`, { team: this.model } as UpdateTeam).toPromise();
        } else {
            await this._http.post(`${this._baseUrl}api/team`, { team: this.model } as CreateTeam).toPromise();
        }
        await this._router.navigate(["teams"]);
    }

    public async cancel(): Promise<void> {
        await this._router.navigate(["teams"]);
    }
}

interface GetTeamResult {
    team: Team;
}

interface UpdateTeam {
    team: Team;
}

interface CreateTeam {
    team: Team;
}

interface Team {
    id: string;
    title: string;
    teamLeadId: string;
    cityId: string;
    isBlocked: boolean;
}