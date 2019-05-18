import { Vehicle } from './../models/vehicle';
import { Component, OnInit } from '@angular/core';

import { VehicleService } from '../services/vehicle.service';

@Component({
    selector: 'app-vehicle-list',
    templateUrl: './vehicle-list.component.html',
    styleUrls: ['./vehicle-list.component.css']
})

export class VehicleListComponent implements OnInit {

    vehicles: Vehicle[];

    constructor(private service: VehicleService) { }

    ngOnInit() {
        this.service.getVehicles().subscribe(result => { console.log(result) }, error => { console.log("Problem!") });
    }

}
