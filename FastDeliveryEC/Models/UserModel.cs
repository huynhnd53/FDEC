using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastDeliveryEC.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Userame { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int Role { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
      
    }
}
