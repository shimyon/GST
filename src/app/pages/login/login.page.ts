import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../../../services/AuthenticationService';
import { HttpClientService } from '../../../services/HttpClientService';
import { async } from 'q';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { MatSnackBar } from '@angular/material';
import {MatCardModule} from '@angular/material/card';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
  providers: [AuthenticationService, HttpClientService]
})
export class LoginPage implements OnInit {
  username: string;
  password: string;
  showSpinner: boolean = false;
  public onLoginForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private _router: Router,
    private authe: AuthenticationService,
    private snackBar: MatSnackBar
  ) { }

  ionViewWillEnter() {
    // this.menuCtrl.enable(false);
  }

  ngOnInit() {

    // this.onLoginForm = this.formBuilder.group({
    //   'email': [null, Validators.compose([
    //     Validators.required
    //   ])],
    //   'password': [null, Validators.compose([
    //     Validators.required
    //   ])]
    // });
  }

  // // //
  goToRegister() {
    this._router.navigate(['/register']);
  }

  Signin() {

    // let email = this.onLoginForm.get('email').value;
    // let pass = this.onLoginForm.get('password').value;
    // this.authe.login(email, pass).subscribe(s => {
    //   this.authe.GetRoleRights().subscribe(f => {
    //     this.goToHome()
    //   },
    //     async err => {
    //       this.DisplayMessage(err);
    //     }
    //   );
    // }, async err => {
    //   this.DisplayMessage(err);
    // });
  }


  private async DisplayMessage(err: any) {
    this.snackBar.open(err);
    // const toast = await this.toastCtrl.create({
    //   showCloseButton: true,
    //   message: err,
    //   duration: 3000,
    //   position: 'middle'
    // });
    // toast.present();
  }

  goToHome() {
    // this.navCtrl.navigateRoot('/home-results');    
    this._router.navigate(['/dashboard']);
  }

}
