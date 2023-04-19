import { HttpClient } from '@angular/common/http';
import { ErrorHandler, Injectable } from '@angular/core';
import { catchError, map, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StripeService {
  baseUrl = environment.apiUrl;
  

  constructor(private http: HttpClient) { }

  // createCheckoutSession() {
  //   return this.http.post(this.baseUrl + 'stripe/create-checkout-session').pipe(
  //     const priceId = 'price_1MRiqWFZ624MM6ngd1sVR479'
  //   )
        
  //     }
    
  }

