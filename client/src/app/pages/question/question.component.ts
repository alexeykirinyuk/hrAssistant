import { Component, OnInit, OnDestroy } from '@angular/core';
import { QuestionService } from 'src/app/services/question.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription, Observable } from 'rxjs';
import { Question } from '../../models/question';

@Component({
    selector: 'app-question',
    templateUrl: './question.component.html',
    styleUrls: ['./question.component.scss']
})
export class QuestionComponent implements OnInit, OnDestroy {
    private QUESTIONS_COOKIE_KEY = 'QUESTIONS';
    private routesSubscription: Subscription;

    title: string;
    question: Question;

    constructor(
        private routes: ActivatedRoute,
        private questionService: QuestionService) {
    }

    ngOnInit() {
        this.routesSubscription = this.routes.params.subscribe(async params => {
            const index = +params.index;
            const jobCode = params.jobCode as string;

            const questions = await this.getQuestions(jobCode, index);
            this.question = questions[index];
        });
    }

    private async getQuestions(jobCode: string, index: number): Promise<Question[]> {
        if (index === 1) {
            const questions = await this.questionService.getAll(jobCode);
            sessionStorage.setItem(this.QUESTIONS_COOKIE_KEY, JSON.stringify(questions));

            return questions;
        }

        return JSON.parse(sessionStorage.getItem(this.QUESTIONS_COOKIE_KEY));
    }

    ngOnDestroy() {
        this.routesSubscription.unsubscribe();
    }
}
