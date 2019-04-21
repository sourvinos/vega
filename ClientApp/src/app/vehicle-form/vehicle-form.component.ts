import { Component, OnInit } from '@angular/core';
import { VehicleService } from './../services/vehicle.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})

export class VehicleFormComponent implements OnInit {

  makes: any;
  models: any;
  features: any;
  vehicle: any = {
    features: []
  }

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe(result => this.makes = result);
    this.vehicleService.getFeatures().subscribe(result => this.features = result);
  }

  onMakeChange() {
    let selectedMake = this.makes.find((x: { id: any; }) => x.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vehicle.modelId;
  }

  onFeatureToggle(featureId, $event) {
    if ($event.target.checked) {
      this.vehicle.features.push(featureId)
    } else {
      let index = this.vehicle.features.indexOf(featureId);
      console.log("Delete: " + index);
      this.vehicle.features.splice(index, 1);
    }

  }

}
