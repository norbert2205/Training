import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { first, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { User } from '@app/_models';

@Injectable({ providedIn: 'root' })
export class AccountService {
    private userSubject: BehaviorSubject<User | null>;
    public user: Observable<User | null>;
    private authenticateTimeout?: NodeJS.Timeout;

    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.userSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('user')!));
        this.user = this.userSubject.asObservable();
    }

    public get userValue() {
        return this.userSubject.value;
    }

    login(username: string, password: string) {
        return this.http.post<User>(`${environment.apiUrl}/User/Login`,
        {
        "Username": username,
        "Password": password
        })
            .pipe(map(user => {
                this.userSubject.next(user);
                this.startAuthenticateTimer();
                return user;
            }));
    }
    
    logout() {
        this.stopAuthenticateTimer();
        this.userSubject.next(null);
        this.router.navigate(['/account/login']);
    }

    register(user: User) {          
        return this.http.post(`${environment.apiUrl}/User/Register`, user);
    }

    getAll() {
        return this.http.get<User[]>(`${environment.apiUrl}/User/GetAll`);
    }

    getById(id: string) {
        return this.http.get<User>(`${environment.apiUrl}/User/Get/${id}`);
    }

    update(id: string, params: any) {
        return this.http.put(`${environment.apiUrl}/User/Update/${id}`, params)
            .pipe(map(x => {
                // update stored user if the logged in user updated their own record
                if (id == this.userValue?.Id) {
                    const user = { ...this.userValue, ...params };
                    // publish updated user to subscribers
                    this.userSubject.next(user);
                }
                return x;
            }));
    }

    delete(id: string) {
        return this.http.delete(`${environment.apiUrl}/User/Delete/${id}`)
            .pipe(map(x => {
                // auto logout if the logged in user deleted their own record
                if (id == this.userValue?.Id) {
                    this.logout();
                }
                return x;
            }));
    }

    private startAuthenticateTimer() {
        // parse json object from base64 encoded jwt token
        const jwtBase64 = this.userValue!.Token!.split('.')[1];
        const jwtToken = JSON.parse(atob(jwtBase64));

        // set a timeout to re-authenticate with the api one minute before the token expires
        const expires = new Date(jwtToken.exp * 1000);
        const timeout = expires.getTime() - Date.now() - (60 * 1000);

        this.authenticateTimeout = setTimeout(() => {
            this.login(this.userValue!.Username!, this.userValue?.Password!).subscribe();
        }, timeout);
    }

    private stopAuthenticateTimer() {
        clearTimeout(this.authenticateTimeout);
    }
}