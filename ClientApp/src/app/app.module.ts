import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { UsersComponent } from './user-management/users/users.component';
import { UserComponent } from './user-management/user/user.component';
import { JobPositionsComponent } from './job-position-management/job-positions/job-positions.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    UsersComponent,
    UserComponent,
    JobPositionsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: UsersComponent, pathMatch: 'full' },
      { path: 'users/:pageIndex', component: UsersComponent },
      { path: 'users', component: UsersComponent },
      { path: 'jobPositions/:pageIndex', component: JobPositionsComponent },
      { path: 'jobPositions', component: JobPositionsComponent },
      { path: 'user/:id', component: UserComponent },
      { path: 'user', component: UserComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
