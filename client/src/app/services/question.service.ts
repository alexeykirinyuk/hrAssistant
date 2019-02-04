import { Injectable } from '@angular/core';
import { Question } from '../models/question';
import { ApiClient } from './api-client';

@Injectable()
export class QuestionService {
    constructor(private server: ApiClient) { }

    getAll(jobCode: string): Promise<Question[]> {
        return this.server.get<Question[]>(`/api/jobs/${jobCode}`);
    }
}
