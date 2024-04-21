import { IBaseEntity } from "./BaseEntity";

export interface PasswordToken extends IBaseEntity {
    token : string,
    expireDate : Date,
    userId : number
}

export interface Token {
    role : string;
    aud : string;
    exp : number;
    iss : string;
    nbf : number;
}

export enum Roles {
    Guest = 0,
    Admin = 1,
    User = 2,
}