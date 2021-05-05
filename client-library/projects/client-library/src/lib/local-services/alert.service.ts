import { Injectable } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Observable ,  Subject } from 'rxjs';

//fonte: https://www.pointblankdevelopment.com.au/blog/113/aspnet-core-20-angular-24-user-registration-and-login-tutorial-example
//rever e decidir se sera usada
@Injectable()
export class AlertService {
    private subject = new Subject<any>();
    private keepAfterNavigationChange = false;
 
    constructor(private router: Router) {
        // clear alert message on route change
        router.events.subscribe(event => {
            if (event instanceof NavigationStart) {
                if (this.keepAfterNavigationChange) {
                    // only keep for a single location change
                    this.keepAfterNavigationChange = false;
                } else {
                    // clear alert
                    this.subject.next();
                }
            }
        });
    }
 
    success(message: string, keepAfterNavigationChange = false) {
        this.keepAfterNavigationChange = keepAfterNavigationChange;
        this.subject.next({ type: 'success', text: `${new Date()} - ${message}` });
    }

    warning(message: string, keepAfterNavigationChange = false) {
        this.keepAfterNavigationChange = keepAfterNavigationChange;
        this.subject.next({ type: 'warning', text: `${new Date()} - ${message}` });
    }

    error(message: string, keepAfterNavigationChange = false) {
        this.keepAfterNavigationChange = keepAfterNavigationChange;
        this.subject.next({ type: 'error', text: `${new Date()} - ${message}` });
    }
 
    getMessage(): Observable<any> {
        return this.subject.asObservable();
    }
}
