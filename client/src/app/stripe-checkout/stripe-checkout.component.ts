import { Component, OnInit } from '@angular/core';
import { CheckboxControlValueAccessor, FormBuilder, FormGroup, Validators} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-stripe-checkout',
  templateUrl: './stripe-checkout.component.html',
  styleUrls: ['./stripe-checkout.component.css']
})
export class StripeCheckoutComponent implements OnInit {
  
  handler:any = null;
  ngOnInit(): void {
    this.loadStripe();
  }

  pay(amount: any) {    
 
    var handler = (<any>window).StripeCheckout.configure({
      key: 'pk_test_51Mc9s2BPbT1hdGIIu7nH6iwHZFTlk7P3Rka1qr5HUQk5BzfU45CIgIKonNanSIJvZTHritW0k6h4LeVN8HLHE6nS00rB4Er9sp',
      locale: 'auto',
      token: function (token: any) {
        // You can access the token ID with `token.id`.
        // Get the token ID to your server-side code for use.
        console.log(token)
        alert('Token Created!!');
      }
    });
 
    handler.open({
      name: 'Demo Site',
      description: '2 widgets',
      amount: amount * 100
    });
 
  }

  loadStripe() {
     
    if(!window.document.getElementById('stripe-script')) {
      var s = window.document.createElement("script");
      s.id = "stripe-script";
      s.type = "text/javascript";
      s.src = "https://checkout.stripe.com/checkout.js";
      s.onload = () => {
        this.handler = (<any>window).StripeCheckout.configure({
          key: 'pk_test_51Mc9s2BPbT1hdGIIu7nH6iwHZFTlk7P3Rka1qr5HUQk5BzfU45CIgIKonNanSIJvZTHritW0k6h4LeVN8HLHE6nS00rB4Er9sp',
          locale: 'auto',
          token: function (token: any) {
            // You can access the token ID with `token.id`.
            // Get the token ID to your server-side code for use.
            console.log(token)
            alert('Payment Success!!');
          }
        });
      }
       
      window.document.body.appendChild(s);
    }
  }
  

  }

  




  //Price ID For 1 Month Subscription
  // price_1MRiqWFZ624MM6ngd1sVR479


