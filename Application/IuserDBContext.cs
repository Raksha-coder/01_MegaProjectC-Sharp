using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IuserDBContext
    {
        DbSet<Country> CountryTable { get; }

        DbSet<State> StateTable { get; }

        DbSet<User> UserTable { get; }


        Task SaveChangesAsync();
    }
}
