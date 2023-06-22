import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Observable, retry } from "rxjs";
import { Skill } from "../_models/skill";


@Injectable({ providedIn: 'root' })
export class SkillService {
    constructor(private http: HttpClient) { }

    public getAll(categoryId:string) {
        return this.http.get<Skill[]>(`${environment.apiUrl}/api/Skill/${categoryId}`);
    }
} 