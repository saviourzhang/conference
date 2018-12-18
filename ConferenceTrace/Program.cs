using ConferenceTrace.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrace
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Meeting> meetinglist = Tool.LoadMeetinglist(AppDomain.CurrentDomain.BaseDirectory + "\\meetinglist.txt");


            List<Trace> tracelist = Tool.LoadSessions(AppDomain.CurrentDomain.BaseDirectory + "\\sessions.xml");


            int meeting_sum = meetinglist.Sum(o => o.Duration);

            int trace_sum = 0;
            foreach (var item in tracelist)
            {
                trace_sum += item.Statis.Sum(o => o.Total);
            }
            if (meeting_sum > trace_sum)
            {
                return;
            }

            Tool.Shedule(tracelist, meetinglist);


            Tool.PrintList(tracelist);

            Console.ReadLine();
        }
    }
}
