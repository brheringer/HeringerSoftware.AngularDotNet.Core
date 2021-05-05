import { Directive, forwardRef, Attribute } from '@angular/core';
import { Validator, AbstractControl, NG_VALIDATORS } from '@angular/forms';
@Directive({
  selector: '[validateEqual][formControlName],[validateEqual][formControl],[validateEqual][ngModel]',
  providers: [
    { provide: NG_VALIDATORS, useExisting: forwardRef(() => MetalsoftEqualValidator), multi: true }
  ]
})
export class MetalsoftEqualValidator implements Validator {

  //injects the attribute value via annotation @Attribute(‘validateEqual’) and assign it to the validateEqual variable
  constructor( @Attribute('validateEqual') public validateEqual: string) { }

  validate(c: AbstractControl): { [key: string]: any } {
    let givenControlValue = c.value;
    let theOtherControl = c.root.get(this.validateEqual);

    if (theOtherControl && givenControlValue !== theOtherControl.value) return {
      validateEqual: false
    }

    if (theOtherControl && givenControlValue === theOtherControl.value /*&& this.isReverse*/) {
      delete theOtherControl.errors['validateEqual'];
      if (!Object.keys(theOtherControl.errors).length)
        theOtherControl.setErrors(null);
    }

    if (theOtherControl && givenControlValue !== theOtherControl.value /*&& this.isReverse*/) {
      theOtherControl.setErrors({ validateEqual: false });
    }

    return null;
  }
}

//https://scotch.io/tutorials/how-to-implement-a-custom-validator-directive-confirm-password-in-angular-2
