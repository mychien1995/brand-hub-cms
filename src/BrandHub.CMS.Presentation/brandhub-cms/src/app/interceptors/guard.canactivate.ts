import { Router, CanActivate } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthenticationService } from '../services/authentication/authentication.service';

@Injectable({
    providedIn: 'root'
})
export class Guard implements CanActivate {

    constructor(protected router: Router, protected authenticationService : AuthenticationService) {}

     canActivate() {
        if (this.authenticationService.isTokenPresent()) {
            return true;
        }
        this.router.navigate(['/login']);
        return false;
    }
}