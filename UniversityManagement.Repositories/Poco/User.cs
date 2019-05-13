using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UniversityManagement.Repositories.Poco
{
    public class User:BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [ConcurrencyCheck]
        public string UserName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        [NotMapped]
        public string Avatar { get; }
        [NotMapped]
        public string FullName { get => FirstName + ' ' + LastName; }
    }
}
