import { QuestionType } from './question-type';

export class Question {
    title: string;
    type: QuestionType;
}

export class Answer {
    title: string;
}

export class SelectQuestion extends Question {
    choices: Answer[];
}
