import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Vacancy } from './Vacancy';
import { Contact } from './Contact';

interface SetContactResult {
    interviewId: string;
}

export class InterviewStore {
    private static readonly STORAGE_KEY: string = "tinkoff.interviewId";

    public static get interviewId(): string {
        return localStorage.getItem(this.STORAGE_KEY);
    }

    public static set interviewId(value: string) {
        localStorage.setItem(this.STORAGE_KEY, value);
    }
}

@Component({
    selector: 'app-start',
    templateUrl: 'start.component.html',
    styleUrls: ['start.component.scss']
})
export class StartComponent implements OnInit {
    public vacancy: Vacancy;
    public contact: Contact;

    constructor(
        @Inject("API_BASE_URL") private _api: string,
        private _httpClient: HttpClient,
        private _activatedRoute: ActivatedRoute,
        private _router: Router) {
    }

    public async ngOnInit(): Promise<void> {
        this._activatedRoute.paramMap.subscribe(async query => {
            let vacancyId: string = query.get("id");

            this.vacancy = await this._httpClient.get<Vacancy>(`${this._api}api/InterviewWorkflow/vacancy/${vacancyId}`).toPromise();
            this.contact = {
                vacancyId: vacancyId,
                firstName: "",
                lastName: "",
                email: "",
                phone: "",
                termsAgreed: false
            };
        });
    }

    public async submit(): Promise<void> {
        let result = await this._httpClient.post<SetContactResult>(`${this._api}api/InterviewWorkflow/contact`, this.contact).toPromise();
        InterviewStore.interviewId = result.interviewId;
        
        await this._httpClient
            .post(`${this._api}api/InterviewWorkflow/start`, { interviewId: result.interviewId })
            .toPromise();

        await this._router.navigate(["question"]);
    }
}