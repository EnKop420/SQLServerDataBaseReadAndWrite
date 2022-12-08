using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerDataBaseReadAndWrite.Codes
{
    internal class PatientModel
    {
        public string? FullName { get; set; }
        public DoctorModel? Doctor { get; set; }
    }
}
