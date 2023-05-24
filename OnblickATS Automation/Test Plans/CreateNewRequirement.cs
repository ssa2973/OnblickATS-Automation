using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace OnblickATS_Automation
{
    [TestFixture]
    public class CreateNewRequirement
    {
        ChromeOptions options = new ChromeOptions();
        private IWebDriver driver;


        [Test]
        public void CreateNewRequirement_Automation()
        {
            Login login = new Login();
            CreateRequirement requirement = new CreateRequirement();

            login.Employer_Login(driver);
            requirement.Create_New_Requirement(driver);
        }

        [SetUp]
        public void SetUp()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string downloadPath = Path.Combine(currentDirectory, "..", "..", "..", "Downloads");
            Directory.CreateDirectory(downloadPath);
            options.AddUserProfilePreference("download.default_directory", downloadPath);
            options.AddArguments("--disable-extensions"); // Disable any extensions
            options.AddArguments("--disable-popup-blocking"); // Disable popup blocking
            options.AddArguments("--start-maximized"); // Maximize the window
            options.AddArguments("--foreground-flash-enabled"); // Bring window to foreground
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [OneTimeTearDown]
        public void Quit()
        {
            driver.Quit();
        }

    }
}
