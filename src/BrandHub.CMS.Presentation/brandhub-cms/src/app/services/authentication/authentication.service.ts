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
export class AuthenticationService {
  
  	private BaseApiUrl : string;
  	private TokenStorageKey : string = 'app-token';

  	constructor(private _config: ConfigService, private _httpClient: HttpClient, private _localStorageService : LocalStorageService) {
   	}

   	login(user: LoginModel) : Observable<MessageModel>{
  		this.BaseApiUrl = this._config.get('BASE_API_URL');
   		var url = `${this.BaseApiUrl}authentication/token`;
   		var requestOptions : Object = {
   			responseType : 'text'
   		};
   		return this._httpClient.post(url, user, requestOptions)
  		.pipe(
			map(data => { return this.loginSuccess(data); }),
  			catchError(error => { return this.loginFailed(error); })
  	 	);
   	}

   	getToken() : string{
  		return this._localStorageService.get(this.TokenStorageKey);
  	}

  	isTokenPresent() : boolean{
	    return this._localStorageService.get(this.TokenStorageKey);
	}

   	private loginFailed(error : HttpErrorResponse){
   		var result : MessageModel = {
   			Message : error.error,
		  	IsSuccess : false
	  	};
	 	return of(result);
   	}

   	private loginSuccess(data : Object){
   		 this._localStorageService.set(this.TokenStorageKey, data);
   		 var result : MessageModel = {
		  	IsSuccess : true
		  };
	  	return result;
   	}

}
