import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material-module';
import { AppComponent } from './app.component';
import { StartComponent } from './start/start.component';
import { TermsComponent } from './terms/terms.component';
import { QuestionComponent } from './question/question.component';
import { CompleteComponent } from './complete/complete.component';

@NgModule({
  declarations: [
    AppComponent,
    StartComponent,
    TermsComponent,
    QuestionComponent,
    CompleteComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'interview/:id', component: StartComponent },
      { path: 'terms', component: TermsComponent },

      { path: "question", component: QuestionComponent },
      { path: "complete", component: CompleteComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
