import { Component } from '@angular/core';
import { AccountService } from './_services';
import { User, UserType } from './_models';

@Component({ selector: 'app-root', templateUrl: 'app.component.html' })
export class AppComponent {
    user?: User | null;
    userTypes = UserType;

    constructor(private accountService: AccountService) {
        this.accountService.user.subscribe(x => this.user = x);
    }

    logout() {
        this.accountService.logout();
    }
}