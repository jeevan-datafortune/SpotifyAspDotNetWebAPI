import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


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
  {path:'',pathMatch:'full',redirectTo:'artist'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
