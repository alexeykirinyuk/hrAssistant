import { Question } from 'src/app/job-position-management/models/Question';

export interface Form {
    description: string;
    questions: Question[];
}
