import { Question } from './Question';
import { Option } from "./Option";

export interface SelectQuestion extends Question {
    options: Option[];
    oneCorrectAnswer: boolean;
}
