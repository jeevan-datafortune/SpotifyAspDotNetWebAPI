import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import {MatSnackBarModule} from '@angular/material/snack-bar'
import { httpInterceptor } from './httpInterceptor';
import { MatDividerModule, MatIconModule, MatMenuModule } from '@angular/material';
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
    MatSnackBarModule,
    MatMenuModule,    
    MatIconModule,
    MatDividerModule
  ],
  providers: [{
    provide:HTTP_INTERCEPTORS,useClass:httpInterceptor,multi:true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
