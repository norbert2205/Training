import { Observable } from "rxjs";
import { User } from "./user";

export class Course {
    Id?: string;
    Name?: string;
    Users?: Observable<User>
}