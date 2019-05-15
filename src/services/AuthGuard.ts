import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs/Rx";
@Injectable()
export class AuthGuard implements CanActivate {
    checkDataForDate: any = {}
    constructor(
        // public navCtrl: NavController,
        private _router: Router,
    ) {
    }

    handleError(error: any) {
        let errMsg = (error.message) ? error.message : error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        return Observable.throw(error);
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
        let UserId = localStorage.getItem("UserId");
        if (UserId) {
            return true;
        } else {
            this._router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
            // this.navCtrl.navigateRoot('/');
            return false;
        }
        // return this._userService.verify().map(
        //     data => {
        //         if (data !== null) {
        //             //logged in so return true
        //             return true;
        //         }
        //         //error when verify so redirect to login page with the return url
        //         this._router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
        //         return false;
        //     },
        //     error => {
        //         // error when verify so redirect to login page with the return url
        //         this._router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
        //         return false;
        //     }).catch((error: any) => {
        //         this._router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
        //         let errMsg = (error.message) ? error.message : error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        //         return Observable.throw(error);
        //     });
    }




}
