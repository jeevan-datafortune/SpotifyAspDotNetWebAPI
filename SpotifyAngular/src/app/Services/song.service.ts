import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import{GlobalVariables} from '../Global';
import { SongModel } from '../Models/songModel';
import { PlaylistSongModel } from '../Models/playlistSongModel';
import { NotificationModel } from '../Models/notificationModel';

@Injectable({
  providedIn: 'root'
})
export class SongService {

  constructor(private http:HttpClient) { }
   Create(playlist:SongModel):Observable<NotificationModel>{
      return this.http.post<NotificationModel>(`${GlobalVariables.BASE_API_URL}/songs/Create`,playlist);
    }
    update(playlist:SongModel):Observable<NotificationModel>{
      return this.http.put<NotificationModel>(`${GlobalVariables.BASE_API_URL}/songs/Update`,playlist);
    }
    delete(id:number):Observable<NotificationModel>{
      return this.http.delete<NotificationModel>(`${GlobalVariables.BASE_API_URL}/songs/Delete/${id}`);
    }
    get(id:number):Observable<SongModel>{
      return this.http.get<SongModel>(`${GlobalVariables.BASE_API_URL}/songs/Get/${id}`);
    }
    getAll():Observable<SongModel[]>{
      return this.http.get<SongModel[]>(`${GlobalVariables.BASE_API_URL}/songs/GetAll`);
    }
    getByPlayList(playlistId:number):Observable<SongModel[]>{
      return this.http.get<SongModel[]>(`${GlobalVariables.BASE_API_URL}/songs/GetSongsByPlayList/${playlistId}`);
    }
    addToPlayList(req:PlaylistSongModel):Observable<any>{
     return this.http.post<any>(`${GlobalVariables.BASE_API_URL}/songs/AddToPlayList`,req);
    }
    removeFromPlayList(req:PlaylistSongModel):Observable<any>{
      return this.http.post<any>(`${GlobalVariables.BASE_API_URL}/songs/RemoveFromPlayList`,req);
    }
    uploadImage(file: File,id:number):Observable<any>{
      const formData: FormData = new FormData();
      formData.append('file', file, file.name);
      return this.http.post(`${GlobalVariables.BASE_API_URL}/songs/Uplaod/${id}`, formData);
    }
}
