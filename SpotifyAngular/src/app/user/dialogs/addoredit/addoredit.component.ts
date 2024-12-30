import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {UserModel} from '../../../Models/userModel';
import{UserService} from '../../../Services/user.service';
import { NotificationModel } from '../../../Models/notificationModel';
@Component({
  selector: 'app-addoredit',
  templateUrl: './addoredit.component.html',
  styleUrls: ['./addoredit.component.css']
})
export class AddOrEditComponent implements OnInit { 
  userForm:FormGroup;
  constructor(public dialogRef: MatDialogRef<AddOrEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserModel,
    public dataService: UserService) { 

      this.userForm = new FormGroup({
        name:new FormControl(['',Validators.name]),
        email:new FormControl(['', [Validators.required, Validators.email]])
      });
    }   
    
    getNameErrorMessage() {
      return this.userForm.get('name').hasError('required')? 'Required field':'';   
    }
    email(){     
        return this.userForm.get('email');
    }
    
  ngOnInit() {
  }
  onNoClick(): void {   
    this.dialogRef.close(null);
  }
  public onSubmit(): void {
    if(this.data.id > 0){
      this.dataService.Update(this.data).subscribe((res:NotificationModel)=>{
        this.dialogRef.close(res);
      });
    }
    else{
    const data:UserModel={
      id:null,
      name:this.data.name,
      email:this.data.email,
      isActive:true,
      password:'12345678'     
    }

    this.dataService.Create(data).subscribe((res:NotificationModel)=>{
      this.dialogRef.close(res);
    });
    }
  }
}
