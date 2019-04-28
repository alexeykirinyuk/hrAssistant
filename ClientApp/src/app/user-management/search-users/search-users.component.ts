import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SearchResults } from '../../libs/search-results';

@Component({
  selector: 'app-search-users',
  templateUrl: './search-users.component.html'
})
export class SearchUsersComponent {
  public model: SearchResults<SearchUserItem>;

  constructor(http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    http.get<SearchResults<SearchUserItem>>(baseUrl + 'api/user')
      .subscribe(result => this.model = result, error => console.error(error));
  }
}

interface SearchUserItem {
  userId: string;
  username: string;
  displayName: string;
  role: string;
  blocked: boolean;
}
