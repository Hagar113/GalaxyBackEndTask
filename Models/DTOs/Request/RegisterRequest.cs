﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Request
{
    public class RegisterRequest
    {
        
        public string? userName { get; set; }
        public string? Email { get; set; }
        public string? Mopile { get; set; }
        public string? Password { get; set; }
        public string? token { get; set; }
    }
}
