using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vidly.Models;
using vidly.Controllers;
using vidly.Dtos;
using AutoMapper;
using AutoMapper.Mappers;
using System.Data.Entity;

namespace vidly.Controllers.Api
{
    
    public class CustomerController : ApiController
    {
        IMapper mapper;
        ApplicationDbContext _context;
        public CustomerController()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customers, CustomerDto>();
                cfg.CreateMap<CustomerDto, Customers>();
                cfg.CreateMap<MembershipType, MembershipTypeDto>();
            });
            mapper = config.CreateMapper();
            _context = new ApplicationDbContext();
        }

        //[GET] "/api/customers"
        public IEnumerable<CustomerDto> GetCustomers()
        { 
            var customerdtos = _context.Customers.Include(c => c.MembershipType).Select(mapper.Map<Customers, CustomerDto>);
            return customerdtos;
        }

        //[GET] "/api/customer/id"
        public IHttpActionResult getCustomers(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                // throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();

            //return mapper.Map<Customers,CustomerDto>(customer);
            return Ok(mapper.Map<Customers, CustomerDto>(customer));
        }

        //[POST] "/api/customer"
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerdto)
        {
            if (!ModelState.IsValid)
                //  throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();

            Customers customer = mapper.Map<CustomerDto,Customers>(customerdto);

            //Adding customer to the database
            _context.Customers.Add(customer);
            _context.SaveChanges();


            return Created(new Uri(Request.RequestUri + "/"+"id"),customer) ;
        }

        //[PUT] "/api/customer/id"
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerdto)
        {
            if (!ModelState.IsValid)
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();

            //Updating CustomerInDb with CustomerInParameter
            mapper.Map<CustomerDto, Customers>(customerdto,customerInDb);
            _context.SaveChanges();
            return Ok();
        }

        //[DELETE] "/api/customer/id"
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            
           
                //Deleting customer by id
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return Ok();
        }

    }
}
