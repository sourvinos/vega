import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})

export class FeatureService {

    private url: string = '/api/features';

    constructor(private http: HttpClient) { }

    getFeatures() {
        return this.http.get(this.url);
    }

}
