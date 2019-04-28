import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SearchResults } from '../../libs/search-results';
import { Router } from '@angular/router';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html'
})
export class UsersComponent {
  public model: SearchResults<SearchUserItem>;

  constructor(http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private _router: Router) {
    http.get<SearchResults<SearchUserItem>>(baseUrl + 'api/user')
      .subscribe(result => this.model = result, error => console.error(error));
  }

  public async createNew(): Promise<void> {
    await this._router.navigate(["user"]);
  }
}

interface SearchUserItem {
  userId: string;
  username: string;
  displayName: string;
  role: string;
  blocked: boolean;
}
