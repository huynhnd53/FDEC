using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastDeliveryEC.Models
{
    public class OrderModel
    {
        [Required]
        public int CreaterID { get; set; }
        [Required]
        public int AddressFrom { get; set; }
        [Required]
        public int AddressOrder { get; set; }
        [Required]
        public string NumberPhoneOrder { get; set; }
        [Required]
        public int WeightID { get; set; }
        [Required]
        public float Cash { get; set; }
    }
}
