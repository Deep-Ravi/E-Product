import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Observable, retry } from "rxjs";
import { CreateSkillSet, SkillSet } from "../_models/skillset";


@Injectable({ providedIn: 'root' })
export class SkillSetService {
    constructor(private http: HttpClient) { }

    public getAll(userName?:string) {
        return this.http.get<SkillSet[]>(`${environment.apiUrl}/api/SkillSet/${userName}`).pipe(retry(3));
    }

    public post(skillSet: CreateSkillSet) : Observable<any> {
        return this.http.post<CreateSkillSet>(`${environment.apiUrl}/api/SkillSet`, skillSet);
    }

    public put(skillSet: SkillSet): Observable<any> {
        return this.http.put<CreateSkillSet>(`${environment.apiUrl}/api/SkillSet`, skillSet);
    }
    
    public delete(id: string): Observable<any> {
        return this.http.delete<string>(`${environment.apiUrl}/api/SkillSet/${id}`);
    }
    public getApprovals() {
        return this.http.get<SkillSet[]>(`${environment.apiUrl}/api/SkillSet/Approvals`).pipe(retry(3));
    }
    public setApprovals(skillset:SkillSet[]) {
        return this.http.put<SkillSet[]>(`${environment.apiUrl}/api/SkillSet/Approvals`,skillset);
    }
    public updateApproval(skillSet: SkillSet): Observable<any> {
        return this.http.put<CreateSkillSet>(`${environment.apiUrl}/api/SkillSet/Approval`, skillSet);
    }
} 