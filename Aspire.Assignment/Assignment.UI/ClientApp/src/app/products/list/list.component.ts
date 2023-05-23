import { Component, OnInit,OnDestroy } from '@angular/core';
import { Product } from '../../_models/product';
import { ProductService } from 'src/app/_services/product.service';
import { AddEvent, CancelEvent, EditEvent, GridComponent, RemoveEvent, SaveEvent } from '@progress/kendo-angular-grid';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AlertService } from 'src/app/_services/alert.service';
import { Router } from '@angular/router';
import { ExcelExportData } from '@progress/kendo-angular-excel-export';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit, OnDestroy{
  public pageSize = 5;
  public buttonCount = 2;
  public sizes = [10,20,30];
  private price!:string;
  products: Product[] = [];
  product!:Product;
  fileFormat: string = "xlsx";
  public formGroup?: FormGroup;
  public currentDate=new Date();
  private editedRowIndex?: number;
  subscription!:Subscription;
  constructor(private productService: ProductService, private alertService: AlertService, private router: Router) {
    this.allData = this.allData.bind(this);
  }

  ngOnInit() {
    this.subscription=this.productService.getAll()
      .subscribe(products => this.products = products);
  }

  public addHandler(args: AddEvent): void {
    this.closeEditor(args.sender);
    this.formGroup = new FormGroup({
      name: new FormControl('',
        Validators.required),
      description: new FormControl('', [
        Validators.required,
        Validators.minLength(3)]),
      price: new FormControl('', [
        Validators.required,
        Validators.pattern("(^\\d+(\\.\\d{1,2})?$)")]),
      expiryDate: new FormControl(new Date(), [Validators.required])
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
    this.product=formGroup.value;
    this.price=formGroup.value.price.toString();
    this.product.price=this.price.includes(".")?this.price:formGroup.value.price.toFixed(2).toString();
    if(formGroup.value.id!=null){
      this.subscription=this.productService.put(this.product).subscribe({
        next: () => {
          this.ngOnInit();
          this.alertService.success('Product detail updated', { keepAfterRouteChange: true });
          this.router.navigateByUrl('/products');
           sender.closeRow(rowIndex);
        },
        error: (error: any) => {
          this.alertService.error(error);
        }
      });
    }
    else{
      this.subscription=this.productService.post(this.product).subscribe({
        next: () => {
          this.ngOnInit();
          this.alertService.success('Product detail saved', { keepAfterRouteChange: true });
          this.router.navigateByUrl('/products');
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
      id: new FormControl(dataItem.id.toString()),
      name: new FormControl(dataItem.name,
        Validators.required),
      description: new FormControl(dataItem.description, [
        Validators.required,
        Validators.minLength(3)]),
      price: new FormControl(+dataItem.price, [
        Validators.required,
        Validators.pattern("(^\\d+(\\.\\d{1,2})?$)")]),
      expiryDate: new FormControl(new Date(dataItem.expiryDate), [Validators.required])
    });

    this.editedRowIndex = rowIndex;

    sender.editRow(rowIndex, this.formGroup);    
  }
  public removeHandler({ dataItem }: RemoveEvent): void {
    if (confirm('Are you sure??')) {
    this.subscription=this.productService.delete(dataItem.id).subscribe({
      next: () => {
        this.ngOnInit();
        this.alertService.success('Delete Successfully', { keepAfterRouteChange: true });
        this.router.navigateByUrl('/products');
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
  convertor(newFormat: string) {
    this.fileFormat = newFormat;
  }
  public allData(): ExcelExportData {
    var result: ExcelExportData = {
      data: this.products
    }
    return result;
  }
  ngOnDestroy(): void {
    if(this.subscription){
      this.subscription.unsubscribe();
    }
  }
  public dateChanges(date:Date): void {
    this.formGroup!.controls.expiryDate.setValue(date);
  }
}