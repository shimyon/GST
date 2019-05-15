import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import 'rxjs/Rx';
@Injectable()
export class HttpClientService {

    constructor(private http: Http) { }

    createAuthorizationHeader(headers: Headers, notoken: boolean = false, ContentType: boolean = true) {
        if (localStorage.getItem("access_token") && notoken == false) {
            let tokenval = localStorage.getItem("access_token");
            const token: string = 'Bearer ' + tokenval;
            headers.append('Authorization', token);
        }
        if (ContentType) {
            headers.append('Content-Type', 'application/json; charset=utf-8');
        }
        // headers.append("Access-Control-Allow-Origin", "*");
        // headers.append("Access-Control-Allow-Credentials", "true");
        // headers.append("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
        // headers.append("TEST", "123");
        // headers.append("Access-Control-Allow-Headers", "Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers");
        // headers.append("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Authorization");

    }

    get(url) {
        const headers = new Headers();
        this.createAuthorizationHeader(headers);
        return this.http.get(url, {
            headers: headers
        });
    }

    async getasync(url) {
        const headers = new Headers();
        this.createAuthorizationHeader(headers);
        return await this.http.get(url, {
            headers: headers
        }).toPromise();
    }

    getnotoken(url) {
        const headers = new Headers();
        const notoken: boolean = true;
        this.createAuthorizationHeader(headers, notoken);
        return this.http.get(url, {
            headers: headers
        });    
    }    

    async getnotokenasync(url) {
        const headers = new Headers();
        const notoken: boolean = true;
        this.createAuthorizationHeader(headers, notoken);
        return await this.http.get(url, {
            headers: headers
        }).toPromise();
    }

    postnotoken(url, data) {
        const headers = new Headers();
        const notoken: boolean = true;
        this.createAuthorizationHeader(headers, notoken);
        return this.http.post(url, data, {
            headers: headers
        });
    }

    post(url, data) {
        const headers = new Headers();
        this.createAuthorizationHeader(headers);
        return this.http.post(url, data, {
            headers: headers
        });
    }

    async postasync(url, data) {
        const headers = new Headers();
        this.createAuthorizationHeader(headers);
        return await this.http.post(url, data, {
            headers: headers
        }).toPromise();
    }

    file(url, data, options) {
        const headers = new Headers();
        const notoken: boolean = true;
        this.createAuthorizationHeader(headers, notoken, false);
        return this.http.post(url, data, {
            headers: headers
        });
    }

}