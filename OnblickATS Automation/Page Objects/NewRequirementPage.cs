using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace OnblickATS_Automation
{
    public class NewRequirementPage
    {
        private IWebDriver driver;
        WaitHelpers wait = new WaitHelpers();
        private By CheckboxOnblick = By.XPath("//label[@for='onBlick']"); public By CheckboxOnblickId() { return CheckboxOnblick; }
        private By CreateNewRequirement = By.XPath("//a[normalize-space()='Create New Requirement']"); public By CreateNewRequirementId() { return CreateNewRequirement; }
        private By CreateRequirementBtn = By.XPath("//button[normalize-space()='Create Requirement']"); public By CreateRequiremenBtnId() { return CreateRequirementBtn; }
        private By CreateRequirementContinueBtn = By.Id("btnContinueJob"); public By CreateRequirementContinueBtnId() { return CreateRequirementContinueBtn; }
        private By CreateRequirementH2 = By.XPath("//h2[normalize-space()='Create Requirement']"); public By CreateRequirementH2Id() { return CreateRequirementH2; }
        private By CreateRequirementH4 = By.XPath("//h4[normalize-space()='Create Requirement']"); public By CreateRequirementH4Id() { return CreateRequirementH4; }
        private By InputCity = By.XPath("//label[contains(text(),'City')]/following::span[contains(@class,'dropdown')]/input[@type='search'][@role='textbox']"); public By InputCityId() { return InputCity; }
        private By KeySkillsSelectionChoice = By.XPath("//label[normalize-space()='Key Skills*']/following::li[@class='select2-selection__choice']"); public By KeySkillsSelectionChoiceId() { return KeySkillsSelectionChoice; }
        private By PostRequirementHeader = By.XPath("//h4[normalize-space()='Post Requirement']"); public By PostRequirementHeaderId() { return PostRequirementHeader; }
        private By PostRequirementPostBtn = By.Id("btnCreatePublishJobPopup"); public By PostRequirementPostBtnId() { return PostRequirementPostBtn; }
        private By SearchResult = By.XPath("//li[@role='treeitem'][1]"); public By SearchResultId() { return SearchResult; }
        private By SuccessfullyPostedOkBtn = By.XPath("//div[contains(text(),'Requirement successfully posted!')]/following::button[normalize-space()='Ok'][1]"); public By SuccesfullyPostedOkBtnId() { return SuccessfullyPostedOkBtn; }
        private By SuccessfullyPostedPopup = By.XPath("//div[contains(text(),'Requirement successfully posted!')]"); public By SuccesfullyPostedPopupId() { return SuccessfullyPostedPopup; }
        public NewRequirementPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        //PageFactory

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Create New Requirement']")]
        private IWebElement createNewRequirementBtn;

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Manually Create the requirement']")]
        private IWebElement manuallyCreateRequirement;

        [FindsBy(How = How.Id, Using = "JobTitle")]
        private IWebElement inputJobTitle;

        [FindsBy(How = How.XPath, Using = "//label[contains(text(),'City')]/following::span[contains(@class,'select2-container')][1]")]
        private IWebElement searchCity;

        [FindsBy(How = How.XPath, Using = "//label[contains(text(),'City')]/following::span[contains(@class,'dropdown')]/input[@type='search'][@role='textbox']")]
        private IWebElement inputCity;

        [FindsBy(How = How.Id, Using = "MinExperience")]
        private IWebElement minExperience;

        [FindsBy(How = How.Id, Using = "MaxExperience")]
        private IWebElement maxExperience;

        [FindsBy(How = How.XPath, Using = "//iframe[@id='divDescription_ifr']")]
        private IWebElement descriptionIframe;

        [FindsBy(How = How.XPath, Using = "//body[@class='mce-content-body ']")]
        private IWebElement descriptionBox;

        [FindsBy(How = How.XPath, Using = "//label[normalize-space()='Key Skills*']/following::input[@type='search']")]
        private IWebElement inputKeySkills;

        [FindsBy(How = How.XPath, Using = "//li[@role='treeitem'][1]")]
        private IWebElement searchResult;

        [FindsBy(How = How.Id, Using = "btnContinueJob")]
        private IWebElement createRequirementContinueBtn;

        [FindsBy(How = How.XPath, Using = "//button[normalize-space()='Create Requirement']")]
        private IWebElement createRequirementBtn;

        [FindsBy(How = How.XPath, Using = "//label[@for='onBlick']")]
        private IWebElement checkboxOnblick;

        [FindsBy(How = How.Id, Using = "btnCreatePublishJobPopup")]
        private IWebElement postRequirementPostBtn;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Requirement successfully posted!')]/following::button[normalize-space()='Ok'][1]")]
        private IWebElement successfullyPostedOkBtn;

        public void enterCity(string city)
        {
        enterCity:
            searchCity.Click();
            try { wait.waitForElementToBeVisible(driver, InputCity, 5); } catch (Exception) { goto enterCity; }
            inputCity.SendKeys(city);
            wait.waitForElementToBeClickable(driver, SearchResult);
            Thread.Sleep(600);
            try
            {
                searchResult.Click();
                var _city = searchCity.Text;
                bool containsCity = _city.Contains(city);
                Assert.True(containsCity);
            }
            catch (Exception)
            {
                goto enterCity;
            }
        }


        public void enterKeySkills(string keySkills)
        {
        enterKeySkills:
            inputKeySkills.Click();
            string[] skills = keySkills.Split(',');
            foreach (string skill in skills)
            {
                inputKeySkills.SendKeys(skill.Trim());
                wait.waitForElementToBeClickable(driver, SearchResult);
                Thread.Sleep(1000);
                try
                {
                    searchResult.Click();
                    IList<IWebElement> keySkillChoices = driver.FindElements(KeySkillsSelectionChoice);
                    int count = keySkillChoices.Count;
                    int index = count - 1;
                    var _keySkills = keySkillChoices[index].Text;
                    bool containsSkill = _keySkills.Contains(skill.Trim());
                    Assert.True(containsSkill);
                }
                catch (Exception)
                {
                    inputKeySkills.SendKeys(Keys.Control + "A" + Keys.Backspace);
                    goto enterKeySkills;
                }
            }
        }

        public void createMultipleRequirements(string jobTitle, string city, string minExp, string maxExp, string description, string keySkills)
        {
            wait.waitForElementToBeClickable(driver, CreateNewRequirement);
            createNewRequirementBtn.Click();
            wait.waitForElementToBeVisible(driver, CreateRequirementH4);
            manuallyCreateRequirement.Click();
            wait.waitForElementToBeVisible(driver, CreateRequirementH2);
            inputJobTitle.SendKeys(jobTitle);
            enterCity(city);
            minExperience.SendKeys(minExp);
            maxExperience.SendKeys(maxExp);
            driver.SwitchTo().Frame(descriptionIframe);
            descriptionBox.SendKeys(description);
            driver.SwitchTo().ParentFrame();
            enterKeySkills(keySkills);
            wait.waitForElementToBeClickable(driver, CreateRequirementContinueBtn);
            createRequirementContinueBtn.Click();
            wait.waitForElementToBeClickable(driver, CreateRequirementBtn);
            createRequirementBtn.Click();
            wait.waitForElementToBeVisible(driver, PostRequirementHeader);
            wait.waitForElementToBeClickable(driver, CheckboxOnblick);
            checkboxOnblick.Click();
            wait.waitForElementToBeClickable(driver, PostRequirementPostBtn);
            postRequirementPostBtn.Click();
            wait.waitForElementToBeVisible(driver, SuccessfullyPostedPopup);
            wait.waitForElementToBeClickable(driver, SuccessfullyPostedOkBtn);
            successfullyPostedOkBtn.Click();
        }

        public void processJobs()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(currentDirectory, "..", "..", "..", "Test Data", "Sample Jobs", "sample_jobs.json");
            string json = File.ReadAllText(filePath);
            dynamic data = JsonConvert.DeserializeObject(json);

            for (int i = 0; i < data.JOBS.Count; i++)
            {
                var job = data.JOBS[i];
                if (i != 0)
                {
                    driver.Navigate().GoToUrl(URLs.Instance.Dashboard_URL);
                }
                createMultipleRequirements((string)job.jobTitle, (string)job.city, (string)job.minExp, (string)job.maxExp, (string)job.description, (string)job.keySkills);
            }
        }
    }
}
