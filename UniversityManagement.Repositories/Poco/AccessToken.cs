﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniversityManagement.Repositories.Poco
{
    public class AccessToken:BaseEntity
    {
        [ConcurrencyCheck]
        public string Key { get; set; }
        public string Type { get; set; }
        public string SubjectId { get; set; }
        public string ClientId { get; set; }
        public DateTime? Expiration { get; set; }
        public string Data { get; set; }
    }
}