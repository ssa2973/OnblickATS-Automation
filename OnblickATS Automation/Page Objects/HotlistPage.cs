using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace OnblickATS_Automation
{
    public class HotlistPage
    {
        private IWebDriver driver;
        WaitHelpers wait = new WaitHelpers();
        private By AddCandidateH5 = By.XPath("//h5[normalize-space()='Add Candidate']"); public By AddCandidateH5Id() { return AddCandidateH5; }
        private By AddCandidateSaveBtn = By.XPath("//h5[normalize-space()='Add Candidate']/following::button[normalize-space()='Save'][1]"); public By AddCandidateSaveBtnId() { return AddCandidateSaveBtn; }
        private By AddHotlistCandidate = By.XPath("//button[normalize-space()='Add Hotlist Candidate']"); public By AddHotlistCandidateId() { return AddHotlistCandidate; }
        private By CreatingContact = By.XPath("//p[normalize-space()='Creating contact...']"); public By CreatingContactId() { return CreatingContact; }
        private By FetchingInfo = By.XPath("//p[normalize-space()='Fetching Information...']"); public By FetchingInfoId() { return FetchingInfo; }
        private By SearchResult = By.XPath("//li[@role='treeitem'][1]"); public By SearchResultId() { return SearchResult; }
        public HotlistPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        //page object factory

        [FindsBy(How = How.XPath, Using = "//button[normalize-space()='Add Hotlist Candidate']")]
        private IWebElement addHotlistCandidate;

        [FindsBy(How = How.XPath, Using = "//h5[normalize-space()='Add Candidate']/following::input[@type='file']")]
        private IWebElement uploadResume;

        [FindsBy(How = How.Id, Using = "fName")]
        private IWebElement inputFirstName;

        [FindsBy(How = How.Id, Using = "lName")]
        private IWebElement inputLastName;

        [FindsBy(How = How.Id, Using = "email")]
        private IWebElement inputEmail;

        [FindsBy(How = How.XPath, Using = "//label[normalize-space()='Location']/following::span[@class='select2-selection__rendered'][1]")]
        private IWebElement inputLocation;

        [FindsBy(How = How.XPath, Using = "//label[normalize-space()='Location']/following::span[@class='select2-selection__rendered'][1]/following::input[@role='textbox']")]
        private IWebElement locationTextbox;

        [FindsBy(How = How.Id, Using = "designation")]
        private IWebElement inputDesignation;

        [FindsBy(How = How.XPath, Using = "//h5[normalize-space()='Add Candidate']/following::button[normalize-space()='Save'][1]")]
        private IWebElement addCandidateSaveBtn;

        private void checkFirstName(string firstName)
        {
            var _firstName = inputFirstName.GetAttribute("value");
            while (string.IsNullOrEmpty(_firstName)|| _firstName == "First name")
            {
                if (firstName != null)
                {
                    inputFirstName.Clear();
                    inputFirstName.SendKeys(firstName);
                    _firstName = inputFirstName.GetAttribute("value");
                }
            }
        }

        private void checkLastName(string lastName)
        {
            var _lastName = inputLastName.GetAttribute("value");
            while (string.IsNullOrEmpty(_lastName) || _lastName == "Last name")
            {
                if (lastName != null)
                {
                    inputLastName.Clear();
                    inputLastName.SendKeys(lastName);
                    _lastName = inputLastName.GetAttribute("value");
                }
            }
        }

        private void checkEmail(string email)
        {
            var _email = inputEmail.GetAttribute("value");
            while (string.IsNullOrEmpty(_email) || _email == "Email")
            {
                if (email != null)
                {
                    inputEmail.Clear();
                    inputEmail.SendKeys(email);
                    _email = inputEmail.GetAttribute("value");
                }
            }
        }

        private void checkDesignation(string designation)
        {
            var _designation = inputDesignation.GetAttribute("value");
            while (string.IsNullOrEmpty(_designation) || _designation == "Designation")
            {
                if (designation != null)
                {
                    inputDesignation.Clear();
                    inputDesignation.SendKeys(designation);
                    _designation = inputDesignation.GetAttribute("value");
                }
            }
        }

        private void checkLocation(string location)
        {
            var _location = inputLocation.Text;
            while (string.IsNullOrEmpty(_location) || _location == "Search")
            {
                var containsLocation = _location.Contains(location);
                while (!containsLocation)
                {
                    inputLocation.Click();
                    locationTextbox.SendKeys(location);
                    wait.waitForElementToBeVisible(driver, SearchResult);
                    driver.FindElement(SearchResult).Click();
                    _location = inputLocation.Text;
                    containsLocation = _location.Contains(location);
                }

            }
        }

        private void addHotlistCandidates(string filePath, string location, [Optional] string firstName, [Optional] string lastName, [Optional] string email, [Optional] string designation)
        {
            driver.Navigate().GoToUrl(URLs.Instance.Hotlist_URL);
            wait.waitForElementToBeVisible(driver, AddHotlistCandidate);
            addHotlistCandidate.Click();
            wait.waitForElementToBeVisible(driver, AddCandidateH5);
            uploadResume.SendKeys(filePath);
            wait.waitForInvisibilityOfElement(driver, FetchingInfo);
            checkLocation(location);
            checkFirstName(firstName);
            checkLastName(lastName);
            checkEmail(email);
            checkDesignation(designation);
            wait.waitForElementToBeClickable(driver, AddCandidateSaveBtn);
            addCandidateSaveBtn.Click();
            wait.waitForInvisibilityOfElement(driver, CreatingContact);
        }

        public void processResumes(string location, [Optional] string firstName, [Optional] string lastName, [Optional] string email, [Optional] string designation)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var folderPath = Path.Combine(currentDirectory, "..", "..", "..", "Test Data", "Sample Resumes");
            string[] filePaths = Directory.GetFiles(folderPath);

            for (int i = 0; i < filePaths.Length; i++)
            {
                string filePath = filePaths[i];
                string fullPath = Path.GetFullPath(filePath);
                addHotlistCandidates(fullPath, location, designation: designation, firstName: firstName, lastName: lastName, email: email);
            }
        }

    }
}
