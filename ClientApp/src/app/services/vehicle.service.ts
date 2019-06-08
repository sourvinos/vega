import { KeyValuePair } from './../models/keyValuePair';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SaveVehicle } from '../models/savevehicle';
import { Observable } from 'rxjs';
import { Vehicle } from '../models/vehicle';

@Injectable({
    providedIn: 'root'
})

export class VehicleService {

    constructor(private http: HttpClient) { }

    getMakes(): Observable<KeyValuePair[]> {
        return this.http.get<KeyValuePair[]>('/api/makes');
    }

    getFeatures() {
        return this.http.get('/api/features');
    }

    getVehicles(): Observable<Vehicle[]> {
        return this.http.get<Vehicle[]>('/api/vehicles/');
    }

    getVehicle(id: number) {
        return this.http.get('/api/vehicles/' + id);
    }

    createVehicle(vehicle: SaveVehicle) {
        return this.http.post('/api/vehicles', vehicle);
    }

    updateVehicle(vehicle: SaveVehicle) {
        return this.http.put('/api/vehicles/' + vehicle.id, vehicle);
    }

    deleteVehicle(id: number) {
        return this.http.delete('/api/vehicles/' + id);
    }

}
