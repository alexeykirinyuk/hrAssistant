import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { Config } from './config';
import { QuestionService } from './services/question.service';
import { JobService } from './services/job.service';
import { ApiClient } from './services/api-client';
import { QuestionComponent } from './pages/question/question.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    QuestionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [Config, JobService, QuestionService, ApiClient],
  bootstrap: [AppComponent, HomeComponent]
})
export class AppModule { }
