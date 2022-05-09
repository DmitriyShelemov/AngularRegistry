import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { catchError, Observable, of } from 'rxjs';
import { AreaService } from 'src/app/services/area.service';
import { ICountry, IProvince } from 'src/app/services/area.types';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  firstFormGroup!: FormGroup;
  secondFormGroup!: FormGroup;
  countries!: Observable<ICountry[]>;
  provinces!: Observable<IProvince[]>;
  selectedCountryValue!: number;
  completed!: boolean;
  error!: string;
  @ViewChild('stepper') private stepper!: MatStepper;

  constructor(
    private _formBuilder: FormBuilder,
    private _areaService: AreaService,
    private _userService: UserService) { }

  ngOnInit() {

    this.countries = this._areaService.getCountries();

    this.firstFormGroup = this._formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.pattern(/^(?=.*[A-Z])(?=.*\d)[^\s]{2,}$/)]],
      confirmPassword: ['', [Validators.required, Validators.pattern(/^(?=.*[A-Z])(?=.*\d)[^\s]{2,}$/)]],
      agreed: [false, Validators.requiredTrue]
    },
      {
        validators: this.checkPwdEquality
      });
    this.secondFormGroup = this._formBuilder.group({
      country: [null, Validators.required],
      province: [null, Validators.required]
    });
  }

  countryChanged(args: any) {
    this.provinces = this._areaService.getProvinces(this.selectedCountryValue)
    this.provinces.subscribe(data => {
      const hasNoData = data.length === 0
      if (hasNoData) {
        this.secondFormGroup.get('province')?.disable();
      } else {
        this.secondFormGroup.get('province')?.enable();
      }
    })
  }

  submit() {
    
    if (!this.firstFormGroup.valid 
      || !this.secondFormGroup.valid) {
        return;
      }

    this._userService.register({
      agreed: this.firstFormGroup.value.agreed,
      email: this.firstFormGroup.value.email,
      password: this.firstFormGroup.value.password,
      countryId: this.secondFormGroup.value.country,
      provinceId: this.secondFormGroup.value.province,
    })
      .pipe(
        catchError((error: Error) => of(error.message))
      )
      .subscribe(
        response => {
          if (typeof response === 'string') {
            this.error = response as string
          } else {
            this.completed = true
            this.stepper.next()
          }
        })
  }

  checkPwdEquality: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
    let pass = group.get('password')?.value;
    let confirmPass = group.get('confirmPassword')?.value
    return pass === confirmPass ? null : { notSame: true }
  }
}
