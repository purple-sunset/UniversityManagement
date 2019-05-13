using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniversityManagement.Repositories.Poco
{
    public class Role
    {
        [Required]
        [MaxLength(50)]
        [ConcurrencyCheck]
        public string Name { get; set; }
    }
}
