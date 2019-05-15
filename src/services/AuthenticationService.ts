import { Injectable, OnInit } from "@angular/core";
import { Http, Response, Headers, Jsonp } from "@angular/http";
import "rxjs/add/operator/map";
import { HttpParams } from "@angular/common/http";
import { environment } from "../environments/environment";
import { AppServiceService } from "./app-service.service";
import { Observable, observable } from "rxjs";
import 'rxjs/Rx';

@Injectable()
export class AuthenticationService implements OnInit {

    ngOnInit() { }

    constructor(private http: Http,  private _appService: AppServiceService) { }

    login(email: string, password: string) {

        let clientId = location.hostname;

        let headers = new Headers();
        headers.append("Content-Type", "application/x-www-form-urlencoded");
        let params = new HttpParams();
        params = params.append("userName", email);
        params = params.append("password", password);
        //params = params.append("clientId", clientId)
        params = params.append("grant_type", 'password');

        return this.http.post(environment.apiUrl + 'Token', params.toString(), { headers: headers })
            .map((response: Response) => {
                // login successful if there's a jwt token in the response
                let user = response.json();
                if (user && user.access_token) {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('TokenData', JSON.stringify(user));
                    localStorage.setItem('UserId', user.userId);
                    localStorage.setItem('userRole', user.userRole);
                    localStorage.setItem('access_token', user.access_token);
                    localStorage.setItem("UserName", user.userName);
                    localStorage.setItem("DisplayName", user.displayName);
                    localStorage.setItem("expires", user['.expires']);
                    localStorage.setItem("issued", user['.issued']);
                    localStorage.setItem("AutoGenerateCode", user['AutoGenerateCode']);
                    localStorage.setItem("Client", user['Client']);
                    localStorage.setItem("InstTrackRequired", user['InstTrackRequired']);
                    localStorage.setItem("IsMultipleCurrency", user['IsMultipleCurrency']);
                    localStorage.setItem("LocalCountry", user['LocalCountry']);
                    localStorage.setItem("OtpVerification", user['OtpVerification']);
                    localStorage.setItem("PriceRights", user['PriceRights']);
                    localStorage.setItem("SystemOTPEnable", user['SystemOTPEnable']);
                    localStorage.setItem("expires_in", user['expires_in']);
                    localStorage.setItem("token_type", user['token_type']);
                    var systemSettings = JSON.parse(user.SystemSettings);
                    localStorage.setItem("SystemSettings", systemSettings);
                    localStorage.setItem("Roles", user.userRole);
                }
            }).catch((err: Response) => {
                let details = err.json();
                return Observable.throw(details.error_description);
            });
    }

    GetRoleRights(): Observable<any> {
        return new Observable(obs => {
            var Roles = localStorage.getItem("Roles");
            Roles = JSON.parse(Roles);
            this._appService.PostNoToken("RoleRights", "GetRightByRole", Roles).subscribe(response => {
                localStorage.setItem('rights_assigned', JSON.stringify(response));
                obs.next();
            }, err => {
                obs.error();
            });
        });
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.clear();
    }
}