import { Component, OnInit } from '@angular/core';
import{PlaylistService} from '../../Services/playlist.service';
import { PlayListDataSource,PlaylistModel } from '../../Models/playListModel';
import { AddOrEditComponent } from '../dialogs/addoredit/addoredit.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material';
import { NotificationModel } from '../../Models/notificationModel';
import { GlobalVariables } from 'src/app/Global';
import { DeleteComponent } from '../dialogs/delete/delete.component';
@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  displayedColumns = ['position','name','description','songsCount','duration', 'actions'];
  dataSource:PlayListDataSource[]; 
  constructor(private playlist:PlaylistService,  public dialog: MatDialog,private snakbar:MatSnackBar) { }

  ngOnInit() {
    this.reload();
  }
 public reload(){
    this.playlist.getUserPlayLists(GlobalVariables.USER_ID).subscribe((playlists)=>{
      this.dataSource = playlists.map((item: PlaylistModel, i: number) => {
          return {
            position:i+1,
            id:item.id,
            name:item.name,
            description:item.description,
            image: (item.images!=null && item.images.length>0)?item.images[0].uri:'',
            duration:item.duration,
            songsCount:item.songsCount,
            userID:item.userID,
            isPublic:item.isPublic
          };
       });    
         
    });
  }
  addNew() {
      const playlist:PlaylistModel={ 
        id:0,
        name:'',
        description:'',
        userID:GlobalVariables.USER_ID
      };
      const dialogRef = this.dialog.open(AddOrEditComponent, { data: playlist });
      dialogRef.afterClosed().subscribe((result:NotificationModel) => this.handleMessageBox(result));
  }
 startEdit(data:PlaylistModel){
    const dialogRef = this.dialog.open(AddOrEditComponent, { data: data});
    dialogRef.afterClosed().subscribe((result:NotificationModel) => this.handleMessageBox(result));
  }
   deleteItem(data:PlaylistModel){
      const dialogRef = this.dialog.open(DeleteComponent, { data: data });
      dialogRef.afterClosed().subscribe((result:NotificationModel) => this.handleMessageBox(result));
    }
  handleMessageBox(result:NotificationModel){
    if(result){      
      if(result.successMessage!=null || result.errorMessage!=null){
        this.snakbar.open(result.successMessage||result.errorMessage,'Close');
        }
        if(result.successMessage!==null){
          this.reload();
      }
    }    
  }
}
