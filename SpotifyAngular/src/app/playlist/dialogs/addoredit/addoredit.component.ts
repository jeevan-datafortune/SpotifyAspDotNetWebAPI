import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {PlaylistModel} from '../../../Models/playListModel';
import{PlaylistService} from '../../../Services/playlist.service';
import { NotificationModel } from '../../../Models/notificationModel';
import { GlobalVariables } from 'src/app/Global';
@Component({
  selector: 'app-addoredit',
  templateUrl: './addoredit.component.html',
  styleUrls: ['./addoredit.component.css']
})
export class AddOrEditComponent implements OnInit { 
  editForm:FormGroup;
  constructor(public dialogRef: MatDialogRef<AddOrEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PlaylistModel,
    public dataService: PlaylistService) { 

      this.editForm = new FormGroup({
        name:new FormControl(['',Validators.name]),
        description:new FormControl(['', [Validators.required]])
      });
    }   
    
    getErrorMessage() {
      return this.editForm.get('name').hasError('required')? 'Required field':''
     ;   
    } 
    
  ngOnInit() {
  }
  onNoClick(): void {   
    this.dialogRef.close(null);
  }
  public onSubmit(): void {
    if(this.data.id > 0){
      this.dataService.update(this.data).subscribe((res:NotificationModel)=>{
        this.dialogRef.close(res);
      });
    }
    else{
    const data:PlaylistModel={
      id:null,
      name:this.data.name,
      description:this.data.description,
      isPublic:true,
      duration:0,
      images:[],
      songsCount:0,
      userID:GlobalVariables.USER_ID,
      owner:null
    }

    this.dataService.Create(data).subscribe((res:NotificationModel)=>{
      this.dialogRef.close(res);
    });
    }
  }
}
