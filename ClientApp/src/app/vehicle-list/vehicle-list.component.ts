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
    allVehicles: Vehicle[]

    makes: KeyValuePair[]
    filter: any = {}

    constructor(private service: VehicleService) { }

    ngOnInit() {
        this.service.getMakes().subscribe(makes => {
            this.makes = makes
        })
        this.service.getVehicles().subscribe(vehicles => this.vehicles = this.allVehicles = vehicles, error => { console.log("Problem!") });
    }

    onFilterChange() {
        let vehicles = this.allVehicles

        if (this.filter.makeId) {
            vehicles = vehicles.filter(v => v.make.id == this.filter.makeId)
        }

        this.vehicles = vehicles
    }

    resetFilters() {
        this.filter = {}
        this.vehicles = this.allVehicles;
    }

}
