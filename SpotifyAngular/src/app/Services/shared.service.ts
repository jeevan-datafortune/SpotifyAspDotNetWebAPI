import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { SongDataSource } from '../Models/songModel';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private songs = new BehaviorSubject<SongDataSource[]|null>(null);  // Initial empty list
  private isFromSearch=new BehaviorSubject<boolean|null>(null);
  public songData = this.songs.asObservable(); 
  public isFromSearchData=this.isFromSearch.asObservable();
  constructor() { } 
  public UpdateFlag(data:boolean){
    this.isFromSearch.next(data);
  }
  public UpdateSongs(data:SongDataSource[]){
     this.songs.next(data);
  }
}
