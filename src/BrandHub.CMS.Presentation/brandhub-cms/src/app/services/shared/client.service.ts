import { Injectable } from '@angular/core';
import { ConfigService } from '../shared/config.service';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { LocalStorageService } from 'angular-2-local-storage';
import { LoginModel } from '../../models/authentication/login.model';
import { MessageModel } from '../../models/message.model';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  constructor(private _httpClient: HttpClient) {

  }

  get <T> (url: string): Observable <T> {
    var requestOptions: Object = {
      responseType: 'text'
    };
    return this._httpClient.get(url, requestOptions)
      .pipe(
        map(data => { return this.tryParse(data); })
      );
  }

  tryParse <T> (data: any): T {
  	var newObj : T;
  	newObj = data as T;
  	return newObj;
  }

}
