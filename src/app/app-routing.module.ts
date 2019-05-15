import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../services/AuthGuard';
const routes: Routes = [
  { path: '', redirectTo:'login', pathMatch: 'full' },
  { path: 'login', loadChildren: './pages/login/login.module#LoginPageModule' },
  // { 'canActivate': [AuthGuard], path: 'register', loadChildren: './pages/register/register.module#RegisterPageModule' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
