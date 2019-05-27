import { Injectable, ErrorHandler, Injector, Inject, Component, ApplicationRef } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';

@Injectable()
export class ApiExceptionHandler implements ErrorHandler {
  constructor(private injector: Injector) {
  }

  async handleError(error: any): Promise<void> {
    const dialog = this.injector.get(MatDialog);

    const errors: Array<string> = error.rejection.error;
    if (errors == null) {
      throw error;
    }

    dialog.open(ValidationDialog,
      {
        width: "500px",
        data: errors,
        disableClose: false
      });
  }
}

@Component({
  selector: 'app-validation',
  template: `
        <h1 mat-dialog-title>Ошибка валидации данных</h1>
        <div mat-dialog-content>
            <ul>
                <li *ngFor="let error of errors">{{error}}</li>
            </ul>
        </div>
        <div mat-dialog-actions>
            <button mat-button (click)="close()" cdkFocusInitial>Закрыть</button>
        </div>`
})
export class ValidationDialog {
  private _appRef: ApplicationRef;

  constructor(
    private _dialogRef: MatDialogRef<ValidationDialog>,
    @Inject(MAT_DIALOG_DATA) public errors: string[],
    private _injector: Injector) {
    setTimeout(() => this._appRef = this._injector.get(ApplicationRef));
  }

  close() {
    this._dialogRef.close();
    this._appRef.tick();
  }
}
