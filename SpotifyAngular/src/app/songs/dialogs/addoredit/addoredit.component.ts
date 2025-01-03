import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { ArtistModel } from '../../../Models/artistModel';
import { NotificationModel } from '../../../Models/notificationModel';
import { SongModel } from '../../../Models/songModel';
import { ArtistService } from '../../../Services/artist.service';
import { SongService } from '../../../Services/song.service';

@Component({
  selector: 'app-addoredit',
  templateUrl: './addoredit.component.html',
  styleUrls: ['./addoredit.component.css']
})
export class AddoreditComponent implements OnInit {
  editForm: FormGroup;
  selectedFile: File | null = null;
  artists: ArtistModel[];
  constructor(public dialogRef: MatDialogRef<AddoreditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SongModel,
    public dataService: SongService, private artistService: ArtistService) {

    this.editForm = new FormGroup({
      name: new FormControl(['', Validators.name]),
      duration: new FormControl(['', [Validators.required]]),
      url: new FormControl(['', [Validators.required]])
    });
    this.artistService.GetAll().subscribe((res) => {
      this.artists = res;
    });
  }
  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }

  ngOnInit() {
  }
  onNoClick(): void {
    this.dialogRef.close(null);
  }
  public onSubmit(): void {  
    this.data.artists=this.data.selectedArtists.map((a)=>{return{id:a,name:''}});    
    if (this.data.id > 0) {     
      this.dataService.update(this.data).subscribe((res: NotificationModel) => {
        if (!this.selectedFile) {
          this.dialogRef.close(res);
        }
        else if (this.selectedFile && this.data.id != null) {
          this.onUpload(res);
        }
        else {
          this.dialogRef.close(res);
        }
      });
    }
    else {
      const data: SongModel = {
        id: null,
        name: this.data.name,
        duration: this.data.duration,
        uri: this.data.uri,
        artists: this.data.artists
      };
      
      this.dataService.Create(data).subscribe((res: NotificationModel) => {
        if (!this.selectedFile) {
          this.dialogRef.close(res);
        }
        else if (this.selectedFile && res.id != null) {
          this.data.id = res.id;
          this.onUpload(res);
        }
        else {
          this.dialogRef.close(res);
        }
      });
    }
  }
  onUpload(res: NotificationModel): void {
    if (this.selectedFile) {
      this.dataService.uploadImage(this.selectedFile, this.data.id).subscribe({
        next: (response) => {
          console.log('Image uploaded', response);
          this.dialogRef.close(res);
        },
        error: (err) => {
          console.error('File upload failed', err);
          res.successMessage = 'Song created without image.'
          this.dialogRef.close(res);
        }
      });
    }
  }
}
