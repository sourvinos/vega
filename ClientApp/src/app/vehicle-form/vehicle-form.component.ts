import * as _ from 'underscore'
import { ActivatedRoute, Router } from '@angular/router'
import { Component, OnInit } from '@angular/core'
import { KeyValuePair } from './../models/keyValuePair'
import { SaveVehicle } from '../models/savevehicle'
import { Vehicle } from './../models/vehicle'
import { VehicleService } from './../services/vehicle.service'
import { forkJoin } from 'rxjs'

@Component({
    selector: 'app-vehicle-form',
    templateUrl: './vehicle-form.component.html',
    styleUrls: ['./vehicle-form.component.css'],
})

export class VehicleFormComponent implements OnInit {

    makes: any
    models: any
    features: any

    vehicle: SaveVehicle = {
        id: 0,
        makeId: 0,
        modelId: 0,
        isRegistered: false,
        features: [],
        contact: { name: '', email: '', phone: '' },
    }

    constructor(private route: ActivatedRoute, private router: Router, private vehicleService: VehicleService) {
        route.params.subscribe(p => (this.vehicle.id = +p['id']))
    }

    ngOnInit() {
        let sources = [
            this.vehicleService.getMakes(),
            this.vehicleService.getFeatures(),
        ]

        if (this.vehicle.id) {
            sources.push(this.vehicleService.getVehicle(this.vehicle.id))
        }

        return forkJoin(sources).subscribe(
            result => {
                this.makes = result[0]
                this.features = result[1]
                if (this.vehicle.id) {
                    this.setVehicle(result[2] as {
                        id: number
                        make: KeyValuePair
                        model: KeyValuePair
                        isRegistered: boolean
                        features: []
                        contact: { name: string; email: string; phone: string }
                        lastUpdate: string
                    })
                    this.populateModels()
                }
            },
            error => {
                if (error.status == 404) {
                    this.router.navigate(['/error'])
                }
            }
        )
    }

    private populateModels() {
        const selectedMake = this.makes.find(
            (x: { id: any }) => x.id == this.vehicle.makeId
        )
        this.models = selectedMake ? selectedMake.models : []
    }

    private setVehicle(v: Vehicle) {
        this.vehicle.id = v.id
        this.vehicle.makeId = v.make.id
        this.vehicle.modelId = v.model.id
        this.vehicle.isRegistered = v.isRegistered
        this.vehicle.contact = v.contact
        this.vehicle.features = _.pluck(v.features, 'id')
    }

    onMakeChange() {
        this.populateModels()
        delete this.vehicle.modelId
    }

    onFeatureToggle(featureId: any, $event: { target: { checked: any } }) {
        if ($event.target.checked) {
            this.vehicle.features.push(featureId)
        } else {
            const index = this.vehicle.features.indexOf(featureId)
            console.log('Delete: ' + index)
            this.vehicle.features.splice(index, 1)
        }
    }

    onSubmit() {
        this.vehicleService
            .createVehicle(this.vehicle)
            .subscribe(result => result)
    }
}
