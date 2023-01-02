import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ServicesService {
  baseApiUrl: string =environment.apiUrl;
  constructor(private http:HttpClient) { }
  getAllItems(){
    return this.http.get(this.baseApiUrl+'/api/Order/GetAllItem');
  }
  getAllCustomer(){
    return this.http.get(this.baseApiUrl+'/api/Order/GetAllCustomer');
  }
  saveOrder(order:any){
      console.log(order,"orderrrrrrrrrrrr");
    return this.http.post(this.baseApiUrl+'/api/Order/CreatOrder',order);
  }
}
