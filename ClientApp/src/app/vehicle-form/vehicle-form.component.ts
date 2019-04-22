import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
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
        makeId: null,
        isRegistered: false,
        features: [],
        contact: {
            name: null,
            email: null,
            phone: null
        }
    };

    constructor(private vehicleService: VehicleService, private toastr: ToastrService) { }

    ngOnInit() {
        this.vehicleService.getMakes().subscribe(result => this.makes = result);
        this.vehicleService.getFeatures().subscribe(result => this.features = result);
    }

    onMakeChange() {
        const selectedMake = this.makes.find((x: { id: any; }) => x.id == this.vehicle.makeId);
        this.models = selectedMake ? selectedMake.models : [];
        delete this.vehicle.modelId;
    }

    onFeatureToggle(featureId: any, $event: { target: { checked: any; }; }) {
        if ($event.target.checked) {
            this.vehicle.features.push(featureId);
        } else {
            const index = this.vehicle.features.indexOf(featureId);
            console.log('Delete: ' + index);
            this.vehicle.features.splice(index, 1);
        }
    }

    onSubmit() {
        this.vehicleService
            .create(this.vehicle)
            .subscribe(result => console.log(result));
    }

}
