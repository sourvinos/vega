import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

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

    create(vehicle: any) {
        return this.http.post('/api/vehicles', vehicle);
    }

    get(id: number) {
        return this.http.get('/api/vehicles/' + id);
    }

}
