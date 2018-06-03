using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AGSports.Contracts;
using AGSports.Models;

namespace AGSports.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AGSportsContext _context;
        public CustomerRepository(AGSportsContext context)
        {
            _context =context;
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
            return await _context.Customer.Include(customer => customer.Order).SingleOrDefaultAsync(a => a.CustomerId == id);
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
