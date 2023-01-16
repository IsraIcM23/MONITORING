using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WACO
{
    public class Consumption
    {
        public string Period { get; set; }
        public int Lecture { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaidDate { get; set; }

        public int initialLecture = 10;

        public Consumption(string date, int lecture)
        {
            Period = date;
            Lecture = lecture;  
            IsPaid = false;
            PaidDate = null;
        }

        public void Pay()
        {
            IsPaid = true;
            PaidDate = DateTime.Now;
        }
    }
}
