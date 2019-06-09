import { KeyValuePair } from './../models/keyValuePair';
import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import { Vehicle } from '../models/vehicle';


@Component({
    selector: 'app-vehicle-list',
    templateUrl: './vehicle-list.component.html',
    styleUrls: ['./vehicle-list.component.css']
})

export class VehicleListComponent implements OnInit {

    vehicles: Vehicle[]
    makes: KeyValuePair[]
    filter: any = {}

    constructor(private service: VehicleService) { }

    ngOnInit() {
        this.service.getMakes().subscribe(makes => { this.makes = makes })
        this.populateVehicles()
    }

    onFilterChange() {
        this.populateVehicles()
    }

    resetFilters() {
        this.filter = {}
        this.populateVehicles()
    }

    populateVehicles() {
        return this.service.getVehicles(this.filter).subscribe(vehicles => this.vehicles = vehicles, error => { console.log("Problem!") });
    }

}
