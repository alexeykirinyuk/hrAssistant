import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class Config {
    private CONFIG_URL = 'assets/config.json';

    constructor(private httpClient: HttpClient) { }

    async getBaseUrl(): Promise<string> {
        const configs: any = await this.httpClient.get(this.CONFIG_URL).toPromise();
        return configs.url;
    }
}
