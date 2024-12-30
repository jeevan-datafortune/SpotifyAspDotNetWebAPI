import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {FormControl, Validators} from '@angular/forms';
import {ArtistModel} from '../../../Models/artistModel';
import{ArtistService} from '../../../Services/artist.service';
import { NotificationModel } from '../../../Models/notificationModel';
@Component({
  selector: 'app-addoredit',
  templateUrl: './addoredit.component.html',
  styleUrls: ['./addoredit.component.css']
})
export class AddOrEditComponent implements OnInit { 
  constructor(public dialogRef: MatDialogRef<AddOrEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ArtistModel,
    public dataService: ArtistService) {
     
     }
    formControl = new FormControl('', [Validators.required ]);          
    getErrorMessage() {
      return this.formControl.hasError('required') ? 'Required field':'';
    }
  ngOnInit() {
  }
  onNoClick(): void {
    const res:ArtistModel={
      id:-1,
      name:''
    }
    this.dialogRef.close(res);
  }
  public onSubmit(): void {
    if(this.data.id > 0){
      this.dataService.Update(this.data).subscribe((res:NotificationModel)=>{
        this.dialogRef.close(res);
      });
    }
    else{
    const data:ArtistModel={
      id:null,
      name:this.data.name
    }
    this.dataService.Create(data).subscribe((res:NotificationModel)=>{
      this.dialogRef.close(res);
    });
    }
  }
}
