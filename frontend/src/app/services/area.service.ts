import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ICountry, IProvince } from './area.types';

@Injectable({
  providedIn: 'root'
})
export class AreaService {

  constructor(private router: Router, private http: HttpClient) { }

  getCountries() : Observable<ICountry[]> {
    return this.http.get<ICountry[]>(`/area/getcountries`)
  }

  getProvinces(countryId: number) : Observable<IProvince[]> {
    return this.http.get<IProvince[]>(`/area/getprovinces?countryId=${countryId}`)
  }
}
