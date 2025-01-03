import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { httpInterceptor } from './httpInterceptor';
import { ArtistModule } from './artist/artist.module';
import { PlaylistModule } from './playlist/playlist.module';
import { UserModule } from './user/user.module';
import { SongsModule } from './songs/songs.module';
import { HomeComponent } from './home/home.component';
import {MatMenuModule} from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon'
import {MatDividerModule} from '@angular/material/divider';
import {MatSnackBarModule} from '@angular/material/snack-bar';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,   
    ArtistModule,
    PlaylistModule,
    UserModule,
    SongsModule,
    MatMenuModule,    
    MatIconModule,
    MatDividerModule,
    MatSnackBarModule,
  ],
  providers: [{
    provide:HTTP_INTERCEPTORS,useClass:httpInterceptor,multi:true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
