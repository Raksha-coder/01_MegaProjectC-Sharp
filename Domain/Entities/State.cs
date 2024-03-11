using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        public string StateName { get; set; }

        public ICollection<User> User { get; set; }

    }
}
