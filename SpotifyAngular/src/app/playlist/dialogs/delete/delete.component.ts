import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { NotificationModel } from '../../../Models/notificationModel';
import { PlaylistService } from '../../../Services/playlist.service';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css']
})
export class DeleteComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<DeleteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any, public dataService: PlaylistService) { }

  ngOnInit() {
  }
  onNoClick(): void {
    this.dialogRef.close(null);
  }

  confirmDelete(): void {
    this.dataService.delete(this.data.id).subscribe((res:NotificationModel)=>{
      console.log(res)
      this.dialogRef.close(res);
    });
  }
}
