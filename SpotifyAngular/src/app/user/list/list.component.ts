import { Component, OnInit } from '@angular/core';
import{UserService} from '../../Services/user.service';
import { UserModel,UserDataSource } from '../../Models/userModel';
import { AddOrEditComponent } from '../dialogs/addoredit/addoredit.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material';
import { NotificationModel } from '../../Models/notificationModel';
@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  displayedColumns = ['position','name','email', 'actions'];
  dataSource:UserDataSource[]; 
  constructor(private user:UserService,  public dialog: MatDialog,private snakbar:MatSnackBar) { }

  ngOnInit() {
    this.reload();
  }
 public reload(){
    this.user.GetAll().subscribe((artists)=>{
      this.dataSource = artists.map((item: UserModel, i: number) => {
          return {position:i+1,...item};
       });       
    });
  }
  addNew() {
      const user:UserModel={ id:0, name:'',email:'',isActive:true,password:''};
      const dialogRef = this.dialog.open(AddOrEditComponent, { data: user });
      dialogRef.afterClosed().subscribe((result:NotificationModel) => this.handleMessageBox(result));
  }
 startEdit(data:UserModel){
    const dialogRef = this.dialog.open(AddOrEditComponent, { data: data});
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
