import { Component, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { MatSidenav } from '@angular/material';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';

  @ViewChild('sidenav') sidenav: MatSidenav;

  constructor(private _router: Router) {
  }

  public async userManagement(): Promise<void> {
    await this.navigateTo("users");
  }

  public async jobPositionManagement(): Promise<void> {
    await this.navigateTo("jobPositions");
  }

  public async cityManagement(): Promise<void> {
    await this.navigateTo("cities");
  }

  public async teamManagement(): Promise<void> {
    await this.navigateTo("teams");
  }

  public async vacancyManagement(): Promise<void> {
    await this.navigateTo("vacancies");
  }

  private async navigateTo(page: string) {
    await this.sidenav.close();
    await this._router.navigate([page]);
  }
}
