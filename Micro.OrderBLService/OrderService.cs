﻿using Domain.Model.ServiceFacades;
using Domane.Model;
using Domane.Model.ServiceFacades;
using System;
using System.Collections.Generic;

namespace Micro.OrderBLService
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repo;

        public OrderService(IRepository<Order> repo)
        {
            _repo = repo;
        }
        public Order Add(Order order)
        {
            throw new NotImplementedException();
        }

        public void ChangeStatus(int OrderId, OrderStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public Order Get(int orderId)
        {
            throw new NotImplementedException();
        }

        public IList<Order> GetAllByCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
