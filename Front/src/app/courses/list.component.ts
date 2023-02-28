import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { CourseService } from '@app/_services';

@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
    courses?: any[];

    constructor(private courseService: CourseService) {}

    ngOnInit() {
        this.courseService.getAll()
            .pipe(first())
            .subscribe(courses => this.courses = courses);
    }

    deleteCourse(id: string) {
        const course = this.courses!.find(x => x.Id === id);
        course.isDeleting = true;
        this.courseService.delete(id)
            .pipe(first())
            .subscribe(() => this.courses = this.courses!.filter(x => x.Id !== id));
    }
}