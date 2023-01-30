using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;

namespace ApotekaTests.PageObjects
{
    public class HomePagePO
    {
        private IWebDriver Driver;
        private const string PAGE_URL = "https://apopro.dk/";
        private const string PARTIAL_PAGE_TITLE = "Apopro online apotek | Receptpligtig medicin leveret til døren!";
        
        internal HomePagePO(IWebDriver driver)
        {
            Driver = driver;
        }

        internal void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PAGE_URL);
            Driver.Manage().Window.Maximize();
			List<IWebElement> cookiePrompt = new List<IWebElement>();
			cookiePrompt.AddRange(Driver.FindElements(By.XPath("//*[text()='Accepter alle']")) );
			//checking element count in list
			if (cookiePrompt.Count > 0)
			{
                cookiePrompt[0].Click();    
			}
          //  EnsurePageLoaded();
        }

        internal bool EnsurePageLoaded()
        {
            bool pageHasLoaded = (Driver.Url == PAGE_URL)&& (Driver.Title.Contains (PARTIAL_PAGE_TITLE));
            if (!pageHasLoaded)
            {
                return false;
                throw new Exception($"Failed to load page. Page URL = '{Driver.Url}', Page Title = {Driver.Title}");
            }
            else
            {
                return true;
            }
        }

        internal void Login(string eMail, string password)
        {
            
            Driver.FindElement(By.LinkText("Log ind")).Click();

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
           // IWebElement emailInput = wait.Until((d) => d.FindElement(By.CssSelector("input[id=’Email’]")));
			 Driver.FindElement(By.XPath("//*[@id=\"Email\"]")).SendKeys(eMail);

			 Driver.FindElement(By.XPath("//*[@id=\"password\"]")).SendKeys(password);

			Driver.FindElement(By.XPath("//*[@id=\"login-submit\"]")).Click();


		}

        internal bool EnsureLoggedIn(string eMail)
        {
            IWebElement h1Greeting = Driver.FindElement(By.TagName("h1"));
            // Have checked only 1 h1 !!!

            if (h1Greeting.Text.Contains(eMail))
			{return true;}
			
			else { return false; }
			
        }


       internal bool AddToBasketAndCheck()
        {
            ReadOnlyCollection<IWebElement> products = Driver.FindElements(By.LinkText("Læg i kurv"));
            string productName = products[0].Text;
            products[0].Click();
			
            try
			{
				IWebElement textDemo = Driver.FindElement(By.XPath("//*[text()='" + productName + "']"));

			}
			catch (NoSuchElementException)
			{

                return false;
			}

            return true;
		}

		internal bool RemoveFromBasketAndCheck()
		{
		 WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
		 IWebElement tilKurv = wait.Until((d) => d.FindElement(By.XPath("//*[text()=' Gå til kurv']")));
         tilKurv.Click();
            
		 Driver.FindElement(By.XPath("//*[text()='Fjern']")).Click();

			try
			{
				IWebElement textDemo = Driver.FindElement(By.XPath("//*[text()='Din kurv er tom']"));

			}
			catch (NoSuchElementException)
			{

				return false;
			}

			return true;

		}




	}
}
