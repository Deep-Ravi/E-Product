using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Contracts.Data.Entities
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        [ForeignKey("Operation")]
        public int OperationId { get; set; }
        public virtual Operation Operation { get; set; }
        public DateTime? LastPasswordChange { get; set; }
    }
}
