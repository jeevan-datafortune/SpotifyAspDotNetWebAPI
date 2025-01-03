import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SongsRoutingModule } from './songs-routing.module';
import { ListComponent } from './list/list.component';
import { AddoreditComponent } from './dialogs/addoredit/addoredit.component';
import { DeleteComponent } from './dialogs/delete/delete.component';
import { MatDividerModule, MatIconModule, MatMenuModule } from '@angular/material';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatTableModule} from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { FormsModule } from '@angular/forms'; 
@NgModule({
  declarations: [ListComponent,AddoreditComponent,DeleteComponent],
  imports: [
    CommonModule,
    SongsRoutingModule,
    MatSnackBarModule,
    MatMenuModule,    
    MatIconModule,
    MatDividerModule ,
    MatTableModule,
    MatButtonModule,
    MatDialogModule,
    MatInputModule,
    MatToolbarModule,
    FormsModule,
    MatSelectModule,
    FormsModule
  ],
   entryComponents:[AddoreditComponent,DeleteComponent]
})
export class SongsModule { }
