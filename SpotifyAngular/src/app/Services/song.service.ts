import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import{GlobalVariables} from '../Global';
import { SongModel } from '../Models/songModel';
import { PlaylistSongModel } from '../Models/playlistSongModel';

@Injectable({
  providedIn: 'root'
})
export class SongService {

  constructor(private http:HttpClient) { }
   Create(playlist:SongModel):Observable<SongModel>{
      return this.http.post<SongModel>(`${GlobalVariables.BASE_API_URL}/songs/Create`,playlist);
    }
    update(playlist:SongModel):Observable<SongModel>{
      return this.http.put<SongModel>(`${GlobalVariables.BASE_API_URL}/songs/Update`,playlist);
    }
    delete(id:number):Observable<any>{
      return this.http.delete<any>(`${GlobalVariables.BASE_API_URL}/songs/Delete/${id}`);
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
}
