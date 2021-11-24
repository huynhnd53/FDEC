using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class OrdersModel
    {
        public int ID { get; set; }
        public int CreaterID { get; set; }
        public int ShipperID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime SuccessDate { get; set; }
        public int AddressFrom { get; set; }
        public int AddressOrder { get; set; }
        public int StatusOrder { get; set; }
        public int ShopID { get; set; }
        public string NumberPhoneOrder { get; set; }
        public float Cash { get; set; }
        public int CategoryID { get; set; }
        public string Size { get; set; }
        public float WeightID { get; set; }
        public bool IsActive { get; set; }
    }

}
