using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Application.Responses;
using System.Runtime.ConstrainedExecution;
using System.IO;
using Application.DTO;
using Application.Image;
using Application.Encryption;
using Application.services;

namespace Application.userCQRS.command
{
    public class postRegistration : IRequest<UserResponse>
    {
       
       public RegisterDTO registerdto { get; set; }

    }

    public class postRegisterHandler : IRequestHandler<postRegistration, UserResponse>
    {
        public readonly IuserDBContext _context;
        public readonly ImageService _img;
        private readonly string _pepper;  //password
        private readonly int _iteration = 3; //password
        public readonly IMailService _email;


        public postRegisterHandler(IuserDBContext context,ImageService img,IMailService email)
        {
            _context = context;
            _img = img;
            _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
            _email= email;
        }

        public async Task<UserResponse> Handle(postRegistration request, CancellationToken cancellationToken)
        {

            try
            {

                //for image
                if(request.registerdto.formFile!= null)
                {
                    var fileResult = _img.SaveImage(request.registerdto.formFile);
                    if (fileResult.Item1 == 1)
                    {
                        request.registerdto.ImageName = fileResult.Item2; //actual value of image with format form
                    }
                    else
                    {
                        return new UserResponse()
                        {
                            Status = 400,
                            Message = fileResult.Item2,
                            Error = null
                        };
                    }

                }


                EncryptPassUser obj = new EncryptPassUser();
        

                var generatedPass = Generate8Digitpassword();

                var newuser = new User()
                {
                    FirstName = request.registerdto.FirstName,
                    LastName = request.registerdto.LastName,
                    Email = request.registerdto.Email,
                    UserType = request.registerdto.UserType,
                    DateOfBirth = request.registerdto.DateOfBirth,
                    Username = GenerateUsername(request.registerdto.FirstName,
                    request.registerdto.LastName,
                    request.registerdto.DateOfBirth),
                    PasswordSalt = obj.GenerateSalt(),
                    Mobile = request.registerdto.Mobile,
                    Address = request.registerdto.Address,
                    ZipCode = request.registerdto.ZipCode,
                    ImageName= request.registerdto.ImageName, //img
                    FormFile = request.registerdto.formFile,  //This is not necessary
                    CountryId = request.registerdto.CountryId,
                    StateId = request.registerdto.StateId,
                };

             

                newuser.Password = obj.ComputeHash(generatedPass,
                                               newuser.PasswordSalt,
                                               _pepper,
                                               _iteration);        //password

                await _context.UserTable.AddAsync(newuser,cancellationToken); //password
                await _context.SaveChangesAsync();


                //send email to user along with password and username 
                //de-crypt it 


                var maildata = new EmailData()
                {
                    //take email from the user 
                    EmailToId = request.registerdto.Email,
                    //user name
                    EmailToName = request.registerdto.FirstName,
                    //subject default
                    EmailSubject = "You Have Successfully Registered to Our Website",
                    EmailBody = "Thank You!!:>",
                };

                var sended = _email.sendData(maildata,newuser.Username,generatedPass);
                
                
                //it will return yes and no
                //check email has sent successfully or  not
                var emailresponse = "no";
                if(sended == "yes")
                {
                    emailresponse = "email sended successfully";
                }


                return new UserResponse()
                {
                    Status = 200,
                    Message = "Data added and" + emailresponse,
                    Error = null
                };

            }catch(Exception ex)
            {
                return new UserResponse()
                {
                    Status = 400,
                    Message = "error",
                    Error = ex.Message
                };
            }
        }


        //generate 8 digit password
        public string Generate8Digitpassword()
        {
            Random ran = new Random();
            var otp = ran.Next(11111, 99999);
            return otp.ToString();
        }

        public string GenerateUsername(string firstname,string lastname,DateTime dob)
        {
            var user = "EC_" + lastname.ToUpper() + char.ToUpper(firstname[0]) + dob.ToString("ddMMyy");
            return user;
        }
    }


 
   


}
