import { MakeService } from './../services/make.service';
import { Component, OnInit } from '@angular/core';
import { makeStateKey } from '@angular/platform-browser';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})

export class VehicleFormComponent implements OnInit {

  makes: any;
  models: any;
  vehicle = {
    make: null
  };

  constructor(private makeService: MakeService) { }

  ngOnInit() {
    this.makeService.getMakes().subscribe(result => {
      this.makes = result;
    });
  }

  onMakeChange() {
    let selectedMake = this.makes.find((model: { id: any; }) => model.id == this.vehicle.make);
    this.models = selectedMake.models;
  }

}
