using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.DTO.Request
{
    public class Register
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public bool Salaried { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HiredDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Status { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string Address { get; set; }
        public IFormFile?  Image { get; set; }
        public string? AssignedTo { get; set; }
    }

}
