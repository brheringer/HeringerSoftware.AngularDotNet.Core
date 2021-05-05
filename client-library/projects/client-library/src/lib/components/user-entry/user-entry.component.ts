import { Observable ,  Subject } from 'rxjs';

import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import { AlertService } from '../../local-services/alert.service';
import { UserService } from '../../services/user.service';
import { User } from '../../model/user';
//import { UserProfile } from '../../model/user-profile.model';
import { UserProfileAssignment } from '../../model/user-profile-assignment.model';

@Component({
  selector: 'user-entry',
  templateUrl: './user-entry.component.html',
  styleUrls: ['../../css/metalsoft-core.css', './user-entry.component.css'],
  providers: [UserService]
})
export class UserEntryComponent implements OnInit {

  public model = new User();

  constructor(
    private service: UserService,
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
    }, err => { this.alertService.error(err); }); //teoricamente isso nao acontece
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
    this.model = new User();
  }

  removeItem(index: number): void {
    this.model.profilesAssignments[index].deleteMe = true;
  }

  newItem(): void {
    let profileAssignment: UserProfileAssignment = new UserProfileAssignment();
    //profileAssignment.assignedProfile = new UserProfile();
    this.model.profilesAssignments.push(profileAssignment);
  }

  search(): void {
    this.router.navigateByUrl('/users');
  }

  isPersistent(): boolean {
    return this.model.id > 0;
  }

}
