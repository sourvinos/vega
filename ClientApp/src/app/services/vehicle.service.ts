import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SaveVehicle } from '../models/savevehicle';

@Injectable({
    providedIn: 'root'
})

export class VehicleService {

    constructor(private http: HttpClient) { }

    getMakes() {
        return this.http.get('/api/makes');
    }

    getFeatures() {
        return this.http.get('/api/features');
    }

    getVehicles() {
        return this.http.get('/api/vehicles/');
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
