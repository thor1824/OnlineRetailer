using Or.Domain.Model;
using Or.Domain.Model.SharedModels;
using System;

namespace Or.Micro.Customers.Models
{
    public class CustomerConverter : IConverter<Customer, CustomerDto>
    {
        public Customer Convert(CustomerDto model)
        {
            return new Customer
            {
                Name = model.Name,
                Email = model.Email,
                CustomerId = model.CustomerId,
                BillingAddress = model.BillingAddress,
                CreditStanding = model.CreditStanding,
                ShippingAddress = model.ShippingAddress,
                Phone = model.Phone
            };
        }


        public CustomerDto Convert(Customer model)
        {
            return new CustomerDto
            {
                Name = model.Name,
                Email = model.Email,
                CustomerId = model.CustomerId,
                BillingAddress = model.BillingAddress,
                CreditStanding = model.CreditStanding,
                ShippingAddress = model.ShippingAddress,
                Phone = model.Phone
            };
        }
    }
}
