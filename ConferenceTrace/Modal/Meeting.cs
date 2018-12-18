using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrace.Modal
{
    /// <summary>
    /// 会议信息
    /// </summary>
    public class Meeting
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int Duration { get; set; }

        public string Desc { get; set; }



    }
}
