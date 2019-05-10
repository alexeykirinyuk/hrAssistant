import { HttpClient } from "@angular/common/http";
import { Inject, OnInit, Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { City } from '../City';

@Component({
    selector: 'app-city',
    templateUrl: './city.component.html'
})
export class CityComponent implements OnInit {
    public city: City;
    private isEditing: boolean;

    constructor(
        private _http: HttpClient,
        @Inject('BASE_URL') private _baseUrl: string,
        private _route: ActivatedRoute,
        private _router: Router) {
    }

    async ngOnInit(): Promise<void> {
        this._route.paramMap.subscribe(async paramsMap => {
            var cityId = paramsMap.get("id");
            if (cityId != null) {
                this.city = (await this._http.get<GetCityResult>(`${this._baseUrl}api/city/${cityId}`).toPromise())
                    .city;
                this.isEditing = true;
            } else {
                this.city = { } as City;
                this.isEditing = false;
            }
        });
    }

    public get buttonText(): string {
        return this.isEditing ? "Обновить" : "Добавить";
    }

    public get title(): string {
        return this.isEditing ? this.city.name : "Новый город";
    }

    public async submit(): Promise<void> {
        if (this.isEditing) {
            await this._http.put(`${this._baseUrl}api/city`, { city: this.city } as UpdateCity).toPromise();
        } else {
            await this._http.post(`${this._baseUrl}api/city`, { city: this.city } as CreateCity).toPromise();
        }
        await this._router.navigate(["cities"]);
    }

    public async cancel(): Promise<void> {
        await this._router.navigate(["cities"]);
    }
}

interface GetCityResult {
    city: City;
}

interface UpdateCity {
    city: City;
}

interface CreateCity {
    city: City;
}