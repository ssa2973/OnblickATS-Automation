using OpenQA.Selenium;
using System.Diagnostics;

namespace OnblickATS_Automation
{
    public class CreateRequirement
    {
        public void Create_New_Requirement(IWebDriver driver)
        {
            NewRequirementPage requirement = new NewRequirementPage(driver);
            StackTrace stackTrace = new StackTrace();
            string callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            switch(callingMethod)
            {
                case "CreateNewRequirement_Automation":
                {
                    requirement.processJobs();
                }
                break;
            }
        }
    }
}
