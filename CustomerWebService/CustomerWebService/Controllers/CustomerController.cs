using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiteDB;
using CustomerWebService.Models;
using Microsoft.Extensions.Logging;

namespace CustomerWebService.Controllers
{
    /// <summary>
    /// this class creates HTTP requests for basic CRUD operations to our WebService
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger _logger;
        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// get all customers from litedb database
        /// if database contains no customer, empty array is returned
        /// </summary>
        /// <returns></returns>
        // GET api/customer
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            using(var db = new LiteDatabase(@"myLiteDb.db"))
            {
                _logger.LogInformation("Getting all customers at {RequestTime}", DateTime.Now);
                var _context = db.GetCollection<Customer>().FindAll();
                return Ok(_context);
            }
        }
        /// <summary>
        /// get customer specified by his id, the id needs to be GUID, otherwise bad request is returned
        /// if database does not contain customer with this id, return NOT FOUND message
        /// </summary>
        /// <param name="id">GUID type, specifies wanted customer</param>
        /// <returns>function returns customer with specified id</returns>
        // GET api/customer/5
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(Guid id)
        {
            _logger.LogInformation("Getting customer by {ID} at {RequestTime}", id, DateTime.Now);
            using (var db = new LiteDatabase(@"myLiteDb.db"))
            {
                var customerItem = db.GetCollection<Customer>().FindById(id);
                if(customerItem == null)
                {
                    _logger.LogWarning("Getting customer by ({ID}) was not successful, customer was NOT FOUND at {RequestTime}", id, DateTime.Now);
                    return NotFound();
                }
                return customerItem;
            }
        }
        /// <summary>
        /// creates new customer from body, all customer parameters needs to be set
        /// </summary>
        /// <param name="customer">requires parameters name,surrname adress and contact type</param>
        /// <returns>newly created customer</returns>
        // POST api/customer
        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            using (var db = new LiteDatabase(@"myLiteDb.db"))
            {
                _logger.LogInformation("Inserting new customer at {RequestTime}", DateTime.Now);
                var _context = db.GetCollection<Customer>();
                _context.Insert(customer);
                return CreatedAtAction("Post", new Customer { Id = customer.Id }, customer);
                

            }
        }
        /// <summary>
        /// update of specified customer, given id needs to be the same as the id in body
        /// </summary>
        /// <param name="id">GUID type id of customer that is going to be changed</param>
        /// <param name="customer">requires parameters GUID id,name,surrname adress and contact type</param>
        /// <returns>if id's are the same, No Content status is returned, otherwise Bad request is returned</returns>
        // PUT api/customer/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Customer customer)
        {

            using (var db = new LiteDatabase(@"myLiteDb.db"))
            {
                _logger.LogInformation("Updating customer with {ID} at {RequestTime}", id, DateTime.Now);
                if (id != customer.Id)   // we expect we don't want to change the id of existing item
                {
                    _logger.LogWarning("Updating customer with {ID} was not successful, id's are not matching at {RequestTime}",id, DateTime.Now);
                    return BadRequest();
                }
                var _context = db.GetCollection<Customer>();
                _logger.LogInformation("Updating customer with {ID} was successful at {RequestTime}", id, DateTime.Now);
                _context.Update(customer);
                return NoContent();


            }

        }
        /// <summary>
        /// delete specified customer
        /// </summary>
        /// <param name="id">GUID id of customer that we want to delete</param>
        /// <returns>Not found status if customer with that id does not exist, otherwise returns customer that is successfuly deleted</returns>
        // DELETE api/customer/5
        [HttpDelete("{id}")]
        public ActionResult<Customer> Delete(Guid id)
        {
            using (var db = new LiteDatabase(@"myLiteDb.db"))
            {
                _logger.LogInformation("Deleting customer with {ID} at {RequestTime}", id, DateTime.Now);
                var res = db.GetCollection<Customer>().FindById(id);
                if(res == null)
                {
                    _logger.LogWarning("Deleting customer with {ID} was not successful, customer was not found at {RequestTime}", id, DateTime.Now);

                    return NotFound();
                }
                var _context = db.GetCollection<Customer>();
                _logger.LogInformation("Deleting customer with {ID} was successful at {RequestTime}", id, DateTime.Now);

                _context.Delete(res.Id);
                return res;


            }


        }
    }
}
