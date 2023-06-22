import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SkillSetRoutingModule } from './skillset-routing.module';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { GridModule } from '@progress/kendo-angular-grid';
import { DateInputModule, DatePickerModule } from '@progress/kendo-angular-dateinputs';
import { IntlModule } from '@progress/kendo-angular-intl';
import { DropDownListModule, MultiSelectModule } from '@progress/kendo-angular-dropdowns';
import { LayoutComponent } from './layout/layout.component';
import { ListComponent } from './list/list.component';
import { TextBoxModule } from '@progress/kendo-angular-inputs';


@NgModule({
  declarations: [
    LayoutComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    SkillSetRoutingModule,
    ButtonsModule,
    GridModule,
    DateInputModule,
    IntlModule,
    DatePickerModule,
    DropDownListModule,
    TextBoxModule
  ]
})
export class SkillSetModule { }
