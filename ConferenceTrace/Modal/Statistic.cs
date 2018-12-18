using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrace.Modal
{

    /// <summary>
    /// 会议区间
    /// </summary>
    public class Statistic
    {
        private int id;

        private int total;

        private int unused;

        private int used;


        public int ID {
            get { return id; }
            set { id = value; }
        }

        public int Total
        {
            get { return total; }
            set { total = value; }
        }

        public int Unused
        {
            get {

                return  this.Total-this.Used;
            }
            set { unused = value; }
        }

        public int Used
        {
            get
            {
                int sum = 0;
                foreach (var item in Meetings)
                {
                    sum += item.Duration;
                }
                return sum;

            }
            set { used = value; }
        }


        public List<Meeting> Meetings { get; set; }


        public void Add(Meeting meeting)
        {
            Meetings.Add(meeting);
        }

    }
}
