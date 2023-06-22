import { Component, OnInit,OnDestroy } from '@angular/core';
import { AddEvent, CancelEvent, EditEvent, GridComponent, RemoveEvent, SaveEvent, SelectionEvent } from '@progress/kendo-angular-grid';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AlertService } from 'src/app/_services/alert.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Role, TokenResponse, User } from 'src/app/_models/user';
import { Category } from 'src/app/_models/category';
import { Skill } from 'src/app/_models/skill';
import { CategoryService } from 'src/app/_services/category.service';
import { SkillSetService } from 'src/app/_services/skillset.service';
import { AccountService } from 'src/app/_services/account.service';
import { SkillSet } from 'src/app/_models/skillset';
import { SkillService } from 'src/app/_services/skill.service';
import { environment } from 'src/environments/environment.prod';
import { process } from "@progress/kendo-data-query";


@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit, OnDestroy{
  public pageSize = 5;
  public buttonCount = 2;
  public sizes = [10,20,30];
  public approvalStatus=environment.approvalStatus;
  public proficiency=environment.proficiency;
  public yearsOfExperience=environment.yearsOfExperience;
  categories: Category[] = [];
  skills:Skill[]=[];
  skillSetDetails:SkillSet[]=[];
  selectedSkillSetDetails:SkillSet[]=[];
  skillSet!:SkillSet;
  gridView: SkillSet[]=[];
  isDisable:boolean=true;
  user!:TokenResponse|null;
  public formGroup?: FormGroup;
  private editedRowIndex?: number;
  subscription!:Subscription;
  public currentDate=new Date();
  constructor(private categoryService: CategoryService, private alertService: AlertService,private accountService:AccountService,private router: Router,private skillSetService:SkillSetService,private skillService:SkillService) {
    this.accountService.user.subscribe(x => this.user = x);
  }

  ngOnInit() {
    this.subscription=this.skillSetService.getAll(this.user!.userName)
      .subscribe(skillSetDetails => {this.skillSetDetails = skillSetDetails;
      this.gridView=skillSetDetails})
    this.subscription=this.categoryService.getAll()
      .subscribe(categories => this.categories = categories);  
  }

  public addHandler(args: AddEvent): void {
    this.closeEditor(args.sender);
    this.formGroup = new FormGroup({
      proficiency: new FormControl(this.proficiency[0],
        Validators.required),
      yearsOfExperience: new FormControl(this.yearsOfExperience[0]),
      achievement: new FormControl(''),
      category: new FormControl({ type: "Select Category",id:"0" },
        Validators.required),
      skill:new FormControl({ name:"Select Skills",id:"0",categoryId:"0" },
        Validators.required),
      lastWorkedDate:new FormControl(new Date(), [Validators.required]),
      userName:new FormControl(this.user!.userName),
      isNotified:new FormControl(false)
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
    this.skillSet=this.formGroup?.value;
    this.skillSet.categoryId=this.formGroup?.value.category.id;
    this.skillSet.skillId=this.formGroup?.value.skill.id;
    if(formGroup.value.id!=null){      
      this.subscription=this.skillSetService.put(this.skillSet).subscribe({
        next: () => {
          this.ngOnInit();
          this.alertService.success('Skill Updated', { keepAfterRouteChange: true });
          this.router.navigateByUrl('/skillset');
           sender.closeRow(rowIndex);
        },
        error: (error: any) => {
          this.alertService.error(error);
        }
      });
    }
    else{
      this.skillSet.isSendForApproval=false;
      this.subscription=this.skillSetService.post(this.skillSet).subscribe({
        next: () => {
          this.ngOnInit();
          this.alertService.success('New Skill Added', { keepAfterRouteChange: true });
          this.router.navigateByUrl('/skillset');
          sender.closeRow(rowIndex);
        },
        error: (error: any) => {
          this.alertService.error(error);
        }
      });
    }
  }
  public editHandler({ sender, rowIndex, dataItem }: EditEvent): void {
    this.subscription=this.skillService.getAll(dataItem.categoryId)
      .subscribe(skills => this.skills = skills);
    this.closeEditor(sender);
    this.formGroup = new FormGroup({
      id: new FormControl(dataItem.id),
      proficiency: new FormControl(dataItem.proficiency,
        Validators.required),
      yearsOfExperience: new FormControl(dataItem.yearsOfExperience),
      achievement: new FormControl(dataItem.achievement),
      category: new FormControl({ type:dataItem.categoryName, id:dataItem.categoryId },
        Validators.required),
      skill:new FormControl({ name:dataItem.skillName, id:dataItem.skillId ,categoryId:dataItem.categoryId},
        Validators.required),
      lastWorkedDate:new FormControl(new Date(dataItem.lastWorkedDate), [Validators.required]),
      userId:new FormControl(dataItem.userId),
      approvalStatus:new FormControl(dataItem.approvalStatus), 
      isSendForApproval:new FormControl(dataItem.isSendForApproval)

    });
    this.editedRowIndex = rowIndex;

    sender.editRow(rowIndex, this.formGroup);    
  }
  public removeHandler({ dataItem }: RemoveEvent): void {
    if (confirm('Are you sure??')) {
    this.subscription=this.skillSetService.delete(dataItem.id).subscribe({
      next: () => {
        this.ngOnInit();
        this.alertService.success('Delete Successfully', { keepAfterRouteChange: true });
        this.router.navigateByUrl('/skillset');
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
  public categoryDropDownChanges(category: Event): void {
    this.formGroup!.controls.category.setValue(category);
    this.subscription=this.skillService.getAll(this.formGroup!.value.category.id)
      .subscribe(skills => this.skills = skills);
      this.formGroup!.controls.skill.setValue({ name:"Select Skills",id:"0",categoryId:"0" });
  }
  public skillDropDownChanges(skill: Event): void {
    this.formGroup!.controls.skill.setValue(skill);
  }
  public proficiencyDropDownChanges(proficiency:string[]): void {
    this.formGroup!.controls.proficiency.setValue(proficiency);
  }
  public yearsOfExperienceDropDownChanges(yearsOfExperience:string[]): void {
    this.formGroup!.controls.yearsOfExperience.setValue(yearsOfExperience);
  }

  public onFilter(inputValue: string): void {
      this.gridView = process(this.skillSetDetails, {
        filter: {
          logic: "or",
          filters: [
            {
              field: "categoryName",
              operator: "contains",
              value: inputValue,
            },
            {
              field: "skillName",
              operator: "contains",
              value: inputValue,
            },
            {
              field: "proficiency",
              operator: "contains",
              value: inputValue,
            },         
          ],
        },
      }).data;
    }
    public SelectedChange(e:SelectionEvent){
      var selectLength=e.selectedRows!.length;
      if(selectLength!=0){
        for(let i=0;i<selectLength;i++){
          let selectedData=e.selectedRows?.map(row=>row.dataItem)[i];            
            if(!this.selectedSkillSetDetails.find(s=>s.id==selectedData.id)&&selectedData.id!=undefined){
              this.selectedSkillSetDetails.push(selectedData);
            }   
        }
      }
      
      var deselectLength=e.deselectedRows!.length;
      if(deselectLength!=0){
        for(let i=0;i<deselectLength;i++){
        let deselectedData=e.deselectedRows?.map(row=>row.dataItem)[i];
        if(deselectedData){
          if(this.selectedSkillSetDetails.find(s=>s.id==deselectedData.id&&deselectedData.id!=undefined)){
            this.selectedSkillSetDetails.splice(this.selectedSkillSetDetails.indexOf(deselectedData), 1);
          } 
        } 
        } 
      }
      if(this.selectedSkillSetDetails.length>0){
         this.isDisable=false;
      }
      else{
        this.isDisable=true;
      }
    }
    public SendApproval()
    {
      this.selectedSkillSetDetails.map(value=>{value.isSendForApproval=true,value.approvalStatus=this.approvalStatus[1]});
      this.subscription=this.skillSetService.setApprovals(this.selectedSkillSetDetails).subscribe({
        next: () => {
          this.ngOnInit();
          this.alertService.success('Skills are send for Approval', { keepAfterRouteChange: true });
          this.router.navigateByUrl('/skillset');
        },
        error: (error: any) => {
          this.alertService.error(error);
        }
      });
    }
}