import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';

import { LoginPage } from './login.page';
import { HttpClientModule } from '@angular/common/http';
import { CustomMaterialModule } from 'src/core/CustomMaterialModule';

const routes: Routes = [
  {
    path: '',
    component: LoginPage
  }
];

@NgModule({
  imports: [
    HttpModule,
    HttpClientModule ,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CustomMaterialModule,
    RouterModule.forChild(routes)
  ],
  declarations: [LoginPage]
})
export class LoginPageModule {}
