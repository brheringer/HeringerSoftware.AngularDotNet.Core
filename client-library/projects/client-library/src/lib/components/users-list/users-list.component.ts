
import {debounceTime} from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable ,  Subject ,  Subscription } from 'rxjs';



import { AlertService } from '../../local-services/alert.service';
import { Users } from '../../model/users';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['../../css/metalsoft-core.css', './users-list.component.css'],
  providers: [UserService]
})
export class UsersListComponent implements OnInit {

  public model = new Users();
  public showFilters = true;
  public searchAsYouTypeSubject = new Subject();
  public searchAsYouTypeObservable = new Subscription(); //new Observable<string>();

  constructor(
    private service: UserService,
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
    this.router.navigateByUrl('/user');
  }

  expand(id: number): void {
    this.router.navigateByUrl(`/user/${id}`);
  }

}
