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
@NgModule({
  declarations: [
    AppComponent       
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,   
    ArtistModule,
    PlaylistModule,
    UserModule
  ],
  providers: [{
    provide:HTTP_INTERCEPTORS,useClass:httpInterceptor,multi:true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
