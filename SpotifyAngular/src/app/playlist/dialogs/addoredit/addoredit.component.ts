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
  selectedFile: File | null = null;
  constructor(public dialogRef: MatDialogRef<AddOrEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PlaylistModel,
    public dataService: PlaylistService) { 

      this.editForm = new FormGroup({
        name:new FormControl(['',Validators.name]),
        description:new FormControl(['', [Validators.required]])
      });
    }   
    
    getErrorMessage() {
      return this.editForm.get('name').hasError('required')? 'Required field':'';     
    } 
    onFileSelected(event: any): void {
      this.selectedFile = event.target.files[0];
    }
  ngOnInit() {
  }
  onNoClick(): void {   
    this.dialogRef.close(null);
  }
  public onSubmit(): void {
    if(this.data.id > 0){
      this.dataService.update(this.data).subscribe((res:NotificationModel)=>{
        if(!this.selectedFile){
          this.dialogRef.close(res);
        }
        else if(this.selectedFile && this.data.id!=null){         
          this.onUpload(res);
        }     
        else{
          this.dialogRef.close(res);
        }
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
      owner:null,
      image:null
    }

    this.dataService.Create(data).subscribe((res:NotificationModel)=>{
      if(!this.selectedFile){
         this.dialogRef.close(res);
      }
      else if(this.selectedFile && res.id!=null){
        this.data.id=res.id;
        this.onUpload(res);
      }     
      else{
        this.dialogRef.close(res);
      }
    });
    }
  }
  onUpload(res:NotificationModel): void {
    if (this.selectedFile) {
      this.dataService.uploadImage(this.selectedFile,this.data.id).subscribe({
        next: (response) => {          
          console.log('Image uploaded', response);
          this.dialogRef.close(res);
        },
        error: (err) => {      
          console.error('File upload failed', err);   
          res.successMessage='Playlist created without image.'      
          this.dialogRef.close(res);
        }
      });
    } 
  }
}
