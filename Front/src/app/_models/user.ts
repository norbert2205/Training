export class User {
    Id?: string;
    Username?: string;
    Password?: string;
    FirstName?: string;
    LastName?: string;
    Token?: string;
    Type?: UserType;
}

export enum UserType {
    Admin = 1,
    Teacher = 2,
    Student = 3
}