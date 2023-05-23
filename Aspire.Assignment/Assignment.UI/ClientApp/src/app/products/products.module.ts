import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { ListComponent } from './list/list.component';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { ExcelModule, GridModule, PDFModule } from '@progress/kendo-angular-grid';
import { ExcelExportModule } from '@progress/kendo-angular-excel-export';
import { PDFExportModule } from '@progress/kendo-angular-pdf-export';
import { DateInputModule, DatePickerModule } from '@progress/kendo-angular-dateinputs';
import { IntlModule } from '@progress/kendo-angular-intl';


@NgModule({
  declarations: [
    LayoutComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    ProductsRoutingModule,
    ButtonsModule,
    GridModule,
    ExcelExportModule,
    PDFExportModule,
    PDFModule,
    ExcelModule,
    DateInputModule,
    IntlModule,
    DatePickerModule,
  ]
})
export class ProductsModule { }
