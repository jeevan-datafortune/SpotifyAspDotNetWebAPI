import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  {
    path: 'artist',
    loadChildren: () =>
    import('./artist/artist.module').then((a) => a.ArtistModule),
  } ,
  {
    path: 'user',
    loadChildren: () =>
    import('./user/user.module').then((a) => a.UserModule),
  } ,
  {
    path: 'playlist',
    loadChildren: () =>
    import('./playlist/playlist.module').then((a) => a.PlaylistModule),
  } ,
  {
    path: 'songs',
    loadChildren: () =>
    import('./songs/songs.module').then((a) => a.SongsModule),
  } ,
  {path:'home',component:HomeComponent},
  {path:'',pathMatch:'full',redirectTo:'home'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
