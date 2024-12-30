import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddOrEditComponent } from './dialogs/addoredit/addoredit.component';
import { DeleteComponent } from './dialogs/delete/delete.component';
import { ListComponent } from './list/list.component';
import { ArtistRoutingModule } from './artist-routing.module';
import { MatDividerModule, MatIconModule, MatMenuModule } from '@angular/material';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatTableModule} from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FormsModule } from '@angular/forms'; 
@NgModule({
  declarations: [AddOrEditComponent, DeleteComponent, ListComponent],
  imports: [
    CommonModule,
    ArtistRoutingModule,
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
  entryComponents: [AddOrEditComponent,DeleteComponent]
})
export class ArtistModule { }
