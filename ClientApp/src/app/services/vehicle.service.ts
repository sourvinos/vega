import { KeyValuePair } from './../models/keyValuePair'
import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { SaveVehicle } from '../models/savevehicle'
import { Observable } from 'rxjs'
import { Vehicle } from '../models/vehicle'

@Injectable({
    providedIn: 'root'
})

export class VehicleService {

    private readonly vehiclesEndPoint: string = '/api/vehicles'

    constructor(private http: HttpClient) { }

    getMakes(): Observable<KeyValuePair[]> {
        return this.http.get<KeyValuePair[]>('/api/makes')
    }

    getFeatures() {
        return this.http.get('/api/features')
    }

    getVehicles(filter: any): Observable<Vehicle[]> {
        return this.http.get<Vehicle[]>(this.vehiclesEndPoint + "?" + this.toQueryString(filter))
    }

    getVehicle(id: number) {
        return this.http.get(this.vehiclesEndPoint + id)
    }

    createVehicle(vehicle: SaveVehicle) {
        return this.http.post(this.vehiclesEndPoint, vehicle)
    }

    updateVehicle(vehicle: SaveVehicle) {
        return this.http.put(this.vehiclesEndPoint + vehicle.id, vehicle)
    }

    deleteVehicle(id: number) {
        return this.http.delete(this.vehiclesEndPoint + id)
    }

    toQueryString(obj) {
        var parts = []

        for (var property in obj) {
            var value = obj[property]
            if (value != null && value != undefined) {
                parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value))
            }
        }

        return parts.join('&')
    }

}
