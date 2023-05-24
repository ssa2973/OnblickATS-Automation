using OpenQA.Selenium;
using System.Diagnostics;

namespace OnblickATS_Automation
{
    public class Login
    {
        public void Employer_Login(IWebDriver driver)
        {
            LoginPage loginPage = new LoginPage(driver);
            StackTrace stackTrace = new StackTrace();
            string callingMethod = stackTrace.GetFrame(1).GetMethod().Name;
            switch (callingMethod)
            {
                case "CreateNewRequirement_Automation":
                {
                    loginPage.validLogin(LoginCreds.Instance.HR_Email, LoginCreds.Instance.HR_Pwd);
                    loginPage.changeRole("Recruiter");
                }
                break;

                case "AddHotListProfiles_Automation":
                {
                    loginPage.validLogin(LoginCreds.Instance.HR_Email, LoginCreds.Instance.HR_Pwd);
                    loginPage.changeRole("Account Manager");
                }
                break;
            }
        }
    }
}
