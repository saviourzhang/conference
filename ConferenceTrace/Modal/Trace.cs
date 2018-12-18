using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrace.Modal
{

    /// <summary>
    /// 会议跟踪
    /// </summary>
    public class Trace
    {
        public int ID { get; set; }

        public List<Statistic> Statis { get; set; }
    }
}
