import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { GridModule, PDFModule } from '@progress/kendo-angular-grid';
import { PDFExportModule } from '@progress/kendo-angular-pdf-export';
import { DateInputModule, DatePickerModule } from '@progress/kendo-angular-dateinputs';
import { IntlModule } from '@progress/kendo-angular-intl';
import { DropDownListModule, MultiSelectModule } from '@progress/kendo-angular-dropdowns';
import { LayoutComponent } from './layout/layout.component';
import { ListComponent } from './list/list.component';
import { TextBoxModule } from '@progress/kendo-angular-inputs';
import { SkillApprovalRoutingModule } from './skillapproval-routing.module';


@NgModule({
  declarations: [
    LayoutComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    SkillApprovalRoutingModule,
    GridModule,
    TextBoxModule
  ]
})
export class SkillApprovalModule { }
