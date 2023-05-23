export class User {
    id?: number;
    username?: string;
    email?:string;
    password?: string;
    token?: string;
    roleId?:number;
    operations?:string[];
    rolename?:string;
    operationId?:number;
    lastPasswordChange?:Date;
}

export class TokenResponse {
    userName?: string;
    token?: string;
}

export class Role{
    id?: number;
    name?: string;
}