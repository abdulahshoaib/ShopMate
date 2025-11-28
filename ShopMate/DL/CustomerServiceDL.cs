using ShopMate.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.DL
{
    class CustomerServiceDL
    {
        public CustomerServiceDL() {
            
        }

        public bool AddCustomer(CustomerDTO cDTO)
        {
            // TODO: Add DB call to add new customer to the customers table
            return true;
        }

        public bool RemoveCustomer(int ID)
        {
            // TODO: Add DB call to remove customer to the customers table
            return true;
        }

        public bool UpdateCustomer(CustomerDTO cDTO)
        {
            // TODO: Add DB to update the incoming customer's data
            return true; 
        }
    }
}