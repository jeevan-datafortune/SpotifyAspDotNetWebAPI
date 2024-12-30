import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import{GlobalVariables} from '../Global';
import { PlaylistModel } from '../Models/playListModel';
import { NotificationModel } from '../Models/notificationModel';

@Injectable({
  providedIn: 'root'
})
export class PlaylistService {

  constructor(private http:HttpClient) { }
  Create(playlist:PlaylistModel):Observable<NotificationModel>{
    return this.http.post<NotificationModel>(`${GlobalVariables.BASE_API_URL}/playlist/Create`,playlist);
  }
  update(playlist:PlaylistModel):Observable<NotificationModel>{
    return this.http.put<NotificationModel>(`${GlobalVariables.BASE_API_URL}/playlist/update`,playlist);
  }
  delete(id:number):Observable<NotificationModel>{
    return this.http.delete<NotificationModel>(`${GlobalVariables.BASE_API_URL}/playlist/delete/${id}`);
  }
  get(id:number):Observable<PlaylistModel>{
    return this.http.get<PlaylistModel>(`${GlobalVariables.BASE_API_URL}/playlist/get/${id}`);
  }
  getUserPlayLists(userId:number):Observable<PlaylistModel>{
    return this.http.get<PlaylistModel>(`${GlobalVariables.BASE_API_URL}/playlist/GetUserPlayLists/${userId}`);
  }
}
