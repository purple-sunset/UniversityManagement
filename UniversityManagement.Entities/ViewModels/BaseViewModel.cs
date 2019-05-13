using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityManagement.Entities.ViewModels
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
