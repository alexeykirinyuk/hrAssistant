import { Question } from './Question';

export interface Template {
    description: string;
    questions: Question[];
}
