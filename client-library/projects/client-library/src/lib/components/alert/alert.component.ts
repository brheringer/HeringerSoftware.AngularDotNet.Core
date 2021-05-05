import { Component, OnInit } from '@angular/core';

import { AlertService } from '../../local-services/alert.service';

@Component({
  //moduleId: module.id, //TODO pq isso? https://stackoverflow.com/questions/37178192/angular-what-is-the-meanings-of-module-id-in-component
  selector: 'alert',
  templateUrl: 'alert.component.html'
})
export class AlertComponent {
  message: any;

  constructor(private alertService: AlertService) { }

  ngOnInit() {
    this.alertService.getMessage().subscribe(message => { this.message = message; });
  }

}
