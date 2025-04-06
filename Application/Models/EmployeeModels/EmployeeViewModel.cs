using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.EmployeeModels
{
    public class EmployeeViewModel
    {
        public string PayrollNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
    }
}
