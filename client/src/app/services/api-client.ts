import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Config } from '../config';

@Injectable()
export class ApiClient {
    constructor(private client: HttpClient, private config: Config) {}

    async get<T>(url: string): Promise<T> {
        return (await this.client.get(await this.getUrl(url)).toPromise()) as T;
    }

    async post<T>(url: string, body: any): Promise<T> {
        return (await this.client.post(await this.getUrl(url), body).toPromise()) as T;
    }

    async getUrl(url: string): Promise<string> {
        const baseUrl = await this.config.getBaseUrl();
        return baseUrl + url;
    }
}
