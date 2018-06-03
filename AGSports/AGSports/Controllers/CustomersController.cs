using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AGSports.Contracts;
using AGSports.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AGSports.Controllers
{
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
       
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
          
            _customerRepository = customerRepository;

        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            var result = new ObjectResult(_customerRepository.GetAll())
            {
                StatusCode = (int)HttpStatusCode.OK
            };
            Request.HttpContext.Response.Headers.Add("X-Total-Count", _customerRepository.GetAll().Count().ToString());

            return result;
        }


        [HttpGet("{id}",Name ="GetCustomer")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            var customer = await _customerRepository.Find(id);
            return Ok(customer);
          }

        private async Task<bool> CustomerExists(int id)
        {
            return await _customerRepository.Exists(id);
        }

        [HttpPost]
        public async Task<IActionResult>  PostCustomer([FromBody] Customer customer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           await _customerRepository.Add(customer);          
           return CreatedAtAction("getCustomer", new { id = customer.CustomerId});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>  PutCustomer([FromRoute] int id , [FromBody] Customer customer)
        {

            try
            {
              
                await _customerRepository.Update(customer);
                return Ok(customer);

            }
            catch (DbUpdateConcurrencyException)
            {

                if(!await CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
          
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            var customer =await _customerRepository.Remove(id);           
            return Ok(customer);

        }


     
    }
}
