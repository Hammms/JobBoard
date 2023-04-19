using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;
using AutoMapper;
using API.DTOs;
using API.Interfaces;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class StripeRepository : IStripeRepository
    {
        private readonly DataContext _context;
        public readonly IMapper _mapper;
        public StripeRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> handlePaymentIntentSucceeded(PaymentIntent paymentIntent) {
            var SubDate = DateTime.Now.AddDays(30);
            //var query = _context.Users.AsQueryable();
            Console.WriteLine(paymentIntent.CustomerId); Console.WriteLine(paymentIntent.CustomerId);Console.WriteLine(paymentIntent.CustomerId);
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
                        
            return await Ok();
            // var query = from _context in _context.Users where _context.StripeId == paymentIntent.CustomerId select _context;
            // if(query != null)
            // {
            //     query.SubscriptionExperation = SubDate;
            // }

       
            // run a query to find the user associated with the Stripe ID
      
            //  return await _context.Update();

            // find the username that is associated with the paramter customer id
            // convert that into a DTO
            // create another 

            // var user = query.ProjectTo<UserDto>(_mapper.ConfigurationProvider);

            //var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
          
            // Create the subscription time 
          
           // _context.Users.Update();
            
        }

        private Task<bool> Ok()
        {
            throw new NotImplementedException();
        }
    }
}