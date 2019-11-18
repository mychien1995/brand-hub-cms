import { Injectable, APP_INITIALIZER } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  private _config: Object;
  private _env: string;

  constructor(private _httpClient: HttpClient) {}

  load() {
  	this._env = 'development';
      if (environment.production)
        this._env = 'production';
    return this._httpClient.get('./assets/config/' + this._env + '.json')
      .toPromise()
      .then(data => {
        this._config = data;
      });
  }

  get(key: any) {
  	if(!this._config) return undefined;
    return this._config[key];
  }

  getBaseUrl(){
    return this.get('BASE_API_URL');
  }

  getEndpoint(url : string){
    return `${this.get('BASE_API_URL')}${url}`;
  }
}


export function ConfigServiceFactory(configService: ConfigService) {
  return () => configService.load();
}

export const ConfigServiceProvider = {
  provide: APP_INITIALIZER,
  useFactory: ConfigServiceFactory,
  deps: [ConfigService],
  multi: true
}
