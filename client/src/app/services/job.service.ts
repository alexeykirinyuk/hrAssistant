import { Injectable } from '@angular/core';
import { Job } from '../models/job';
import { ApiClient } from './api-client';

@Injectable()
export class JobService {
    constructor(private client: ApiClient) { }

    async getJob(code: string): Promise<Job> {
        return this.client.get<Job>(`/api/jobs/${code}`);
    }
}
