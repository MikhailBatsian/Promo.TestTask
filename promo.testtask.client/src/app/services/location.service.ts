import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../environments/environment'; 

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  constructor(private http:HttpClient) { }
  baseURL = `${environment.apiUrl}/location`;

  getCountries(): Observable<any[]>{
    return this.http.get<any[]>(this.baseURL+'/countries');
  }

  getProvinces(countryId:number): Observable<any[]>{
    let params = new HttpParams().set('countryId', countryId);
    return this.http.get<any[]>(this.baseURL+'/provinces', {params});
  }
}
