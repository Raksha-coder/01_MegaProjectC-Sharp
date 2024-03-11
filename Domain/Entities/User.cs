using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities
{
    public class User
    {

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string? UserType { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; } // for decrypt

        [Phone]
        public string Mobile { get; set; }

        public string Address { get; set; }

        [RegularExpression(@"^\d{6}$", ErrorMessage = "Zip code must be a 6-digit integer.")]
        public int ZipCode { get; set; }


        //image
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }

        //state
        public int StateId { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }

        
        //country
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }
    };


   
}
