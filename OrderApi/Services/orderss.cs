using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Or.Micro.Orders.Services
{
    public class orderss
    {

        public void SubmitOrder() {
            //mark order as submitted/Validating:Customer on db

            //Call PublishCustomeToValidation(productId, CustomerId)
        }

        public void PublishCustomeToValidation() {
            //publish Request for customer Validation (productId, CustomerId)
        }

        public void HandleCustomerResult() {
            //if rejected
            ///Call PublishOrderStatus{OrderId, CustomerId}

            //else
            ///mark order as Validating on db
            ///Call PublishCustomeToValidation(productId, CustomerId)
        }

        public void PublishInStockValidation() { }

        public void HandleInstockResult() { }

        public void MarkAsCompleted() { }

        public void PublishOrderStatus() {
            // rejected
            ///publich notify Customer

            //Completed, Shipped, Canceled
            ///publich notify Customer
            ///publich OrderChange

        }


    }

    public class prodss
    {
        public void HandleInstockValidation() {
            //check if item and quantity is instock

            //call PublishInstockResult
        }
        public void PublishInstockResult() {
            //publich InstockResult {Accepted | Rejected}

        }

        public void HandleOrderStatusChange() {
            //Completed
            ///remove from stock
            ///add to reserved
            
            //Cancelled
            ///Restore
            
            //Shipped
            ///Remove from reserv
        }


    }

    public class Custss
    {
        public void HandleCustomeToValidation() {
            //check credit reading

            //call PublishCustomerResult
        }
        public void PublishCustomerResult() {
            //publich Customerresult{Accepted | Rejected}

        }
        public void NotifyCustomer() {
            //cw sending mail to {customer email}
        }


    }
}
