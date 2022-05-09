import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IRegisterRequest, IRegisterResponse } from './user.types';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  register(request: IRegisterRequest): Observable<IRegisterResponse> {
    return this.http.post<IRegisterResponse>(`/user/register`, request)
  }
}
