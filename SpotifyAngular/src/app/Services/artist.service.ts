import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import{GlobalVariables} from '../Global';
import { ArtistModel } from '../Models/artistModel';
import { NotificationModel } from '../Models/notificationModel';

@Injectable({
  providedIn: 'root'
})
export class ArtistService {

  constructor(private http:HttpClient) { }
  Create(artist:ArtistModel):Observable<NotificationModel>{
    return this.http.post<NotificationModel>(`${GlobalVariables.BASE_API_URL}/artist/create`,artist);
  }
  Update(artist:ArtistModel):Observable<NotificationModel>{
    return this.http.put<NotificationModel>(`${GlobalVariables.BASE_API_URL}/artist/update`,artist);
  }
  Delete(id:number):Observable<NotificationModel>{
    return this.http.delete<NotificationModel>(`${GlobalVariables.BASE_API_URL}/artist/Delete/${id}`);
  }
  Get(id:number):Observable<ArtistModel>{
    return this.http.get<ArtistModel>(`${GlobalVariables.BASE_API_URL}/artist/Get/${id}`);
  }
  GetAll():Observable<ArtistModel[]>{
    return this.http.get<ArtistModel[]>(`${GlobalVariables.BASE_API_URL}/artist/GetAll`);
  }
}
