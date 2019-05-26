import { Component } from '@angular/core';
import { Location } from '@angular/common';

@Component({
    selector: 'app-terms',
    templateUrl: 'terms.component.html'
})
export class TermsComponent {
    constructor(private _location: Location) {
    }

    public back() {
        this._location.back();
    }
}