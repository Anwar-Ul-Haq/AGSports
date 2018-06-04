using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AGSports.Contracts;
using AGSports.Models;
using Microsoft.Extensions.Caching.Memory;

namespace AGSports.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AGSportsContext _context;

        private readonly IMemoryCache _cache;
        public CustomerRepository(AGSportsContext context , IMemoryCache cache)
        {
            _context =context;
            _cache = cache;
        }
        public async Task<Customer> Add(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public  async Task<bool> Exists(int id)
        {
            return await _context.Customer.AnyAsync(x => x.CustomerId == id);
        }

        public  async Task<Customer> Find(int id)
        {

            var cachedCustomer = _cache.Get<Customer>(id);

            if(cachedCustomer != null)
            {
                return cachedCustomer;
            }

            else
            {
              var persistedCustomer =  await _context.Customer.Include(customer => customer.Order).SingleOrDefaultAsync(a => a.CustomerId == id);
              var cacheEntryOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(persistedCustomer.CustomerId, persistedCustomer, cacheEntryOption);

                return persistedCustomer;
            }
          
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customer;
        }

        public  async Task<Customer> Remove(int id)
        {
            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CustomerId == id);
            _context.Customer.Remove(customer);

            await _context.SaveChangesAsync();

            return customer;

        }

        public async Task<Customer> Update(Customer customer)
        {
            _context.Customer.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}
