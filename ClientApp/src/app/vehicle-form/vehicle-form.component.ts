import { Component, OnInit } from '@angular/core';
import { VehicleService } from './../services/vehicle.service';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';

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

    constructor(private route: ActivatedRoute, private router: Router, private vehicleService: VehicleService) {
        route.params.subscribe(p => this.vehicle.id = +p['id']);
    }

    ngOnInit() {
        let sources = [this.vehicleService.getMakes(), this.vehicleService.getFeatures()];

        if (this.vehicle.id) {
            sources.push(this.vehicleService.get(this.vehicle.id));
        }

        return forkJoin(sources).subscribe(result => {
            this.makes = result[0], this.features = result[1], this.vehicle = result[2]
        });
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
            .subscribe(result => result);
    }

}
