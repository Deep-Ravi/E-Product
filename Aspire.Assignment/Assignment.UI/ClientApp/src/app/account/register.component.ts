import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';
import { AlertService } from '../_services/alert.service';
import { matchPasswordValidator, patternValidator } from '../validator/matchPasswordValidator';
import { environment } from 'src/environments/environment';


@Component({ templateUrl: 'register.component.html' })
export class RegisterComponent implements OnInit {
    form!: FormGroup;
    loading = false;
    submitted = false;
    encryptUser="";
    name="";
    email="";
    isShow=true;
    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService
    ) { }

    ngOnInit() {
        this.isShow=true;
        this.encryptUser = this.route.snapshot.queryParams.userId || null;
        if(this.encryptUser!=null){
            this.isShow=false;
            this.accountService.getUserDetails(encodeURIComponent(this.encryptUser)).subscribe({
                next: (data : any) => {
                    console.log('data',data)
                    this.form = this.formBuilder.group({
                        username: data.username,
                        email:data.email,
                        password: ['', [Validators.required, Validators.minLength(9)]],
                        confirmPassword: ['', [Validators.required]],
                        id: data.id,
                        roleId: data.roleId,
                        operationId: data.operationId
                    },
                    { validators: [matchPasswordValidator,patternValidator] });
                },
                error: (error: any) => {
                    this.alertService.error(error, { keepAfterRouteChange: true });
                    this.router.navigate(['../login'], { relativeTo: this.route });
                }
            });
        } 
        else{
            this.form = this.formBuilder.group({
                username: ['', Validators.required],
                email:['', Validators.required],
                password: ['', [Validators.required, Validators.minLength(9)]],
                confirmPassword: ['', [Validators.required]],
                id:0,
                roleId: environment.defaultRoleId,
                operations: [environment.defaultOperations]
            },
            { validators: [matchPasswordValidator,patternValidator] });
        }               
    }
    // convenience getter for easy access to form fields
    get f() { return this.form.controls; }
    get e() {  return this.form.errors;}
    onSubmit() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.form.invalid) {
            return;
        }

        this.loading = true;
        console.log('this.form.value => ',this.form.value)
        this.accountService.register(this.form.value)
            .pipe(first())
            .subscribe({
                next: () => {
                    this.isShow=true;
                    this.alertService.success('Registration successful', { keepAfterRouteChange: true });
                    this.router.navigate(['../login'], { relativeTo: this.route });
                },
                error: error => {
                    this.alertService.error(error);
                    this.loading = false;
                }
            });
    }
}