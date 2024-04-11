import { IBaseEntity } from "./BaseEntity";

export interface User extends IBaseEntity {
    name: string,
    email: string
}

export enum Gender {
    Woman,
    Man,
    NonBinary,
    NotSpecified
}