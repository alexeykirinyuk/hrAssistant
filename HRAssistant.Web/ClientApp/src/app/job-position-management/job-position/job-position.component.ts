import { OnInit, Inject, Component } from '@angular/core';
import { JobPosition } from '../models/JobPosition';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { GetJobPositionResult } from '../models/GetJobPositionResult';
import { Template } from '../models/Template';
import { UpdateJobPosition } from '../models/UpdateJobPosition';
import { CreateJobPosition } from '../models/CreateJobPosition';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';
import { QuestionType } from '../models/QuestionType';
import { GeneralQuestion } from '../models/GeneralQuestion';
import { Question } from '../models/Question';
import { InputQuestion } from '../models/InputQuestion';
import { SelectQuestion } from '../models/SelectQuestion';
import { Option } from '../models/Option';
import { EnumUtils } from 'src/app/utils/EnumUtils';
import { Location } from '@angular/common';

@Component({
    selector: 'app-job-position',
    templateUrl: './job-position.component.html',
    styleUrls: ['./job-position.component.scss']
})
export class JobPositionComponent implements OnInit {
    public jobPosition: JobPosition;
    private isEditing: boolean;

    constructor(
        private _http: HttpClient,
        @Inject('BASE_URL') private _baseUrl: string,
        private _route: ActivatedRoute,
        private _router: Router,
        private _location: Location) {
    }

    async ngOnInit(): Promise<void> {

        this._route.paramMap.subscribe(async paramsMap => {
            var jobPositionId = paramsMap.get("id");
            if (jobPositionId != null) {
                this.jobPosition = (await this._http
                    .get<GetJobPositionResult>(`${this._baseUrl}api/jobPosition/${jobPositionId}`)
                    .toPromise())
                    .jobPosition;
                this.isEditing = true;
            } else {
                this.jobPosition = {
                    template: { questions: [] } as Template,
                } as JobPosition;
                this.isEditing = false;
            }
        });
    }

    public get buttonText(): string {
        return this.isEditing ? "Update" : "Create";
    }

    public async submit(): Promise<void> {
        this.updateQuestionOrderIndexes();

        if (this.isEditing) {
            await this._http.put(`${this._baseUrl}api/jobPosition`, { jobPosition: this.jobPosition } as UpdateJobPosition).toPromise();
        } else {
            await this._http.post(`${this._baseUrl}api/jobPosition`, { jobPosition: this.jobPosition } as CreateJobPosition).toPromise();
        }
        await this._router.navigate(["jobPositions"]);
    }

    private updateQuestionOrderIndexes() {
        for (let i = 0; i < this.jobPosition.template.questions.length; i++) {
            this.jobPosition.template.questions[i].orderIndex = i;
        }
    }

    public async cancel(): Promise<void> {
        await this._location.back();
    }

    public addGeneralQuestion(): void {
        this.jobPosition.template.questions.push({questionType: QuestionType.General} as GeneralQuestion);
    }

    public addInputQuestion(): void {
        this.jobPosition.template.questions.push({questionType: QuestionType.Input} as InputQuestion);
    }

    public addSelectQuestion(): void {
        this.jobPosition.template.questions.push({questionType: QuestionType.Select, options: []} as SelectQuestion);
    }

    public removeQuestion(question: Question): void {
        this.jobPosition.template.questions = this.jobPosition.template.questions
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
        moveItemInArray(this.jobPosition.template.questions, event.previousIndex, event.currentIndex);
    }

    public addOption(question: SelectQuestion): void {
        question.options.push({title: "", isCorrect: false} as Option);
    }

    public dropOption(question: SelectQuestion, event: CdkDragDrop<Option[]>): void {
        moveItemInArray(question.options, event.previousIndex, event.currentIndex);
    }

  public removeOption(question: SelectQuestion, option: Option): void {
      question.options = question.options.filter(v => v !== option);
  }
}
