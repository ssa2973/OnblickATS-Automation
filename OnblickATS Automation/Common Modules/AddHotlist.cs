using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.IO;

namespace OnblickATS_Automation
{
    public class AddHotlist
    {
        public void Add_New_Hotlist(IWebDriver driver)
        {
            HotlistPage hotlist = new HotlistPage(driver);
            StackTrace stackTrace = new StackTrace();
            string callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            switch (callingMethod)
            {
                case "AddHotListProfiles_Automation":
                {
                    Random random = new Random(DateTime.Now.Millisecond);
                    var currentDirectory = Directory.GetCurrentDirectory();
                    var filePath = Path.Combine(currentDirectory, "..", "..", "..", "Test Data", "Sample Hotlist Candidates", "sample_hotlist_candidates.json");
                    string json = File.ReadAllText(filePath);
                    dynamic data = JsonConvert.DeserializeObject(json);
                    int count = data.HOTLIST_CANDIDATES.Count;
                    int index = random.Next(0, count - 1);
                    var Hotlist = data.HOTLIST_CANDIDATES[index];
                    hotlist.processResumes((string)Hotlist.location, designation: (string)Hotlist.designation, firstName: (string)Hotlist.firstName, lastName: (string)Hotlist.lastName, email: (string)Hotlist.email);
                }
                break;
            }
        }
    }
}
