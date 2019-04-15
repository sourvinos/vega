import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class MakeService {

    private url: string = 'https://localhost:44322/api/makes';

    constructor(private http: HttpClient) { }

    getMakes() {
        return this.http.get(this.url);
    }

}
