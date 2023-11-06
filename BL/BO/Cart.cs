using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Cart
    {
        public string CustomersName { get; set; }
        public string CustomersEmail { get; set; }
        public string CustomersAddress { get; set; }
        public List<OrderItem> ListOfItems { get; set; }
        public double TotalPriceOfCart { get; set; } = 0;
    }
}
