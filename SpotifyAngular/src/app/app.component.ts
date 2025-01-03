import { Component } from '@angular/core';
import { AuthenticationService } from './Services/authentication.service';
import { LoginModel } from './Models/loginModel';
import { MatSnackBar } from '@angular/material/snack-bar'
import { Router } from '@angular/router';
import { GlobalVariables } from './Global';
import { UserModel } from './Models/userModel';
import { UserService } from './Services/user.service';
import { SongService } from './Services/song.service';
import { SongDataSource, SongModel } from './Models/songModel';
import { SharedService } from './Services/shared.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Spotify App';
  isLoginHidden: boolean = true;
  searchString: string;
  email: string;
  password: string;
  userNameRequired: boolean = false;
  passwordrequired: boolean = false;
  userProfile: UserModel;
  constructor(private auth: AuthenticationService, private snackBar: MatSnackBar, private router: Router
    , private user: UserService, private songs: SongService, private shared: SharedService) {
    if (!GlobalVariables.IsLoggedIn()) {
      this.isLoginHidden = false;
      this.email = '';
      this.password = null;
    }
    if (GlobalVariables.USER_ID > 0) {
      this.user.Get(GlobalVariables.USER_ID).subscribe((res) => {
        this.userProfile = res;
      });
    }
  }
  validateUser() {
    this.userNameRequired = this.email.length == 0;
    this.passwordrequired = this.password === null;
    if (!this.userNameRequired && !this.passwordrequired) {
      const login: LoginModel = {
        userName: this.email,
        password: this.password
      };
      this.auth.ValidateUser(login).subscribe((res) => {
        if (res.isSuccess) {
          localStorage.setItem('userId', res.user.id.toString());
          localStorage.setItem('userName', res.user.email);
          localStorage.setItem('token', res.token);
          localStorage.setItem('refreshToken', res.refreshToken);
          localStorage.setItem('expiresIn', res.expiresIn.toString());
          this.isLoginHidden = true;
          this.userProfile = res.user;
        }
        else {
          this.snackBar.open('Invalid user credentials', 'Close');
        }

      });
    }
  }
  onSearchKeyPress(event: any) {
    this.shared.UpdateFlag(true);
    this.songs.search(this.searchString).subscribe((res: SongModel[]) => {
      console.log(res)
      const songsData: SongDataSource[] = res.map((item) => {
        return {
          id: item.id,
          name: item.name,
          uri: item.uri,
          duration: item.duration,
          image: item.image,
          position: 0,
          artists: item.artists.map((a) => { return a.name }).join(','),
          songArtists: []
        }
      });
      this.shared.UpdateSongs(songsData);
      this.router.navigate(['/home']);

    });
  }
  clearInput() {
    this.searchString = '';
  }
  logOut() {
    localStorage.clear();
    window.location.reload();
  }
}
