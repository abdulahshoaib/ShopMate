using ShopMate.DTOs;
using ShopMate.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.BL
{
    public class CustomerServiceBL
    {
        CustomerServiceDL csDL;

        public CustomerServiceBL() {
            csDL = new CustomerServiceDL();
        }

        public bool AddCustomer(CustomerDTO customerDTO)
        {
            return csDL.AddCustomer(customerDTO);
        }

        public bool RemoveCustomer(int ID)
        {
            return csDL.RemoveCustomer(ID);
        }

        public bool UpdateCustomer(CustomerDTO customerDTO)
        {
            return csDL.UpdateCustomer(customerDTO);
        }
    }
}
