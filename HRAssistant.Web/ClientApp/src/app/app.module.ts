import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { UsersComponent } from './user-management/users/users.component';
import { UserComponent } from './user-management/user/user.component';
import { JobPositionsComponent } from './job-position-management/job-positions/job-positions.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material-module';
import { JobPositionComponent } from './job-position-management/job-position/job-position.component';

@NgModule({
  declarations: [
    AppComponent,
    UsersComponent,
    UserComponent,
    JobPositionsComponent,
    JobPositionComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: UsersComponent, pathMatch: 'full' },

      { path: 'users/:pageIndex', component: UsersComponent },
      { path: 'users', component: UsersComponent },
      { path: 'user/:id', component: UserComponent },
      { path: 'user', component: UserComponent },

      { path: 'jobPositions/:pageIndex', component: JobPositionsComponent },
      { path: 'jobPositions', component: JobPositionsComponent },
      { path: 'jobPosition/:id', component: JobPositionComponent },
      { path: 'jobPosition', component: JobPositionComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
