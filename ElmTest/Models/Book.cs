﻿using System;
using System.Collections.Generic;

namespace Migrations.Models
{
    public class Book
    {
        public long BookId { get; set; }
        public string BookInfo { get; set; } = null!;
        public DateTime LastModified { get; set; }
    }
}
