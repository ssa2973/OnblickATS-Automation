using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace OnblickATS_Automation
{
    public class LoginPage
    {
        private IWebDriver driver;

        WaitHelpers wait = new WaitHelpers();
        private By Answer1Id = By.Id("answer_to_questions-1");
        private By Answer2Id = By.Id("answer_to_questions-2");
        private By ClosePopup = By.Id("closeButtonForFrame"); public By ClosePopupId() { return ClosePopup; }
        private By ContinueId = By.XPath("//input[@value='Continue']");
        private By CurrentRole = By.XPath("//div[@class='loggeninUserRoles']");
        private By HrImmigrationButton = By.XPath("//a[contains(text(),'HR Immigration')]"); public By HrImmigrationButtonId() { return HrImmigrationButton; }
        private By RoleChangeComplete = By.XPath("//h2[normalize-space()='Role change complete']");
        private By RoleChangeOkBtn = By.XPath("//h2[normalize-space()='Role change complete']/following::button[normalize-space()='Ok']");
        private By SignOut = By.XPath("//label[normalize-space()='Sign Out']");
        private By Submit = By.Id("submitButton");
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }


        //PageObjectFactory
        [FindsBy(How = How.XPath, Using = "//h2[normalize-space()='Role change complete']/following::button[normalize-space()='Ok']")]
        private IWebElement roleChangeOkBtn;

        [FindsBy(How = How.XPath, Using = "//div[@class='loggeninUserRoles']")]
        private IWebElement currentRole;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Sign In')]")]
        private IWebElement signInButtonDemo;

        [FindsBy(How = How.XPath, Using = "//a[@href='/account/login']")]
        private IWebElement hrImmigrationButton;
        public void getSignInButtonDemo()
        {
            Actions actions = new Actions(driver);
            signInButtonDemo.Click();
            //try
            //{
            //    actions.MoveToElement(signInButtonDemo).Perform();
            //    hrImmigrationButton.Click();
            //}
            //catch (NoSuchElementException)
            //{

            //} //for dev env
        }

        [FindsBy(How = How.XPath, Using = "(//a[normalize-space()='Sign In'])[2]")]
        private IWebElement signInButtonProd;

        public void getSignInButtonProd()
        {
            signInButtonProd.Click();
        }

        [FindsBy(How = How.XPath, Using = "//input[contains(@placeholder,'Email')]")]
        private IWebElement username;

        public IWebElement getUsername()
        {
            return username;
        }

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement password;

        public IWebElement getPassword()
        {
            return password;
        }

        [FindsBy(How = How.Id, Using = "submitButton")]
        private IWebElement submitButton;

        [FindsBy(How = How.Id, Using = "next")]
        private IWebElement submitButton2;

        public void getSubmitButton()
        {
            try
            {
                submitButton.Click();
            }
            catch (NoSuchElementException)
            {
                submitButton2.Click();
            }
        }

        [FindsBy(How = How.XPath, Using = "//label[normalize-space()='Sign Out']")]
        private IWebElement signOutButton;
        public IWebElement getSignOutButton()
        {
            return signOutButton;
        }

        [FindsBy(How = How.Id, Using = "answer_to_questions-1")]
        private IWebElement answer1;
        public IWebElement getAnswer1()
        {
            return answer1;
        }

        [FindsBy(How = How.Id, Using = "answer_to_questions-2")]
        private IWebElement answer2;
        public IWebElement getAnswer2()
        {
            return answer2;
        }

        [FindsBy(How = How.XPath, Using = "//input[@value='Continue']")]
        private IWebElement continueButton;
        public void getContinueButton()
        {
            continueButton.Click();
        }
        public By getSignOut()
        {
            return SignOut;
        }

        public By getSubmit()
        {
            return Submit;
        }

        [FindsBy(How = How.XPath, Using = "//span[contains(@class,'user-name')]")]
        private static IWebElement readUsername;
        public static string getReadUsername()
        {
            string name = readUsername.Text;
            return name;
        }

        [FindsBy(How = How.Id, Using = "closeButtonForFrame")]
        private IWebElement closeButtonForFrame;
        public void getCloseButton()
        {
            closeButtonForFrame.Click();
        }



        public void validLogin(string user, string pass)
        {
            WebDriverWait wt = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
            driver.Navigate().GoToUrl(URLs.Instance.Login_URL);
            switch (Environment.Name)
            {
                default:
                {
                    getSignInButtonDemo();
                }
                break;

                case "prod":
                {
                    getSignInButtonProd();
                }
                break;
            }
            username.SendKeys(user);
            password.SendKeys(pass);
            getSubmitButton();
            wait.waitForElementToBeVisible(driver, SignOut);
        }

        public void validLoginFirstTime(string user, string pass)
        {
            driver.Navigate().GoToUrl(URLs.Instance.Login_URL);
            switch (Environment.Name)
            {
                default:
                {
                    getSignInButtonDemo();
                }
                break;

                case "prod":
                {
                    getSignInButtonProd();
                }
                break;
            }
            username.SendKeys(user);
            password.SendKeys(pass);
            getSubmitButton();
            wait.waitForElementToBeVisible(driver, Answer1Id);
            getAnswer1().SendKeys("Answer 1");
            getAnswer2().SendKeys("Answer 2");
            getContinueButton();
            wait.waitForElementToBeVisible(driver, SignOut);
        }

        public void logOut()
        {
            wait.waitForElementToBeClickable(driver, getSignOut());
            getSignOutButton().Click();
            wait.waitForElementToBeVisible(driver, getSubmit());
        }

        public void changeRole(string role)
        {
            By newRole = By.XPath("//p[normalize-space()='" + role + "']");
            wait.waitForElementToBeVisible(driver, CurrentRole);
            bool roleDisplayed = CheckRole(role);
            while (!roleDisplayed)
            {
                currentRole.Click();
                wait.waitForElementToBeVisible(driver, newRole);
                driver.FindElement(newRole).Click();
                wait.waitForElementToBeVisible(driver, RoleChangeComplete);
                wait.waitForElementToBeClickable(driver, RoleChangeOkBtn);
                roleChangeOkBtn.Click();
                roleDisplayed = CheckRole(role);
            }

            bool CheckRole(string role)
            {
                var _role = currentRole.Text;
                _role = _role.Trim();
                bool roleDisplayed = _role.Contains(role);
                return roleDisplayed;
            }
        }
    }
}
