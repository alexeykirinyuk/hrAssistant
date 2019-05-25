import { Form } from './Form';

export interface Vacancy {
    id: string;
    teamId: string;
    jobPositionId: string;
    salary: number;
    candidateRequirements: string;
    jobsNumber: number;
    status: string;
    form: Form;
}
