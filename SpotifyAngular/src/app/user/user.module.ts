import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRoutingModule } from './user-routing.module';
import { AddOrEditComponent } from './dialogs/addoredit/addoredit.component';
import { ListComponent } from './list/list.component';
import { MatDividerModule, MatIconModule, MatMenuModule } from '@angular/material';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatTableModule} from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import {  FormsModule } from '@angular/forms'; 


@NgModule({
  declarations: [AddOrEditComponent, ListComponent],
  imports: [
    CommonModule,
    UserRoutingModule,
    MatSnackBarModule,
    MatMenuModule,    
    MatIconModule,
    MatDividerModule ,
    MatTableModule,
    MatButtonModule,
    MatDialogModule,
    MatInputModule,
    MatToolbarModule,
    FormsModule  
  ],
  entryComponents: [AddOrEditComponent]
})
export class UserModule { }
