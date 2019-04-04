import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  baseUrl = environment.apiUrl + 'auth/';

  constructor(private http: HttpClient) { }

  getBrands() {
    return this.http.get(this.baseUrl + 'brands');
  }

}
