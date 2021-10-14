using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Admin : IEntity
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string Password { get; set; }
    }
}
