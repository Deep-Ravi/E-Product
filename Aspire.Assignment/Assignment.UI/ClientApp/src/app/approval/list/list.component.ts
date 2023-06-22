import { Component, OnInit,OnDestroy } from '@angular/core';
import { EditEvent,} from '@progress/kendo-angular-grid';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AlertService } from 'src/app/_services/alert.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Category } from 'src/app/_models/category';
import { Skill } from 'src/app/_models/skill';
import { SkillSetService } from 'src/app/_services/skillset.service';
import { SkillSet } from 'src/app/_models/skillset';
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
  index!:number;
  inline:string='inline';
  categories: Category[] = [];
  skills:Skill[]=[];
  skillSetDetails:SkillSet[]=[];
  selectedSkillSetDetails:SkillSet[]=[];
  skillSet!:SkillSet;
  gridView: SkillSet[]=[];
  public formGroup?: FormGroup;
  subscription!:Subscription;
  public currentDate=new Date();
  constructor(private alertService: AlertService,private router: Router,private skillSetService:SkillSetService) {

  }
  
  ngOnInit() {
    this.subscription=this.skillSetService.getApprovals()
      .subscribe(skillSetDetails => {this.skillSetDetails = skillSetDetails;
      this.gridView=skillSetDetails});  
  }
  public statusHandler({ dataItem }: EditEvent): void {
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
  }
  ngOnDestroy(): void {
    if(this.subscription){
      this.subscription.unsubscribe();
    }
  }
  

  public onFilter(inputValue: string): void {
      this.gridView = process(this.skillSetDetails, {
        filter: {
          logic: "or",
          filters: [
            {
              field: "userName",
              operator: "contains",
              value: inputValue,
            },       
          ],
        },
      }).data;
    }

    public ForApprove(){
      this.UpdateApproval(this.approvalStatus[2]);
    }
    public ForReject(){
      this.UpdateApproval(this.approvalStatus[3]);
    }
    public UpdateApproval(status:string){
       this.skillSet=this.formGroup?.value;
       this.skillSet.isSendForApproval=false;
       this.skillSet.approvalStatus=status;
       this.skillSet.categoryId=this.formGroup?.value.category.id;
       this.skillSet.skillId=this.formGroup?.value.skill.id;
       this.subscription=this.skillSetService.updateApproval(this.skillSet).subscribe({
        next: () => {
          this.ngOnInit();
          this.alertService.success('Skill has been '+status, { keepAfterRouteChange: true });
          this.router.navigateByUrl('/approval');
        },
        error: (error: any) => {
          this.alertService.error(error);
        }
      });
    }
}