using Application.services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MailService : IMailService
    {
        
        public string sendData(EmailData data,string username,string password)   //manually bhi data daal sakte h
        {
            try
            {

                if (data != null)
                {

                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("otherjaiswal20@gmail.com");

                    mail.To.Add(data.EmailToId);
                    mail.Subject = "Your Login Credentials";

                    mail.Body = "<h1>Dear " + data.EmailToName + ",</h1><br><h3>Your login credentials. <br><br>Below you will find your login information: <h2><br>UseID: " + username + " <br> Password: " + password + "</h3></h3><br><br> <h3> Visit Application for Login into your account <a style='color:blue' href='https://ms.stagingsdei.com:9201/#/'>Red Vest Supply </a>. </h3><br><br><h4> Thank you for choosing Us.</h4>";

                    mail.IsBodyHtml = true;

                    SmtpServer.Port = 587;

                    SmtpServer.Credentials = new System.Net.NetworkCredential("otherjaiswal20@gmail.com", "fpsq gnnk puya ojyl");
                    //jo email send krega.
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);


                    return "yes";
                }
                else
                {
                    return "No";
                }

            }
            catch (Exception ex)
            {
                return "No";
            }
        }



        public void SendForgotPasswordLink(string email, string name, string id)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("otherjaiswal20@gmail.com");

                mail.To.Add(email);  //client mail which we will get

                mail.Subject = "Reset Your Account Password";

                mail.Body = "<h1>Dear " + name + ",</h1><br><h3>We hope this email finds you well. It seems that you have forgotten your password for your account with Car Buy and Sell. Don't worry, we're here to assist you in regaining access to your account<br><br>To reset your password.....<a style='color:red' href='http://localhost:4200/changePassword/" + id + "'> Click Here </a></h3><br><br><h4> Thank you for choosing RED Vest Supply. We appreciate your cooperation. We look forward to providing you with a rewarding and enjoyable experience on our platform.</h4>";

                mail.IsBodyHtml = true;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("otherjaiswal20@gmail.com", "fpsq gnnk puya ojyl");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

    

}
}
