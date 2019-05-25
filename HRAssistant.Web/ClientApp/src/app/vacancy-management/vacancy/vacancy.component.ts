import { Component, OnInit, Inject } from '@angular/core';
import { Question } from 'src/app/job-position-management/models/Question';
import { Vacancy } from './Vacancy';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionType } from 'src/app/job-position-management/models/QuestionType';
import { GeneralQuestion } from 'src/app/job-position-management/models/GeneralQuestion';
import { InputQuestion } from 'src/app/job-position-management/models/InputQuestion';
import { SelectQuestion } from 'src/app/job-position-management/models/SelectQuestion';
import { EnumUtils } from 'src/app/utils/EnumUtils';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { GetVacancyResult } from './GetVacancyResult';
import { SearchResults } from 'src/app/libs/search-results';
import { SearchJobPositionItem } from 'src/app/job-position-management/job-positions/SearchJobPositionItem';
import { City } from 'src/app/city-management/City';
import { UpdateVacancy } from './UpdateVacancy';
import { CreateVacancy } from './CreateVacancy';
import { Location } from '@angular/common';
import { Option } from 'src/app/job-position-management/models/Option';
import { SearchTeamItem } from 'src/app/team-management/teams/SearchTeamItem';

interface CreateVacancyResult {
    vacancyId: string;
}

@Component({
    selector: 'app-vacancy',
    templateUrl: './vacancy.component.html',
    styleUrls: ['./vacancy.component.scss']
})
export class VacancyComponent implements OnInit {
    public vacancy: Vacancy;
    public vacancyStatus: string;
    public jobPositions: SearchJobPositionItem[];
    public teams: SearchTeamItem[];

    private isEditing: boolean = false;

    constructor(
        private _http: HttpClient,
        @Inject('BASE_URL') private _baseUrl: string,
        private _route: ActivatedRoute,
        private _router: Router,
        private _location: Location) {
    }

    public get canOpen(): boolean {
        return this.vacancyStatus === "Draft";
    }

    public get canClose(): boolean {
        return this.vacancyStatus === "Opened" || this.vacancyStatus == "Draft";
    }

    async ngOnInit(): Promise<void> {
        let getJobPositionsPromise = this._http.get<SearchResults<SearchJobPositionItem>>(`${this._baseUrl}api/jobPosition`).toPromise();
        let getCitiesPromise = this._http.get<SearchResults<SearchTeamItem>>(`${this._baseUrl}api/team`).toPromise();

        await Promise.all([getJobPositionsPromise, getCitiesPromise]);

        this.jobPositions = (await getJobPositionsPromise).items;
        this.teams = (await getCitiesPromise).items;

        this.vacancy = {
            candidateRequirements: "",
            jobsNumber: 0,
            salary: 0,
            form: {
                description: "",
                questions: []
            },
            jobPositionId: null,
            teamId: null,
        } as Vacancy;

        this._route.paramMap.subscribe(async paramsMap => {
            var jobPositionId = paramsMap.get("id");

            if (jobPositionId != null) {
                this.vacancy = (await this._http
                    .get<GetVacancyResult>(`${this._baseUrl}api/vacancy/${jobPositionId}`)
                    .toPromise())
                    .vacancy;
                this.vacancyStatus = this.vacancy.status;
                this.vacancy.status = null;
                this.isEditing = true;
            }
        });
    }

    public get buttonText(): string {
        return this.isEditing ? "Обновить черновик" : "Создать черновик";
    }

    public async submit(): Promise<void> {
        await this.saveChanges();
        await this._router.navigate(["vacancies"]);
    }

    public async open(): Promise<void> {
        let id = await this.saveChanges();
        await this._http.post(`${this._baseUrl}api/vacancy/open/${id}`, {}).toPromise();
        await this._router.navigate(["vacancies"]);
    }

    public async close(): Promise<void> {
        let id = this.vacancy.id;
        if (id == null) {
            id = await this.saveChanges();
        }
        await this._http.post(`${this._baseUrl}api/vacancy/close/${id}`, {}).toPromise();
        await this._router.navigate(["vacancies"]);
    }

    private async saveChanges(): Promise<string> {
        this.updateQuestionOrderIndexes();
        if (this.isEditing) {
            await this._http.put(`${this._baseUrl}api/vacancy`, { vacancy: this.vacancy } as UpdateVacancy).toPromise();
            return this.vacancy.id;
        }
        else {
            let createVacancyResult = await this._http.post<CreateVacancyResult>(`${this._baseUrl}api/vacancy`, { vacancy: this.vacancy } as CreateVacancy).toPromise();

            return createVacancyResult.vacancyId;
        }
    }

    private updateQuestionOrderIndexes() {
        for (let i = 0; i < this.vacancy.form.questions.length; i++) {
            this.vacancy.form.questions[i].orderIndex = i;
        }
    }

    public async cancel(): Promise<void> {
        await this._location.back();
    }

    public addGeneralQuestion(): void {
        this.vacancy.form.questions.push({ questionType: QuestionType.General } as GeneralQuestion);
    }

    public addInputQuestion(): void {
        this.vacancy.form.questions.push({ questionType: QuestionType.Input } as InputQuestion);
    }

    public addSelectQuestion(): void {
        this.vacancy.form.questions.push({ questionType: QuestionType.Select, options: [] } as SelectQuestion);
    }

    public removeQuestion(question: Question): void {
        this.vacancy.form.questions = this.vacancy.form.questions
            .filter(q => q != question);
    }

    public isGeneralQuestion(question: Question): boolean {
        return EnumUtils.equals(question.questionType, QuestionType.General, QuestionType);
    }

    public isInputQuestion(question: Question): boolean {
        return EnumUtils.equals(question.questionType, QuestionType.Input, QuestionType);
    }

    public isSelectQuestion(question: Question): boolean {
        return EnumUtils.equals(question.questionType, QuestionType.Select, QuestionType);
    }

    public drop(event: CdkDragDrop<Question[]>): void {
        moveItemInArray(this.vacancy.form.questions, event.previousIndex, event.currentIndex);
    }

    public addOption(question: SelectQuestion): void {
        question.options.push({ title: "", isCorrect: false } as Option);
    }

    public dropOption(question: SelectQuestion, event: CdkDragDrop<Option[]>): void {
        moveItemInArray(question.options, event.previousIndex, event.currentIndex);
    }

    public removeOption(question: SelectQuestion, option: Option): void {
        question.options = question.options.filter(v => v !== option);
    }
}
