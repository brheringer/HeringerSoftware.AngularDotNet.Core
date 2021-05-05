import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from 'projects/client-library/src/public_api';

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- true for debugging purposes only
    )
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
