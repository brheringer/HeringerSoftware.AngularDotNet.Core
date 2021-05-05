import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule, registerLocaleData } from '@angular/common';
import { MetalsoftCoreModule } from '../../projects/client-library/src/lib/metalsoft-core.module';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import localePT from '@angular/common/locales/pt';
import { GenericService } from 'projects/client-library/src/lib/services/generic.service';
import { SessionService } from 'projects/client-library/src/lib/local-services/session.service';
import { AlertService } from 'projects/client-library/src/lib/local-services/alert.service';
import { environment } from '../environments/environment.prod';
registerLocaleData(localePT, 'pt-BR');

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    MetalsoftCoreModule.forRoot(environment)
  ],
  providers: [
    GenericService,
    SessionService,
    AlertService,
    { provide: LOCALE_ID, useValue: 'pt-BR' },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
