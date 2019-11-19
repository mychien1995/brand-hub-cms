import { Component, OnInit } from '@angular/core';
import { ConfigService } from '../services/shared/config.service';
import { ClientService } from '../services/shared/client.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AppInjector } from '../app.injector';

export class BaseComponent {

  constructor(protected _configService?: ConfigService,protected  _clientService?: ClientService, protected _router? : Router, protected _activatedRoute? : ActivatedRoute) {
    if(!_configService) this._configService = AppInjector.get(ConfigService);
    if(!_clientService) this._clientService = AppInjector.get(ClientService);
    if(!_router) this._router = AppInjector.get(Router);
    if(!_activatedRoute) this._activatedRoute = AppInjector.get(ActivatedRoute);
  }

}
