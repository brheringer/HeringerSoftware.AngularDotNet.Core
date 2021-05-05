
import {debounceTime} from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable ,  Subject ,  Subscription } from 'rxjs';



import { AlertService } from '../../local-services/alert.service';
import { UsersProfiles } from '../../model/users-profiles.model';
import { UserProfileService } from '../../services/user-profile.service';

@Component({
  selector: 'users-profiles-list',
  templateUrl: './users-profiles-list.component.html',
  styleUrls: ['../../css/metalsoft-core.css', './users-profiles-list.component.css'],
  providers: [UserProfileService]
})
export class UsersProfilesListComponent implements OnInit {

  public model = new UsersProfiles();
  public showFilters = true;
  public searchAsYouTypeSubject = new Subject();
  public searchAsYouTypeObservable = new Subscription();

  constructor(
    private service: UserProfileService,
    private alertService: AlertService,
    private router: Router) { }

  ngOnInit() {
    this.search();

    this.searchAsYouTypeObservable = this.searchAsYouTypeSubject.pipe(
      debounceTime(300))
      .subscribe(() => {
        this.search();
      })
  }

  searchAsYouType(): void {
    this.searchAsYouTypeSubject.next();
  }

  search(): void {
    this.service.search(this.model)
      .subscribe(dto => {
        if (dto.response.hasException) {
          this.alertService.error(dto.response.exception);
        }
        else {
          this.model.items = dto.items; //refresh
        }
      });
  }

  createNew(): void {
    this.router.navigateByUrl('/user-profile');
  }

  expand(id: number): void {
    this.router.navigateByUrl(`/user-profile/${id}`);
  }

}
