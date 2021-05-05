import { Component, OnInit } from '@angular/core';
import { PasswordChange } from '../../model/password-change';
import { LoginService } from '../../services/login.service';
import { AlertService } from '../../local-services/alert.service';

@Component({
  selector: 'change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['../../css/metalsoft-core.css', './change-password.component.css'],
  providers: [LoginService]
})
export class ChangePasswordComponent implements OnInit {

  public model = new PasswordChange();

  constructor(
    private alertService: AlertService,
    private service: LoginService) { }

  ngOnInit() {
  }

  changePassword(): void {
    this.service.changePassword(this.model).subscribe(dto => {
      if (dto.response.hasException) {
        this.alertService.error(dto.response.exception);
      }
      else {
        //TODO podia ir para uma outra página
        this.alertService.success('Senha alterada com sucesso!');
        this.model = new PasswordChange();
      }
    }, err => { this.alertService.error(err); }); //teoricamente isso nao acontece
  }

}
