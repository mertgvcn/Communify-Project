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