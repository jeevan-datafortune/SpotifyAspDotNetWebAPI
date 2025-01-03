import { Component, OnInit } from '@angular/core';
import { SongDataSource } from '../Models/songModel';
import { PlaylistModel } from '../Models/playListModel';
import { PlaylistService } from '../Services/playlist.service';
import { SongService } from '../Services/song.service';
import { SharedService } from '../Services/shared.service'
import { GlobalVariables } from '../Global';
import { PlaylistSongModel } from '../Models/playlistSongModel';
import { MatSnackBar } from '@angular/material';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  searchResponse: SongDataSource[];
  userPlayLists: PlaylistModel[];
  isFromSearch: boolean;
  selectedPlayList:PlaylistModel;
  constructor(private playlistService: PlaylistService, private songService: SongService,
    private shared: SharedService, private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.shared.songData.subscribe((songs) => { this.searchResponse = songs; });
    this.shared.isFromSearchData.subscribe((flag) => { this.isFromSearch = flag; });
    this.playlistService.getUserPlayLists(GlobalVariables.USER_ID).subscribe((playlists) => {
      console.log(playlists)
      this.userPlayLists = playlists;
    });
  }
  playListIcon(data: PlaylistModel): string {
    if (data.image) {
      return `${GlobalVariables.IMAGE_PATH}${data.image}`
    }
    return './assets/music.jpg';
  }
  songIcon(data: SongDataSource): string {
    if (data.image) {
      return `${GlobalVariables.IMAGE_PATH}${data.image}`
    }
    return './assets/music.jpg';
  }
  loadPlayListItems(item: PlaylistModel) {
    this.shared.UpdateFlag(false);
    this.selectedPlayList=item;
    this.songService.getByPlayList(item.id).subscribe((songs) => {
      const songsData: SongDataSource[] = songs.map((item) => {
        return {
          id: item.id,
          name: item.name,
          uri: item.uri,
          duration: item.duration,
          image: item.image,
          position: 0,
          artists: item.artists.map((a) => { return a.name }).join(','),
          songArtists: []
        }
      });
      this.shared.UpdateSongs(songsData);
    });

  }
  formatDuration(ms: number): string {
    const minutes = Math.floor(ms / 60000);
    const seconds = ((ms % 60000) / 1000).toFixed(0);
    return `${minutes}:${parseInt(seconds) < 10 ? '0' : ''}${seconds}`;
  }
  addToPlayList(p, item) {
    const req: PlaylistSongModel = {
      playListId: p.id,
      songId: item.id
    };
    this.songService.addToPlayList(req).subscribe((res) => {
      if (res && res == true) {
        this.snackBar.open('Song added to playlist', 'Close')
      }
    });
  }
  onRemoveFromPlaylist(item) {
    if (confirm(`Do you really wish to remove song ${item.name} from playlist ${this.selectedPlayList.name}?`)) {
      const req: PlaylistSongModel = {
        playListId: this.selectedPlayList.id,
        songId: item.id
      };
      this.songService.removeFromPlayList(req).subscribe((res) => {
        if (res && res == true) {
          this.snackBar.open('Song removed from playlist', 'Close');
          this.loadPlayListItems(this.selectedPlayList);
        }
      });
    }
  }
}
