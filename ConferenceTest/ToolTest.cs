using System;
using System.Collections.Generic;
using ConferenceTrace;
using ConferenceTrace.Modal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConferenceTest
{
    [TestClass]
    public class ToolTest
    {
        [TestMethod]
        public void LoadMeetinglistTest()
        {
            int expected_meetingcount = 19;


            List<Meeting> list= Tool.LoadMeetinglist(AppDomain.CurrentDomain.BaseDirectory + "\\meetinglist.txt");

            int actual_meetingcount = list.Count;

            Assert.AreEqual(expected_meetingcount, actual_meetingcount);


        }
    }
}
