import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SearchResults } from '../../libs/search-results';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-vacancies',
    templateUrl: './vacancies.component.html'
})
export class VacanciesComponent implements OnInit {
    private ONE_PAGE_ITEMS_COUNT = 50;

    public model: SearchResults<SearchVacancyItem>;
    public pageIndex: number;

    public displayedColumns: string[] = ["team", "jobPosition", "salary", "jobsNumber", "status"];

    constructor(
        private _http: HttpClient,
        @Inject('BASE_URL') private _baseUrl: string,
        private _route: ActivatedRoute,
        private _router: Router) {
    }

    private getParams(): { [param: string]: string } {
        let params: { [param: string]: string } = {};
        params["onePageItemsCount"] = this.ONE_PAGE_ITEMS_COUNT.toString();
        params["pageIndex"] = this.pageIndex.toString();

        return params;
    }

    public async ngOnInit(): Promise<void> {
        this._route.paramMap.subscribe(async paramsMap => {
            this.pageIndex = paramsMap["pageIndex"] == null ? +paramsMap["pageIndex"] : 1;

            this.model = await this._http
                .get<SearchResults<SearchVacancyItem>>(`${this._baseUrl}api/vacancy`, { params: this.getParams() })
                .toPromise();
        });
    }

    public async createNew(): Promise<void> {
        await this._router.navigate(["vacancy"]);
    }

    public async open(vacancy: SearchVacancyItem): Promise<void> {
        await this._router.navigate(["vacancy", vacancy.vacancyId]);
    }
}

interface SearchVacancyItem {
    vacancyId: string;
    teamId: string;
    teamTitle: string;
    jobPositionId: string;
    jobPositionTitle: string;
    salary: number;
    jobsNumber: number;
    status: string;
}
