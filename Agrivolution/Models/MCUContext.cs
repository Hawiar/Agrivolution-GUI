using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Agrivolution.Models
{
    public class MCUContext : DbContext
    {
        public MCUContext() : base("Agrivolution")
        { }
        public DbSet<MCU> MCUs { get; set; }
    }
}