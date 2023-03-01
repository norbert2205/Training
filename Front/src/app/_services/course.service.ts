import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Course } from '@app/_models';

@Injectable({ providedIn: 'root' })
export class CourseService {
    constructor(
        private http: HttpClient
    ) {
    }

    getAll() {
        return this.http.get<Course[]>(`${environment.apiUrl}/Course/GetAll`);
    }

    getById(id: string) {
        return this.http.get<Course>(`${environment.apiUrl}/Course/Get/${id}`);
    }

    create(course: Course) {          
        return this.http.post(`${environment.apiUrl}/Course/Create`, course);
    }

    update(id: string, params: any) {
        return this.http.put(`${environment.apiUrl}/Course/Update/${id}`, params);
    }

    delete(id: string) {
        return this.http.delete(`${environment.apiUrl}/Course/Delete/${id}`);
    }
}