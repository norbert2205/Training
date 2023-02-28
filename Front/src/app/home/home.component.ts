import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

import { User } from '@app/_models';
import { AccountService } from '@app/_services';
import { environment } from '@environments/environment';
import { map } from 'rxjs';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    user: User | null;

    constructor(private accountService: AccountService,
        private http: HttpClient) {
        this.user = this.accountService.userValue;
    }

    onGeneratePdf()
    {
        return this.http.get(`${environment.apiUrl}/User/CreatePdf`, {
            headers: {
              "Accept": "application/pdf"
            },
            responseType: "blob"
          })
        // .pipe()
        .subscribe({
            next: _ => {
                console.log("x", _);
                var downloadURL = window.URL.createObjectURL(_);
                var link = document.createElement('a');
                link.href = downloadURL;
                link.download = "report.pdf";
                link.click();
            },
            error: () => {
                // this.alertService.error(error);
            }
        });
    }
}    