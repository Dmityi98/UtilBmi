using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilBmi.Models
{
    public class BmiModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double Bmi { get; set; }
    }
}
