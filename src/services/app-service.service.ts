import { Injectable } from '@angular/core';
import { HttpParams, HttpClient } from "@angular/common/http";
import { Http, Response, Headers, Jsonp } from "@angular/http";
import { HttpClientService } from './HttpClientService';
import { environment } from "../environments/environment";
import { Observable } from "rxjs/Rx";
import { debug } from 'util';
@Injectable({
  providedIn: 'root'
})

export class AppServiceService {
  http: HttpClientService;
  constructor(private httpClient: HttpClientService) {
    this.http = httpClient;
  }

  PostNoToken(ControllerName: any, ActionName: any, Data: any) {
    return this.http.postnotoken(environment.apiUrl + 'api/' + ControllerName + '/' + ActionName, Data).map((response: Response) => {
      return response.text() != "" ? response.json() : null;
    });
  }

  Post(ControllerName: any, ActionName: any, Data: any) {
    return this.http.post(environment.apiUrl + 'api/' + ControllerName + '/' + ActionName, Data).map((response: Response) => {
      return response.text() != "" ? response.json() : null;
    });
  }

  async PostAsync(ControllerName: any, ActionName: any, Data: any) {
    return await this.http.postasync(environment.apiUrl + 'api/' + ControllerName + '/' + ActionName, Data);
    // .map((response: Response) => {
    //   return response.text() != "" ? response.json() : null;
    // });
  }

  Get(ControllerName: any, ActionName: any) {
    let url = environment.apiUrl + 'api/' + ControllerName + '/' + ActionName;
    return this.http.get(url).map((response: Response) => response.json());
  }

  async GetAsync(ControllerName: any, ActionName: any) {
    let url = environment.apiUrl + 'api/' + ControllerName + '/' + ActionName;
    return await this.http.getasync(url);
  }

  GetNoToken(ControllerName: any, ActionName: any) {
    let url = environment.apiUrl + 'api/' + ControllerName + '/' + ActionName;
    var httppost = this.http.getnotoken(url);
    return httppost.map((response: Response) => response.json());
  }

  async GetNoTokenAsync(ControllerName: any, ActionName: any) {
    let url = environment.apiUrl + 'api/' + ControllerName + '/' + ActionName;
    return await this.http.getnotokenasync(url);
    //return httppost.json();
    // return httppost.map((response: Response) => response.json());
  }

  GetById(ControllerName: any, ActionName: any, Id: any) {
    let url = environment.apiUrl + 'api/' + ControllerName + '/' + ActionName + '/' + Id;
    return this.http.get(url).map((response: Response) => response.json());
  }

  async GetByIdAsync(ControllerName: any, ActionName: any, Id: any) {
    let url = environment.apiUrl + 'api/' + ControllerName + '/' + ActionName + '/' + Id;
    return await this.http.getasync(url);
  }

  GetByIdNoToken(ControllerName: any, ActionName: any, Id: any) {
    let url = environment.apiUrl + 'api/' + ControllerName + '/' + ActionName + '?' + Id;
    var httppost = this.http.getnotoken(url);
    return httppost.map((response: Response) => response.json());    
  }

  async GetByIdNoTokenAsync(ControllerName: any, ActionName: any, Id: any) {
    let url = environment.apiUrl + 'api/' + ControllerName + '/' + ActionName + '?' + Id;
    return await this.http.getnotokenasync(url);
    //return httppost.map((response: Response) => response.json());
    //return httppost.json();
  }

  PostWithQueryString(ControllerName: any, ActionName: any, Data: any, queryStringParam: any) {
    return this.http.post(environment.apiUrl + 'api/' + ControllerName + '/' + ActionName + '?' + queryStringParam, Data).map((response: Response) => {
      return response.text() != "" ? response.json() : null;
    });
  }

  async PostWithQueryStringAsync(ControllerName: any, ActionName: any, Data: any, queryStringParam: any) {
    return await this.http.postasync(environment.apiUrl + 'api/' + ControllerName + '/' + ActionName + '?' + queryStringParam, Data);
  }

  PostNoTokenWithQueryString(ControllerName: any, ActionName: any, Data: any, queryStringParam: any) {
    return this.http.postnotoken(environment.apiUrl + 'api/' + ControllerName + '/' + ActionName + '?' + queryStringParam, Data).map((response: Response) => {
      return response.text() != "" ? response.json() : null;
    });
  }

  FileUpload(ControllerName: any, ActionName: any, Data: FormData) {    
    return this.http.file(environment.apiUrl + 'api/' + ControllerName + '/' + ActionName, Data, null).map((response: Response) => {
      return response.text() != "" ? response.json() : null;
    });
  }

}
