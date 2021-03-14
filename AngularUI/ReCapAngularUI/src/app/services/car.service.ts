import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CarDto } from '../models/carDto';
import { ListResponseModel } from '../models/listResponseModel';

@Injectable({
  providedIn: 'root'
})
export class CarService {
 
  apiUrl: string = "https://localhost:44358/api/";

  constructor(private httpClient: HttpClient) { }

  getCars(): Observable<ListResponseModel<CarDto>> {
    let newPath = this.apiUrl + "cars/getcarsdetail";
    return this.httpClient.get<ListResponseModel<CarDto>>(newPath);
  }
}
