import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import{GlobalVariables} from '../Global';
import { UserModel } from '../Models/userModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }
   Create(user:UserModel):Observable<UserModel>{
      return this.http.post<UserModel>(`${GlobalVariables.BASE_API_URL}/user/create`,user);
    }
    Update(user:UserModel):Observable<UserModel>{
      return this.http.put<UserModel>(`${GlobalVariables.BASE_API_URL}/user/update`,user);
    }
   
    Get(id:number):Observable<UserModel>{
      return this.http.get<UserModel>(`${GlobalVariables.BASE_API_URL}/user/Get/${id}`);
    }
    GetAll():Observable<UserModel[]>{
      return this.http.get<UserModel[]>(`${GlobalVariables.BASE_API_URL}/user/GetAll`);
    }
}
