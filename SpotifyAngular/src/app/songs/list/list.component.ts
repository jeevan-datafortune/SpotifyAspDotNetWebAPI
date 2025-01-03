import { Component, OnInit } from '@angular/core';
import { SongDataSource, SongModel } from '../../Models/songModel';
import { SongService } from '../../Services/song.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { GlobalVariables } from '../../Global';
import { AddoreditComponent } from '../dialogs/addoredit/addoredit.component';
import { NotificationModel } from '../../Models/notificationModel';
import { DeleteComponent } from '../dialogs/delete/delete.component';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  displayedColumns = ['position', 'photo', 'name', 'duration', 'uri','artists', 'actions'];
  dataSource: SongDataSource[];
  constructor(private songService: SongService, public dialog: MatDialog, private snakbar: MatSnackBar) { }

  ngOnInit() {
    this.reload();
  }
  public reload() {
    this.songService.getAll().subscribe((songs) => {
      this.dataSource = songs.map((item: SongModel, i: number) => {
        return {
          position: i + 1,
          id: item.id,
          name: item.name,
          image: item.image,
          duration: item.duration,
          uri: item.uri,
          artists: item.artists ? item.artists.map((item)=>{return item.name}).join(','):'',
          songArtists:item.artists
        };
      });

    });
  }

  addNew() {
    const song: SongModel = {
      id: 0,
      name: '',
      duration: 0,
      artists: [],
      selectedArtists:[],
      image:null,
      uri: ''
    };
    const dialogRef = this.dialog.open(AddoreditComponent, { data: song });
    dialogRef.afterClosed().subscribe((result: NotificationModel) => this.handleMessageBox(result));
  }
  startEdit(data: SongDataSource) {
    const dialogData:SongModel={
       id:data.id,
       name:data.name,
       uri:data.uri,
       duration:data.duration,
       image:data.image,
       selectedArtists: data.songArtists? data.songArtists.map((item)=>{return item.id}):[],
       artists:[]       
    };
    const dialogRef = this.dialog.open(AddoreditComponent, { data: dialogData });
    dialogRef.afterClosed().subscribe((result: NotificationModel) => this.handleMessageBox(result));
  }
  deleteItem(data: SongModel) {
    const dialogRef = this.dialog.open(DeleteComponent, { data: data });
    dialogRef.afterClosed().subscribe((result: NotificationModel) => this.handleMessageBox(result));
  }
  handleMessageBox(result: NotificationModel) {    
    if (result) {
      if (result.successMessage !== null || result.errorMessage !== null) {
        this.snakbar.open(result.successMessage || result.errorMessage, 'Close');
      }
      if (result.successMessage !== null) {
        this.reload();
      }
    }
  }
  getImageUrl(item: SongModel) {
    if (item.image != null) {
      return `${GlobalVariables.IMAGE_PATH}${item.image}`;
    }

    return './assets/music.jpg';
  }
}
