import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription, from } from 'rxjs';
import { JobService } from '../../services/job.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
  })
export class HomeComponent implements OnInit, OnDestroy {
    jobTitle: string;
    private routesSubscription: Subscription;

    constructor(private routes: ActivatedRoute, private jobs: JobService) { }

    ngOnInit(): void {
        this.routesSubscription = this.routes.params.subscribe(async params => {
            const job = await this.jobs.getJob(params.job as string);
            this.jobTitle = job.title;
        });
    }

    ngOnDestroy(): void {
        this.routesSubscription.unsubscribe();
    }
}
