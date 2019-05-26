import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { InterviewStore as InterviewStorage } from '../start/start.component';
import { Router } from '@angular/router';

interface StartQuestion {
    interviewId: string;
}

interface StartQuestionResult {
    question: Question;
    index: number;
    totalCount: number
}

interface Question {
    title: string;
    description: string;
    orderIndex: string;
    questionType: string;
}

interface SelectQuestion extends Question {
    options: Option[];
    oneCorrectAnswer: boolean;
}

interface Option {
    id: string;
    title: string;

    // For results
    checked: boolean;
}

interface Answer {
    interviewId: string;
    value: string;
    values: string[];
}

interface AnswerResult {
    hasQuestions: boolean;
    result?: boolean;
}

@Component({
    selector: 'app-question',
    templateUrl: 'question.component.html'
})
export class QuestionComponent implements OnInit {
    public question: Question;
    public questionIndex: number;
    public totalQuestionsCount: number;
    public loading: boolean;

    public answer: string;
    public selectedOption: Option;
    public selectedOptions: Option[];

    private _result: AnswerResult;

    constructor(
        @Inject("API_BASE_URL") private _baseUrl: string,
        private _httpClient: HttpClient,
        private _router: Router) {
    }

    public get result(): string {
        if (this._result == null) {
            return null;
        }

        if (this._result.result == null) {
            return "Принято!";
        }

        return this._result.result ? "Правильно :)" : "Неправильно :(";
    }

    public get hasNext(): boolean {
        return this._result.hasQuestions;
    }

    public async ngOnInit(): Promise<void> {
        this.loading = true;

        let interviewId = InterviewStorage.interviewId;

        let startInterviewResult = await this._httpClient
            .post<StartQuestionResult>(`${this._baseUrl}api/InterviewWorkflow/question`, { interviewId } as StartQuestion)
            .toPromise();

        this.question = startInterviewResult.question;
        this.questionIndex = startInterviewResult.index;
        this.totalQuestionsCount = startInterviewResult.totalCount;

        this.loading = false;
    }

    public async submit(): Promise<void> {
        this.loading = true;

        let answer: Answer = {
            interviewId: InterviewStorage.interviewId,
            value: null,
            values: null
        };
        let type = this.question.questionType;

        if (type == "General" || type == "Input") {
            answer.value = this.answer;
        } else if (type == "Select" && (this.question as SelectQuestion).oneCorrectAnswer) {
            answer.values = [this.selectedOption.id];
        } else if (type == "Select" && !(this.question as SelectQuestion).oneCorrectAnswer) {
            answer.values = this.selectedOptions.map(o => o.id);
        }

        this._result = await this._httpClient
            .post<AnswerResult>(`${this._baseUrl}api/InterviewWorkflow/answer`, answer)
            .toPromise();

        this.loading = false;
    }

    public async next(): Promise<void> {
        await this.ngOnInit();
    }

    public async complete(): Promise<void> {
        await this._router.navigate(["complete"]);
    }
}