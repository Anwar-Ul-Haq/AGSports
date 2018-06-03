using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AGSports.Models;

namespace AGSports.Contracts
{
    public interface ICustomerRepository
    {
        Task<Customer> Add(Customer customer);
        IEnumerable<Customer> GetAll();
        Task<Customer> Find(int id);
        Task<Customer> Update(Customer customer);
        Task<Customer> Remove(int id);
        Task<bool> Exists(int id);

    }
}
