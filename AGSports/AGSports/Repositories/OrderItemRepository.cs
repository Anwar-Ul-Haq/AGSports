using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AGSports.Contracts;
using AGSports.Models;

namespace AGSports.Repositories
{
    public class OrderItemRepository  : IOrderItemRepository
    {
        private readonly AGSportsContext _context;

        public OrderItemRepository(AGSportsContext context)
        {
            _context = context;
        }
    }
}
