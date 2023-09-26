using SizeAdvisor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeAdvisor.Service.Dtos
{
    public class UserForResultDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
    }
}
