import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NgxSpinnerService } from 'ngx-spinner';
import { AlertService } from '../../local-services/alert.service';
import { LoginService } from '../../services/login.service'
import { SessionService } from '../../local-services/session.service'

import { User } from '../../model/user';
import { UserSession } from '../../model/user-session';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['../../css/metalsoft-core.css', './login.component.css']
})
export class LoginComponent implements OnInit {

  model: User = new User();
  returnUrl: string = null;
  isLoggedIn: boolean = false;
  showSpinner: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private alertService: AlertService,
    private loginService: LoginService,
    private sessionService: SessionService) { }

  ngOnInit() {
    //this.authenticationService.logout(); // reset login status

    this.route.queryParams.subscribe(params => this.returnUrl = params['returnUrl']);

    this.isLoggedIn = this.sessionService.hasCurrentSession();
    if (this.isLoggedIn)
      this.model.userName = this.sessionService.getCurrentSession().userName;
  }

  doLogin() {
    this.showSpinner = true;
    this.loginService.login(this.model).subscribe(
      dto => {
        if (dto.userSessionToken)
        {
          this.isLoggedIn = true;
          
          if(this.returnUrl)
            this.router.navigate([this.returnUrl]);
          else
            this.router.navigate(['/']);
        }
        else {
          console.log(dto);
          this.alertService.error('missing userSessionToken');
        }
      },
      err => {
        console.log(err);
        let msg = err.error.error_description;
        this.alertService.error(msg);
        this.showSpinner = false;
      },
      () => this.showSpinner = false);
  }

  doLogout() {
    this.loginService.logout();
    this.isLoggedIn = false;
    //TODO
  }

}
