﻿using Domain.Model.ServiceFacades;
using Domane.Model;
using Domane.Model.ServiceFacades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Micro.CustomerBLService
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repo;

        public CustomerService(IRepository<Customer> repo) {
            _repo = repo;
        }
        public Customer Add(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void Delete(int customerId)
        {
            throw new NotImplementedException();
        }

        public Customer Get(int customerId)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
