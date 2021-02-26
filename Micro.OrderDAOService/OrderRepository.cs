﻿using Domain.Storage;
using Domane.Model;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderApi.Data
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly RetailContext db;

        public OrderRepository(RetailContext context)
        {
            db = context;
        }

        Order IRepository<Order>.Add(Order entity)
        {
            if (entity.Date == null)
                entity.Date = DateTime.Now;

            var newOrder = db.Orders.Add(entity).Entity;
            db.SaveChanges();
            return newOrder;
        }

        void IRepository<Order>.Edit(Order entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        Order IRepository<Order>.Get(int id)
        {
            return db.Orders.FirstOrDefault(o => o.OrderId == id);
        }

        IEnumerable<Order> IRepository<Order>.GetAll()
        {
            return db.Orders.ToList();
        }

        void IRepository<Order>.Remove(int id)
        {
            var order = db.Orders.FirstOrDefault(p => p.OrderId == id);
            db.Orders.Remove(order);
            db.SaveChanges();
        }
    }
}
