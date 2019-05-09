using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniversityManagement.Repositories.Poco
{
    public class Subject:BaseEntity
    {
        [Required]
        [ConcurrencyCheck]
        public string SubjectCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string SubjectName { get; set; }
    }
}
