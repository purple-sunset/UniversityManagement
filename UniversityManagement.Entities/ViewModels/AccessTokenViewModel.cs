﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityManagement.Entities.ViewModels
{
    public class AccessTokenViewModel : BaseViewModel
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public string SubjectId { get; set; }
        public string ClientId { get; set; }
        public DateTime? Expiration { get; set; }
        public string Data { get; set; }
    }
}
