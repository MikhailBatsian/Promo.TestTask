import { Component, OnInit, ViewChild} from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { confirmPasswordValidator } from './confirm-password-validator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatStepper } from '@angular/material/stepper';
import { AccountService } from '../../services/account.service';
import { LocationService } from '../../services/location.service';

@Component({
  selector: 'app-registration-wizard',
  templateUrl: './registration-wizard.component.html',
  styleUrl: './registration-wizard.component.css'
})

export class RegistrationWizardComponent implements OnInit {
  firstFormGroup!: FormGroup;
  secondFormGroup!: FormGroup;
  
  @ViewChild(MatStepper) stepper!: MatStepper;

  countries: any[] = [];
  provinces: any[] = [];
  isNextClicked = false;
  isLinear = true;

  constructor(
    private _formBuilder: FormBuilder, 
    private snackBar: MatSnackBar,
    private _accountService: AccountService, 
    private _locationService: LocationService) {
  }

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.pattern('^(?=.*[a-zA-Z])(?=.*\\d).+$') ]],
      confirmPassword: ['', [Validators.required]],
      agree: [false, Validators.requiredTrue]});

      const passwordControl = this.firstFormGroup.get('password');
      if (passwordControl) {
        this.firstFormGroup.get('confirmPassword')?.setValidators([
          confirmPasswordValidator(passwordControl)
        ]);
      } 

    this.secondFormGroup = this._formBuilder.group({
      country: ['', Validators.required],
      province: ['', Validators.required]
    });

    this._locationService.getCountries().subscribe({
      next: (countries) =>{
        this.countries = countries;
      }
    });

    this.secondFormGroup.get('country')?.valueChanges.subscribe({
      next: (countryId) => {
        this._locationService.getProvinces(countryId).subscribe({
          next: (provinces) =>{
            this.provinces = provinces;
          }
        });
      }
    });
  }
  
  get email() { return this.firstFormGroup.get('email'); }
  get password() { return this.firstFormGroup.get('password'); }
  get confirmPassword() { return this.firstFormGroup.get('confirmPassword'); }
  get agree() { return this.firstFormGroup.get('agree'); }
  get country() { return this.secondFormGroup.get('country'); }
  get province() { return this.secondFormGroup.get('province'); }

  validateFirstForm() {
    this.isNextClicked = true;
    this.firstFormGroup.markAllAsTouched();
  }

  save() {
    this.firstFormGroup.markAllAsTouched();
    
    if (this.firstFormGroup.valid && this.secondFormGroup.valid) {
      if (this.firstFormGroup.valid) {
        const userData = {
          email: this.firstFormGroup.value.email,
          password: this.firstFormGroup.value.password,
          city: this.firstFormGroup.value.city,
          isAgreed: this.firstFormGroup.value.agree,
          provinceId: this.secondFormGroup.value.province
        };
  
        this._accountService.createUser(userData).subscribe({
          next: (r) => {
            this.snackBar.open(`User with email '${r.email}' successfully created!`, 'Close', {
              duration: 5000,
              verticalPosition: 'top',
              horizontalPosition: 'center'
            });
           
            this.stepper.selectedIndex = 0;
            this.resetForm(this.firstFormGroup);
            this.resetForm(this.secondFormGroup);
          },
          error: (e) => {
            const errorsMessages = Object.entries(e.error)
            .map(([key, value]) => value)
            .join(', ');
          
            this.snackBar.open(`Error creating user. ${errorsMessages}`, 'Close', {
              duration: 5000,
              verticalPosition: 'top',
              horizontalPosition: 'center'
            });
          }
        });
      }
    }
  }

  resetForm(formGroup: FormGroup) {
    let control: AbstractControl;
    formGroup.reset();
    formGroup.markAsUntouched();
    Object.keys(formGroup.controls).forEach((name) => {
      control = formGroup.controls[name];
      control.setErrors(null);
    });
  }
}
