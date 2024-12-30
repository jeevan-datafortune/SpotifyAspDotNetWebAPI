import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlayListRoutingModule } from './playlist-routing.module';
import { AddOrEditComponent } from '../playlist/dialogs/addoredit/addoredit.component';
import { DeleteComponent } from '../playlist/dialogs/delete/delete.component';
import { ListComponent } from '../playlist/list/list.component';
import { MatDividerModule, MatIconModule, MatMenuModule } from '@angular/material';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatTableModule} from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FormsModule } from '@angular/forms'; 



@NgModule({
  declarations: [AddOrEditComponent, DeleteComponent,ListComponent],
  imports: [
    CommonModule,
    PlayListRoutingModule,
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
  entryComponents:[AddOrEditComponent,DeleteComponent]
})
export class PlaylistModule { }
