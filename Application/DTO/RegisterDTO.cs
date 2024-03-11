using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.DTO
{
    public class RegisterDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string? UserType { get; set; }
      
        public DateTime DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }

        public int ZipCode { get; set; }
       
        //image
        public string ImageName { get; set; }

        public IFormFile formFile { get; set; }

        public int StateId { get; set; }

        public int CountryId { get; set; }
    }
}
