using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Contracts.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Rolename { get; set; }
        public int OperationId { get; set; }
        public string[] Operations { get; set; }
        public DateTime? LastPasswordChange { get; set; }
    }
}
