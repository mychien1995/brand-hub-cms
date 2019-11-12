import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment'; 

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

	private _config : Object;
	private _env : string;

  	constructor(private _httpClient: HttpClient) { }

  	load(){
  		return new Promise((resolve, reject) => {
            this._env = 'development';
            if (environment.production)
                this._env = 'production';
            this._http.get('./assets/config/' + this._env + '.json')
                .subscribe((data) => {
                    this._config = data;
                    resolve(true);
                },
                (error: any) => {
                    console.error(error);
                    return Observable.throw(error.json().error || 'Server error');
                });
        });
  	}

  	get(key: any){
  		if(!this._config) this.load();
  		return this._config[key];
  	}
}
