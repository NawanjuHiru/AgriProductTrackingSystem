import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OrderContainerModel } from 'src/app/models/order/order.container.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor
  (
    private httpClient: HttpClient
  ) 
  { 

  }
  checkOutOrder(orderContainer:OrderContainerModel):Observable<any>{
      return this.httpClient.post<any>
      (environment.paymentApiUrl + 'PaymentService',orderContainer);
  }
}