using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastDeliveryEC.Models
{
    public class ViewOrderModel
    {
        public string AddressFrom { get; set; }
        public string AddressOrder { get; set; }
        public string Status { get; set; }
        public float Cash { get; set; }
    }
}
