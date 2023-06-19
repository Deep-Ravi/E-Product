import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const matchPasswordValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');
  
    return password?.value === confirmPassword?.value ? null : { notmatched: true };
  };

  export const patternValidator: ValidatorFn=(control: AbstractControl): ValidationErrors | null => {
    if (!control.value.password) {
      return null
    }
  
    let upperCaseCharacters = /[A-Z]+/g
    if (upperCaseCharacters.test(control.value.password) === false) {
      return { passwordStrength: `password has to contain Upper case characters`,invalidPassword:true };
    }
  
    let lowerCaseCharacters = /[a-z]+/g
    if (lowerCaseCharacters.test(control.value.password) === false) {
      return { passwordStrength: `password has to contain lower case characters`,invalidPassword:true };
    }
  
    let numberCharacters = /[0-9]+/g
    if (numberCharacters.test(control.value.password) === false) {
      return { passwordStrength: `password has to contain number characters`,invalidPassword:true };
    }
  
    let specialCharacters = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/
    if (specialCharacters.test(control.value.password) === false) {
      return { passwordStrength: `password has to contain special character`,invalidPassword:true };
    }
    return null;
    };