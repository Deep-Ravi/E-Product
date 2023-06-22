import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Category } from "../_models/category";


@Injectable({ providedIn: 'root' })
export class CategoryService {
    constructor(private http: HttpClient) { }

    public getAll() {
        return this.http.get<Category[]>(`${environment.apiUrl}/api/Category/`);
    }    
} 