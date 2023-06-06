import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { ProductService } from 'src/app/_services/product.service';
import { Product } from 'src/app/_models/product';
import { environment } from 'src/environments/environment';

describe('MockProductService', () => {
  let service: ProductService;
  let fakeData:Product={
    "id":"395AF749-FBF9-4EEE-83B3-3EE630F4A8F4",
    "name":"ABCD",
    "description":"Testing",
    "price":"10.00",
    "expiryDate":new Date()
  }
  let http: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(ProductService);
    http = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('getAll() should return data', () => {
    service.getAll().subscribe((res) => {
      expect(res).toEqual([fakeData]);
    });
    const req = http.expectOne(environment.apiUrl+'/api/Product');
    expect(req.request.method).toBe('GET');
    req.flush([fakeData]);
  });

  it(' post method should post and return message', () => {
    service.post(fakeData).subscribe((res) => {
       expect(res).toEqual({ msg: 'Product detail saved' });
    });
    const req = http.expectOne(environment.apiUrl+'/api/Product');
    expect(req.request.method).toBe('POST');
    req.flush({ msg: 'Product detail saved' });
  });

  it(' put method should put and return message', () => {
    service.put(fakeData).subscribe((res) => {
       expect(res).toEqual({ msg: 'Product detail updated' });
    });
    const req = http.expectOne(environment.apiUrl+'/api/Product');
    expect(req.request.method).toBe('PUT');
    req.flush({ msg: 'Product detail updated' });
  });

  it(' delete method should put and return message', () => {
    service.delete(fakeData.id!).subscribe((res) => {
       expect(res).toEqual({ msg: 'Delete Successfully' });
    });
    const req = http.expectOne(environment.apiUrl+'/api/Product/'+fakeData.id!);
    expect(req.request.method).toBe('DELETE');
    req.flush({ msg: 'Delete Successfully' });
  });
});