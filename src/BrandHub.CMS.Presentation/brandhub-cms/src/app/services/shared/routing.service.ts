import { Injectable, APP_INITIALIZER } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RoutingService {

  constructor(private _activatedRoute: ActivatedRoute) {}

  getLatestRoute(): ActivatedRoute {
    let route = this._activatedRoute.firstChild;
    while (true) {
      if (route.firstChild)
        route = route.firstChild;
      else break;
    }
    return route;

  }
}
