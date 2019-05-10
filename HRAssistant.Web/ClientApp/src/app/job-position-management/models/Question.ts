import { QuestionType } from "./QuestionType";

export interface Question {
    title: string;
    description: string;
    orderIndex: number;
    questionType: QuestionType;
}
