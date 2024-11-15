import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../environments/environment'; 

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  constructor(private http:HttpClient) { }
  baseURL = `${environment.apiUrl}/account`;

  createUser(userData: any): Observable<any> {
    return this.http.post<any>(`${this.baseURL}/create-user`, userData);
  }
}
