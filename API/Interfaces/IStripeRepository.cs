using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;

namespace API.Interfaces
{
    public interface IStripeRepository
    {
        Task<bool> handlePaymentIntentSucceeded(PaymentIntent paymentIntent);
        // handlePaymentIntentSucceeded(paymentIntent)
    }
}