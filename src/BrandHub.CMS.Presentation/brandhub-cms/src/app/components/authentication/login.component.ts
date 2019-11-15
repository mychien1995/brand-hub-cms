import { Component, OnInit } from '@angular/core';
import { LoginModel } from '../../models/authentication/login.model';
import { MessageModel } from '../../models/message.model';
import { AuthenticationService } from '../../services/authentication/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private model : LoginModel;

  constructor(private _authenticationService : AuthenticationService, private _router: Router) { 
  	this.model = new LoginModel();
  }

  ngOnInit() {
    $('body').addClass('login');
  }

  login(){
  	this.model.Hostname = window.location.host;
  	this._authenticationService.login(this.model).subscribe(data => {
  		if(data.IsSuccess){
  			this._router.navigate(['/']);
  		}
  		else{
  			alert(data.Message);
  		}
  	});
  }



}
