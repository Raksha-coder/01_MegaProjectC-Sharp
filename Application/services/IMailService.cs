using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.services
{
    public interface IMailService
    {
        string sendData(EmailData data,string user,string pass);

        public void SendForgotPasswordLink(string email, string name, string id);
    }
}
