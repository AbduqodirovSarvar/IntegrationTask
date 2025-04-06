using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configurations
{
    public class Filter
    {
        public string? SearchingText { get; set; }
        public bool Ascending { get; set; } = false;
    }
}
