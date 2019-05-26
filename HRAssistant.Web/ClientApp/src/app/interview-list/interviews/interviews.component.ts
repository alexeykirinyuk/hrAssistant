import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SearchResults } from '../../libs/search-results';
import { Router, ActivatedRoute } from '@angular/router';

export interface SearchInterviewItem {
    interviewId: string;
    jobPositionTitle: string;
    teamTitle: string;
    candidateFullName: string;
    correctAnswersCount: number;
    incorrectAnswersCount: number;
}

@Component({
  selector: 'app-interviews',
  templateUrl: './interviews.component.html',
  styleUrls: ['./interviews.component.scss']
})
export class InterviewsComponent implements OnInit {
  private ONE_PAGE_ITEMS_COUNT = 50;

  public model: SearchResults<SearchInterviewItem>;
  public pageIndex: number;

  public displayedColumns: string[] = ["jobPositionTitle", "teamTitle", "candidateFullName", "correctAnswersCount", "incorrectAnswersCount"];

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

      this.model = await this._http.get<SearchResults<SearchInterviewItem>>(`${this._baseUrl}api/interviewlist`, { params: this.getParams() })
        .toPromise();
    });
  }

  public async open(interview: SearchInterviewItem): Promise<void> {
    await this._router.navigate(["interview", interview.interviewId]);
  }
}
