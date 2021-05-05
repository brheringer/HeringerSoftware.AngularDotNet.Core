import { NgModule, ModuleWithProviders } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';

import { AlertComponent } from './components/alert/alert.component';
import { BooleanFieldComponent } from './components/boolean-field/boolean-field.component';
import { DateFieldComponent } from './components/date-field/date-field.component';
import { NumberFieldComponent } from './components/number-field/number-field.component';
import { SelectEntityFieldComponent } from './components/select-entity-field/select-entity-field.component';
import { SelectPrimitiveFieldComponent } from './components/select-primitive-field/select-primitive-field.component';
import { SmartSearchFieldComponent } from './components/smart-search-field/smart-search-field.component';
import { TextAreaFieldComponent } from './components/text-area-field/text-area-field.component';
import { TextFieldComponent } from './components/text-field/text-field.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { LoginComponent } from './components/login/login.component';
import { RichLabelComponent } from './components/rich-label/rich-label.component';
import { UserEntryComponent } from './components/user-entry/user-entry.component';
import { UserProfileEntryComponent } from './components/user-profile-entry/user-profile-entry.component';
import { UsersListComponent } from './components/users-list/users-list.component';
import { UsersProfilesListComponent } from './components/users-profiles-list/users-profiles-list.component';
import { MetalsoftEqualValidator } from './validators/equal-validator.directive';
import { MetalsoftBooleanFormatPipe } from './pipes/boolean-pipe';
import { MetalsoftDateFormatPipe } from './pipes/date-pipe';
import { GenericService } from './services/generic.service';
import { LoginService } from './services/login.service';
import { MetalsoftHttpInterceptor } from './services/metalsoft-http-interceptor';

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    HttpClientModule,
    NgbModule.forRoot(),
    NgxSpinnerModule
  ],
  declarations: [
    AlertComponent,
    BooleanFieldComponent, 
    DateFieldComponent, 
    NumberFieldComponent, 
    SelectEntityFieldComponent, 
    SelectPrimitiveFieldComponent, 
    SmartSearchFieldComponent, 
    TextAreaFieldComponent, 
    TextFieldComponent, 
    ChangePasswordComponent, 
    LoginComponent, 
    RichLabelComponent, 
    UserEntryComponent, 
    UserProfileEntryComponent, 
    UsersListComponent, 
    UsersProfilesListComponent,
    MetalsoftEqualValidator, 
    MetalsoftBooleanFormatPipe, 
    MetalsoftDateFormatPipe
  ],
  exports: [
    AlertComponent, 
    BooleanFieldComponent, 
    DateFieldComponent, 
    NumberFieldComponent, 
    SelectEntityFieldComponent, 
    SelectPrimitiveFieldComponent, 
    SmartSearchFieldComponent, 
    TextAreaFieldComponent, 
    TextFieldComponent, 
    ChangePasswordComponent, 
    LoginComponent, 
    RichLabelComponent, 
    UserEntryComponent, 
    UserProfileEntryComponent, 
    UsersListComponent, 
    UsersProfilesListComponent, 
    MetalsoftEqualValidator, 
    MetalsoftBooleanFormatPipe, 
    MetalsoftDateFormatPipe
  ],
  providers: [
    GenericService,
    LoginService,
    NgxSpinnerService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MetalsoftHttpInterceptor,
      multi: true,
    }
  ]
})
export class MetalsoftCoreModule { 

  //https://stackoverflow.com/questions/43529920/passing-environment-variables-to-angular2-library
  public static forRoot(environment: any): ModuleWithProviders {

    return {
        ngModule: MetalsoftCoreModule,
        providers: [
            {
                provide: 'environmentProvider', // you can also use InjectionToken
                useValue: environment
            }
        ]
    };
  }
}
