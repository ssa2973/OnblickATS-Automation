using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace OnblickATS_Automation
{
    public class WaitHelpers
    {
        private IWebDriver driver;
        public void waitForElementToBeVisible(IWebDriver driver, By element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.ElementIsVisible(element));
        }
        public void waitForElementToBeVisible(IWebDriver driver, By element, int Seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Seconds));
            wait.Until(ExpectedConditions.ElementIsVisible(element));
        }

        public void waitForInvisibilityOfElement(IWebDriver driver, By element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(element));
        }

        public void waitForElementToBeClickable(IWebDriver driver, By element)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

    }
}