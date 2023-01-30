// global usings in Using.cs

namespace ApotekaTests.Tests
{
    public class BasicTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        [Category("Login")]
        [TestCase("robert.ingleby@mail.dk", "TjcStr83")]

        public void ShouldOpenHomeAndLogin(string eMail, string password)
        {

            //using (IWebDriver driver = new FirefoxDriver())

            using (IWebDriver driver = new ChromeDriver())

            {
                var homePage = new HomePagePO(driver);
                homePage.NavigateTo();
                Assert.That(homePage.EnsurePageLoaded());

                homePage.Login(eMail, password);
                Assert.That(homePage.EnsureLoggedIn(eMail));

            }



        }

        [Test]
        [Category("AddAndRemove")]
        public void ShouldAddAndRemoveFromBasket()
        {


            //using (IWebDriver driver = new FirefoxDriver())

            using (IWebDriver driver = new ChromeDriver())

            {

                var homePage = new HomePagePO(driver);
                homePage.NavigateTo();
                Assert.That(homePage.EnsurePageLoaded());


                Assert.That(homePage.AddToBasketAndCheck());

                Assert.That(homePage.RemoveFromBasketAndCheck());

            }
        }





    }
}
