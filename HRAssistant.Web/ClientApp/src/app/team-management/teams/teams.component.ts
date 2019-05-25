import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SearchResults } from '../../libs/search-results';
import { Router, ActivatedRoute } from '@angular/router';
import { SearchTeamItem } from './SearchTeamItem';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html'
})
export class TeamsComponent implements OnInit {
  private ONE_PAGE_ITEMS_COUNT = 50;

  public model: SearchResults<SearchTeamItem>;
  public pageIndex: number;

  public displayedColumns: string[] = ["title", "teamLeadFullName", "cityTitle", "isBlocked"];

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

      this.model = await this._http.get<SearchResults<SearchTeamItem>>(`${this._baseUrl}api/team`, { params: this.getParams() })
        .toPromise();
    });
  }

  public async createNew(): Promise<void> {
    await this._router.navigate(["team"]);
  }

  public async open(team: SearchTeamItem): Promise<void> {
    await this._router.navigate(["team", team.teamId]);
  }
}
