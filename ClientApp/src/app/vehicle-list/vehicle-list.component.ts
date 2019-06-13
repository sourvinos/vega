import { Vehicle } from './../models/vehicle';
import { KeyValuePair } from './../models/keyValuePair';
import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';

@Component({
    selector: 'app-vehicle-list',
    templateUrl: './vehicle-list.component.html',
    styleUrls: ['./vehicle-list.component.css']
})

export class VehicleListComponent implements OnInit {

    displayModel: {
        totalItems: number
        items: Vehicle[]
    }

    filter: any = {}
    vehicles: Vehicle[]
    makes: KeyValuePair[]

    queryResult: any = {}

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
        return this.service.getVehicles(this.filter).subscribe(result => {
            this.queryResult = result
            console.log(this.queryResult)
        })
    }

}
