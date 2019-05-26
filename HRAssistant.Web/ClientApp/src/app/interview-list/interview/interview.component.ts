import { Component, OnInit, Inject } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

interface Question {
    title: string;
    answer: string;
    result?: boolean;
}

interface Interview {
    fullName: string;
    email: string;
    phone: string;

    teamTitle: string;
    jobPositionTitle: string;
    cityTitle: string;

    correctAnswerTitle: number;
    incorrectAnswerTitle: number;

    questions: Question[];
}

@Component({
    selector: "app-interview",
    styleUrls: ["interview.component.scss"],
    templateUrl: "interview.component.html"
})
export class InterviewComponent implements OnInit {
    public model: Interview;
    
    constructor(
        private _http: HttpClient,
        @Inject('BASE_URL') private _baseUrl: string,
        private _route: ActivatedRoute) {
    }

    public async ngOnInit(): Promise<void> {
        this._route.paramMap.subscribe(async paramsMap => {
            let id = paramsMap.get("id");

            this.model = await this._http.get<Interview>(`${this._baseUrl}api/interviewlist/${id}`).toPromise();
        });
    }

    public getColor(result: boolean): string {
        if (result == null) {
            return "black";
        }

        return result ? "green" : "red";
    }
}