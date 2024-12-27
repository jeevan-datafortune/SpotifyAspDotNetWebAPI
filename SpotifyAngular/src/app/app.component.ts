import { Component } from '@angular/core';
import { AuthenticationService } from './Services/authentication.service';
import { LoginModel } from './Models/loginModel';
import {MatSnackBar} from '@angular/material/snack-bar'
import { Router } from '@angular/router';
import { GlobalVariables } from './Global';
import { UserModel } from './Models/userModel';
import { UserService } from './Services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Spotify App';  
  isLoginHidden:boolean=true;
  searchString:string;
  email:string;
  password:string;
  userNameRequired:boolean=false;
  passwordrequired:boolean=false;
  userProfile:UserModel;
  constructor(private auth:AuthenticationService,private snackBar:MatSnackBar,private router:Router
    ,private user:UserService){
    if(!GlobalVariables.IsLoggedIn()){
      this.isLoginHidden=false;
      this.email='';
      this.password=null;
    }
    if(GlobalVariables.USER_ID>0){
      this.user.Get(GlobalVariables.USER_ID).subscribe((res)=>{
        this.userProfile=res;
      });
    }
  }
  validateUser(){   
    this.userNameRequired=this.email.length==0;
    this.passwordrequired=   this.password===null;
    if(!this.userNameRequired && !this.passwordrequired){
      const login:LoginModel={
        userName: this.email,
        password: this.password
      };
      this.auth.ValidateUser(login).subscribe((res)=>{
        if(res.isSuccess){
        localStorage.setItem('userId',res.user.id.toString());
        localStorage.setItem('userName',res.user.email);
        localStorage.setItem('token',res.token);
        localStorage.setItem('refreshToken',res.refreshToken);
        localStorage.setItem('expiresIn',res.expiresIn.toString());
        this.isLoginHidden=true;    
        this.userProfile=res.user;         
        }
        else{
          this.snackBar.open('Invalid user credentials','Close');
        }
      
      });
    }
  }
  onSearchKeyPress(event: any){   
    // this.search.Search(this.searchString).subscribe((res:SpotifyTrackSearchResponse)=>{
    //   this.router.navigate(['/search']);
    //   this.shared.UpdateSearchResponse(res);     
    // });
  }
  clearInput(){
    this.searchString='';
  }
  logOut(){
    localStorage.clear();
    window.location.reload();
  }
}
