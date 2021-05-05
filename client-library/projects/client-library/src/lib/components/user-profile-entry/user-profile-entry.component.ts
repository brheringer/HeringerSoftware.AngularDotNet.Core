import { Observable ,  Subject } from 'rxjs';

import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import { AlertService } from '../../local-services/alert.service';
import { UserProfileService } from '../../services/user-profile.service';
import { UserProfile } from '../../model/user-profile.model';

@Component({
  selector: 'user-profile-entry',
  templateUrl: './user-profile-entry.component.html',
  styleUrls: ['../../css/metalsoft-core.css', './user-profile-entry.component.css'],
  providers: [UserProfileService]
})
export class UserProfileEntryComponent implements OnInit {

  public model = new UserProfile();

  constructor(
    private service: UserProfileService,
    private alertService: AlertService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    let id = Number(this.route.snapshot.paramMap.get('id'));
    if (id != NaN && id > 0)
      this.load(id);
  }

  load(id: number): void {
    this.service.get(id).subscribe(dto => {
      if (dto.response.hasException) {
        this.alertService.error(dto.response.exception);
      }
      else {
        this.model = dto;
      }
    });
  }

  update(): void {
    this.service.update(this.model).subscribe(dto => {
      if (dto.response.hasException) {
        this.alertService.error(dto.response.exception);
      }
      else {
        this.alertService.success('Update User ok!');
        this.model = dto; //refresh
      }
    });
  }

  delete(): void {
    this.service.delete(this.model.id).subscribe(dto => {
      if (dto.response.hasException) {
        this.alertService.error(dto.response.exception);
      }
      else {
        this.alertService.success('Delete User ok!');
        this.model = dto; //refresh
      }
    });
  }

  createNew(): void {
    this.model = new UserProfile();
  }

  search(): void {
    this.router.navigateByUrl('/users-profiles');
  }

  isPersistent(): boolean {
    return this.model.id > 0;
  }

}
