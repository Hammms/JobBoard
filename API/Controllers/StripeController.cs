using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using API.DTOs;
using API.Helpers.Stripe;
using API.Helpers;
using API.Extensions;
using API.Interfaces;
using API.Data;

namespace API.Controllers
{
    public class StripeController : BaseAPiController
    {
      public readonly IOptions<StripeOptions> options;
      private readonly IStripeClient client;
      private readonly IStripeRepository _stripeRepository;

      private readonly DataContext _context;

      public StripeController(IOptions<StripeOptions> options, IStripeRepository stripeRepository,DataContext context)
      {
        this.options = options;
        this.client = new StripeClient(this.options.Value.SecretKey);
        _stripeRepository = stripeRepository;
        _context = context;
      }



        [HttpPost("webhook1")]
        public async Task<IActionResult> Webhook1()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            string endpointSecret = this.options.Value.WebhookSecret;
        try
        {
            var stripeEvent = EventUtility.ParseEvent(json);
            var signatureHeader = Request.Headers["Stripe-Signature"];

            stripeEvent = EventUtility.ConstructEvent(json,
                    signatureHeader, endpointSecret);

            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                Console.WriteLine("A successful payment for {0} was made.", paymentIntent.Amount);
                // Then define and call a method to handle the successful payment intent.
                //await _stripeRepository.handlePaymentIntentSucceeded(paymentIntent);
                var SubDate = DateTime.Now.AddDays(30);
                var query = _context.Users.SingleOrDefault(u => u.StripeId == paymentIntent.CustomerId);
                if (query != null)
            {
                try
                {
                    _context.Users.Update(query);
                    query.SubscriptionExperation = SubDate;
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
                        
            
            }
            else if (stripeEvent.Type == Events.PaymentMethodAttached)
            {
                var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                // Then define and call a method to handle the successful attachment of a PaymentMethod.
                // handlePaymentMethodAttached(paymentMethod);
            }
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }
            return Ok();
        }
        catch (StripeException e)
        {
            Console.WriteLine("Error: {0}", e.Message);
            return BadRequest();
        }
        catch (Exception e)
        {
          return StatusCode(500);
        }
        }

      [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            Event stripeEvent;
            try
            {
                
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    this.options.Value.WebhookSecret
                );
                Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something failed {e}");
                return BadRequest();
            }

            switch (stripeEvent.Type) {
                case "checkout.session.completed":
                    // Payment is successful and the subscription is created.
                    // You should provision the subscription and save the customer ID to your database.
                break;
                case "invoice.paid":
                    // Continue to provision the subscription as payments continue to be made.
                    // Store the status in your database and check when a user accesses your service.
                    // This approach helps you avoid hitting rate limits.
                break;
                case "invoice.payment_failed":
                    // The payment failed or the customer does not have a valid payment method.
                    // The subscription becomes past_due. Notify your customer and send them to the
                    // customer portal to update their payment information.
                break;
                default:
                    return BadRequest();
    }

            return Ok();
        }

        [HttpGet("debug")]
        public async Task<IActionResult> ping()
        {
          Console.WriteLine("Testing");
          return Ok();
        }


        [HttpPost("create-customer-portal-session")]
        public IActionResult CustomerPortal()
        {
            // Authenticate your user.
            var options = new SessionCreateOptions
            {
                Customer = "{{CUSTOMER_ID}}",
               // ReturnUrl = "https://localhost:4001/",
            };
            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

    }
}