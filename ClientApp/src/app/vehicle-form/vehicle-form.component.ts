import { VehicleService } from './../services/vehicle.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})

export class VehicleFormComponent implements OnInit {

  makes: any;
  models: any;
  vehicle = { make: null };
  features: any;

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe(result => this.makes = result);
    this.vehicleService.getFeatures().subscribe(result => this.features = result);
  }

  onMakeChange() {
    let selectedMake = this.makes.find((model: { id: any; }) => model.id == this.vehicle.make);
    this.models = selectedMake.models;
  }

}
