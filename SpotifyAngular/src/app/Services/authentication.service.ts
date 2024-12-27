import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import{GlobalVariables} from '../Global';
import { LoginModel } from '../Models/loginModel';
import { AuthResponseModel } from '../Models/authResponseModel';
import { RefreshTokenRequestModel } from '../Models/refreshTokenRequestModel';
import { RefreshTokenResponse } from '../Models/RefreshTokenResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http:HttpClient) { }
  ValidateUser(loginModel:LoginModel):Observable<AuthResponseModel>{
    return this.http.post<AuthResponseModel>(`${GlobalVariables.BASE_API_URL}/auth/Login`,loginModel);
  }
  RefreshToken(refreshTokenRequest:RefreshTokenRequestModel):Observable<RefreshTokenResponse>{
   return this.http.post<RefreshTokenResponse>(`${GlobalVariables.BASE_API_URL}/auth/RefreshToken`,refreshTokenRequest);
  }
}
