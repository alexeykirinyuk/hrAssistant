import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SearchResults } from '../../libs/search-results';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html'
})
export class UsersComponent implements OnInit {
  private ONE_PAGE_ITEMS_COUNT = 50;

  public model: SearchResults<SearchUserItem>;
  public pageIndex: number;
  public displayedColumns: string[] = ["username", "displayName", "role", "blocked"];

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

      this.model = await this._http.get<SearchResults<SearchUserItem>>(`${this._baseUrl}api/user`, { params: this.getParams() })
        .toPromise();
    });
  }

  public async createNew(): Promise<void> {
    await this._router.navigate(["user"]);
  }

  public async openUser(user: SearchUserItem): Promise<void> {
    await this._router.navigate(["user", user.userId]);
  }
}

interface SearchUserItem {
  userId: string;
  username: string;
  displayName: string;
  role: string;
  blocked: boolean;
}
