using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerDataBaseReadAndWrite.Codes
{
    internal class DoctorModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? SpecialtyId { get; set; }
        public string? SpecialtyName { get; set; }
    }
}
