import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { SearchUsersComponent } from './user-management/search-users/search-users.component';
import { EditUserComponent } from './user-management/edit-user/edit-user.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SearchUsersComponent,
    EditUserComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: SearchUsersComponent, pathMatch: 'full' },
      { path: 'users', component: SearchUsersComponent },
      { path: 'user/:id', component: EditUserComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
