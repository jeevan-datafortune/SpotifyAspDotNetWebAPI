import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { NotificationModel } from '../../../Models/notificationModel';
import { ArtistService } from '../../../Services/artist.service';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css']
})
export class DeleteComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<DeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, public dataService: ArtistService) { }

  ngOnInit() {
  }
  onNoClick(): void {
    this.dialogRef.close(null);
  }

  confirmDelete(): void {
    this.dataService.Delete(this.data.id).subscribe((res:NotificationModel)=>{
      this.dialogRef.close(res);
    });
  }
}
