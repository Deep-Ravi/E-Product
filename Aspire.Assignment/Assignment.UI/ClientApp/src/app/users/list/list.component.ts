import { Component, OnInit,OnDestroy } from '@angular/core';
import { AddEvent, CancelEvent, EditEvent, GridComponent, RemoveEvent, SaveEvent } from '@progress/kendo-angular-grid';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AlertService } from 'src/app/_services/alert.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/_services/user.service';
import { Role, User } from 'src/app/_models/user';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit, OnDestroy{
  public pageSize = 5;
  public buttonCount = 2;
  public sizes = [10,20,30];
  public operations=["ADD","EDIT","VIEW","DELETE"];
  users: User[] = [];
  roles:Role[]=[];
  user!:User;
  public formGroup?: FormGroup;
  private editedRowIndex?: number;
  subscription!:Subscription;
  constructor(private userService: UserService, private alertService: AlertService, private router: Router) {

  }

  ngOnInit() {
    this.subscription=this.userService.getAll()
      .subscribe(users => this.users = users);
    this.subscription=this.userService.getRoles()
      .subscribe(roles => this.roles = roles);
  }

  public addHandler(args: AddEvent): void {
    this.closeEditor(args.sender);
    this.formGroup = new FormGroup({
      username: new FormControl('',
        Validators.required),
      email: new FormControl(''),
      operations: new FormControl(this.operations,
        Validators.required),
      role: new FormControl({ name: "Select Role",id: 0 }, [Validators.required])
    });
    args.sender.addRow(this.formGroup);
  }
  private closeEditor(grid: GridComponent, rowIndex = this.editedRowIndex) {
    grid.closeRow(rowIndex);
    this.editedRowIndex = undefined;
    this.formGroup = undefined;
  }
  public saveHandler({ sender, rowIndex, formGroup }: SaveEvent): void {
    if (formGroup.invalid) {
      return;
    }
    this.user=formGroup.value;
    this.user.roleId=formGroup.value.role.id;
    if(formGroup.value.id!=null){
      this.subscription=this.userService.put(this.user).subscribe({
        next: () => {
          this.ngOnInit();
          this.alertService.success('User detail updated', { keepAfterRouteChange: true });
          this.router.navigateByUrl('/users');
           sender.closeRow(rowIndex);
        },
        error: (error: any) => {
          this.alertService.error(error);
        }
      });
    }
    else{
      this.user.password=environment.tempPassword;
      this.subscription=this.userService.post(this.user).subscribe({
        next: () => {
          this.ngOnInit();
          this.alertService.success('User detail saved', { keepAfterRouteChange: true });
          this.router.navigateByUrl('/users');
          sender.closeRow(rowIndex);
        },
        error: (error: any) => {
          this.alertService.error(error);
        }
      });
    }
  }
  public editHandler({ sender, rowIndex, dataItem }: EditEvent): void {

    this.closeEditor(sender);

    this.formGroup = new FormGroup({
      id: new FormControl(dataItem.id),
      username: new FormControl(dataItem.username,
        Validators.required),
      email: new FormControl(dataItem.email),
      operations: new FormControl(dataItem.operations,
        Validators.required),
      role: new FormControl({ name:dataItem.rolename, id:dataItem.roleId }, [Validators.required]),
      operationId: new FormControl(dataItem.operationId),
      password:new FormControl(dataItem.password),
      lastPasswordChange:new FormControl(dataItem.lastPasswordChange)
    });

    this.editedRowIndex = rowIndex;

    sender.editRow(rowIndex, this.formGroup);    
  }
  public removeHandler({ dataItem }: RemoveEvent): void {
    if (confirm('Are you sure??')) {
    this.subscription=this.userService.delete(dataItem.id).subscribe({
      next: () => {
        this.ngOnInit();
        this.alertService.success('Delete Successfully', { keepAfterRouteChange: true });
        this.router.navigateByUrl('/users');
      },
      error: (error: any) => {
        this.alertService.error(error);
      }
    });
   }
  }
  public cancelHandler(args: CancelEvent): void {
    this.closeEditor(args.sender, args.rowIndex);
  }

  ngOnDestroy(): void {
    if(this.subscription){
      this.subscription.unsubscribe();
    }
  }
  public dropDownChanges(role: Event): void {
    this.formGroup!.controls.role.setValue(role);
  }
  public multiSelectChanges(operations:string[]): void {
    this.formGroup!.controls.operations.setValue(operations);
  }
}