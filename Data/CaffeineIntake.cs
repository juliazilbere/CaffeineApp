using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data;

namespace Data
{
    public class CaffeineIntake
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset dt { get; set; }
        public int hrs { get; set; }
        public double CaffeineIntakeNum { get; set; }
    }
}
