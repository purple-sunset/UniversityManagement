using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UniversityManagement.Repositories.Poco
{
    public class Student: BaseEntity
    {
        [Required]
        [Range(0, 4)]
        public float Cpa { get; set; }

        [Required]
        [MaxLength(50)]
        public string Program { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
