import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Observable, retry } from "rxjs";
import { Role, User } from "../_models/user";


@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

    public getAll() {
        return this.http.get<User[]>(`${environment.apiUrl}/api/User`).pipe(retry(3));
    }

    public post(user: User) : Observable<any> {
        return this.http.post<User>(`${environment.apiUrl}/api/User`, user);
    }

    public put(user: User): Observable<any> {
        return this.http.put<User>(`${environment.apiUrl}/api/User`, user);
    }
    
    public delete(id: number): Observable<any> {
        return this.http.delete<number>(`${environment.apiUrl}/api/User/${id}`);
    }

    public getRoles() {
        return this.http.get<Role[]>(`${environment.apiUrl}/api/User/Roles`).pipe(retry(3));
    }
} 