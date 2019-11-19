import { Component, OnInit } from '@angular/core';
import { ConfigService } from '../services/shared/config.service';
import { ClientService } from '../services/shared/client.service';

export class BaseComponent {

  constructor(protected _configService: ConfigService,protected  _clientService: ClientService) {

  }

}
