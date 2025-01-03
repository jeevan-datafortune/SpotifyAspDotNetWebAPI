import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListComponent } from './list/list.component';

const routes: Routes = [ 
  {
     path:'', component:ListComponent,
     children:[
       {path:'list',component:ListComponent},
       { path: '',
         pathMatch: 'full',
         redirectTo: 'list'}
     ]
   }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SongsRoutingModule { }
