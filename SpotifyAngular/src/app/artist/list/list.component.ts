import { Component, OnInit } from '@angular/core';
import{ArtistService} from '../../Services/artist.service';
import { ArtistModel, ArtistTableDataSource } from 'src/app/Models/artistModel';
import { AddOrEditComponent } from '../dialogs/addoredit/addoredit.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material';
import { NotificationModel } from '../../Models/notificationModel';
import { DeleteComponent } from '../dialogs/delete/delete.component';
@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  displayedColumns = ['position','name', 'actions'];
  dataSource:ArtistTableDataSource[]; 
  constructor(private artist:ArtistService,  public dialog: MatDialog,private snakbar:MatSnackBar) { }

  ngOnInit() {
    this.reload();
  }
  public reload(){
    this.artist.GetAll().subscribe((artists)=>{
      this.dataSource = artists.map((item: ArtistModel, i: number) => {
          return {position:i+1,...item};
       });       
    });
  }
  addNew() {
    const artistData:ArtistModel={ id:0, name:'' };
    const dialogRef = this.dialog.open(AddOrEditComponent, { data: artistData });
    dialogRef.afterClosed().subscribe((result:NotificationModel) => this.handleMessageBox(result));
  }
  startEdit(data:ArtistModel){
    const dialogRef = this.dialog.open(AddOrEditComponent, { data: data});
    dialogRef.afterClosed().subscribe((result:NotificationModel) => this.handleMessageBox(result));
  }
  deleteItem(data:ArtistModel){
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
