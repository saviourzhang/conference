using ConferenceTrace.Modal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrace
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Tool
    {

        /// <summary>
        /// 加载会议数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<Meeting> LoadMeetinglist(string path)
        {
            List<Meeting> list = new List<Meeting>();


            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line = "";
            int i = 0;
            while ((line = sr.ReadLine()) != null)
            {
                i++;
                Meeting meeting = new Meeting();

                int pos = line.LastIndexOf(" ");
                string name = line.Substring(0, pos);
                string time = line.Substring(pos + 1);
                if (time.Contains("min"))
                {
                    time = time.Replace("min", "");
                }
                else
                {
                    time = "5";
                }
                meeting.ID = i;
                meeting.Name = name;
                meeting.Duration = int.Parse(time);
                meeting.Desc = line;
                list.Add(meeting);
            }

            return list.OrderBy(o => o.Duration).ToList();

        }

        /// <summary>
        /// 加载Session
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<Trace> LoadSessions(string path)
        {

            DataSet ds = new DataSet();
            ds.ReadXml(path);


            List<Session> sessions = new List<Session>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Session session = new Session();
                DataRow dr = ds.Tables[0].Rows[i];
                session.Name = dr["name"].ToString();
                session.Start = dr["begin"].ToString();
                session.End = dr["end"].ToString();
                sessions.Add(session);
            }

            int track = int.Parse(ConfigurationManager.AppSettings["trace"].ToString());
            int sessioncount = sessions.Count;

            List<Trace> tracelist = new List<Trace>();

            for (int i = 0; i < track; i++)
            {
                Trace trace = new Trace();
                List<Statistic> staticlist = new List<Statistic>();
                for (int j = 0; j < sessions.Count; j++)
                {
                    Statistic stat = new Statistic();
                    stat.ID = j;
                    stat.Total = (int)(DateTime.Parse(sessions[j].End) - DateTime.Parse(sessions[j].Start)).TotalMinutes;
                    stat.Meetings = new List<Meeting>();
                    staticlist.Add(stat);
                }
                trace.ID = i;
                trace.Statis = staticlist;

                tracelist.Add(trace);
            }

            return tracelist;
        }

        /// <summary>
        /// 会议分配
        /// </summary>
        /// <param name="tracelist"></param>
        /// <param name="meetinglist"></param>
        public static List<Trace> Shedule(List<Trace> tracelist, List<Meeting> meetinglist)
        {

            Arrange(tracelist, meetinglist[meetinglist.Count - 1]);

            meetinglist.Remove(meetinglist[meetinglist.Count - 1]);

            if (meetinglist.Count > 0)
            {
                return Shedule(tracelist, meetinglist);
            }
            else
            {
                return tracelist;
            }

        }


        /// <summary>
        /// 安排会议到某一个点
        /// </summary>
        /// <param name="tracelist"></param>
        /// <param name="meet"></param>
        public static void Arrange(List<Trace> tracelist, Meeting meet)
        {
            for (int i = 0; i < tracelist.Count; i++)
            {
                int sesssioncount = tracelist[i].Statis.Count;
                for (int j = 0; j < sesssioncount; j++)
                {
                    if (tracelist[i].Statis[j].Unused >= meet.Duration)
                    {
                        tracelist[i].Statis[j].Meetings.Add(meet);

                        return;
                    }
                }
            }
        }


        /// <summary>
        /// 会议行程输出
        /// </summary>
        /// <param name="tracelist"></param>
        public static void PrintList(List<Trace> tracelist)
        {
            string hour, minute;

            for (int i = 0; i < tracelist.Count; i++)
            {
                Console.WriteLine("Track " + (i + 1).ToString());

                for (int j = 0; j < tracelist[i].Statis.Count; j++)
                {

                    if (j == 0)
                    {
                        int totalminutes = 9 * 60;
                        for (int k = 0; k < tracelist[i].Statis[j].Meetings.Count; k++)
                        {
                            hour = (totalminutes / 60).ToString();
                            minute = ("0" + (totalminutes % 60));
                            minute = minute.Substring(minute.Length - 2, 2);
                            Console.WriteLine(hour + ":" + minute + "AM" + " " + tracelist[i].Statis[j].Meetings[k].Desc);
                            totalminutes += tracelist[i].Statis[j].Meetings[k].Duration;

                        }
                        Console.WriteLine("12:00PM Lunch");
                    }
                    if (j == 1)
                    {
                        int totalminutes = 1 * 60;
                        for (int k = 0; k < tracelist[i].Statis[j].Meetings.Count; k++)
                        {
                            hour = (totalminutes / 60).ToString();
                            minute = ("0" + (totalminutes % 60));
                            minute = minute.Substring(minute.Length - 2, 2);
                            Console.WriteLine(hour + ":" + minute + "PM" + " " + tracelist[i].Statis[j].Meetings[k].Desc);
                            totalminutes += tracelist[i].Statis[j].Meetings[k].Duration;
                        }
                        Console.WriteLine("05:00PM Networking Event");
                    }
                }

            }
        }
    }
}
