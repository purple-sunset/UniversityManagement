using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UniversityManagement.Repositories.Poco
{
    public class Teacher:BaseEntity
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
