import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Observable, retry } from "rxjs";
import { Product } from "../_models/product";


@Injectable({ providedIn: 'root' })
export class ProductService {
    constructor(private http: HttpClient) { }

    public getAll() {
        return this.http.get<Product[]>(`${environment.apiUrl}/api/Product`).pipe(retry(3));
    }

    public post(product: Product) : Observable<any> {
        return this.http.post<Product>(`${environment.apiUrl}/api/Product`, product);
    }

    public put(product: Product): Observable<any> {
        return this.http.put<Product>(`${environment.apiUrl}/api/Product`, product);
    }
    
    public delete(id: string): Observable<any> {
        return this.http.delete<string>(`${environment.apiUrl}/api/Product/${id}`);
    }
} 