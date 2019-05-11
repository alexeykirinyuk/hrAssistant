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
import { CitisComponent } from './city-management/cities/cities.component';
import { CityComponent } from './city-management/city/city.component';
import { TeamsComponent } from './team-management/teams/teams.component';
import { TeamComponent } from './team-management/team/team.component';

@NgModule({
  declarations: [
    AppComponent,
    UsersComponent,
    UserComponent,
    JobPositionsComponent,
    JobPositionComponent,
    CitisComponent,
    CityComponent,
    TeamsComponent,
    TeamComponent
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

      { path: 'cities/:pageIndex', component: CitisComponent },
      { path: 'cities', component: CitisComponent },
      { path: 'city/:id', component: CityComponent },
      { path: 'city', component: CityComponent },

      { path: 'teams/:pageIndex', component: TeamsComponent },
      { path: 'teams', component: TeamsComponent },
      { path: 'team/:id', component: TeamComponent },
      { path: 'team', component: TeamComponent },

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
