import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { AppService } from '../_services/app.service';
import { App } from '../_models/app';


@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
    apps?: App[];

    constructor(private appService: AppService) {}

    ngOnInit() {
        this.appService.getAll()
           // .pipe(first())
            .subscribe(apps => this.apps = apps);
    }

}