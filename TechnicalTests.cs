using NUnit.Framework;
using PlanitTestSolution.Pages;
using System;

namespace PlanitTestSolution
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class TechnicalTests : BaseTest
    {
        [TestCase(TestName = "Verify that error messages are" +
            " displayed for mandatory fields on the feedback page")]
        public void ValidateErrorMessagesOnFeedbackPage()
        {
            homePage.IsPageLoaded();
            var feedbackPage = homePage.ClickContactButton();
            feedbackPage = feedbackPage.ClickSubmitButton().Current;

            Assert.Multiple(() =>
            {
                Assert.IsTrue(feedbackPage.ValidateForenameErrorMessage());
                Assert.IsTrue(feedbackPage.ValidateEmailErrorMessage());
                Assert.IsTrue(feedbackPage.ValidateMessageError());
                Assert.IsTrue(feedbackPage.ValidateMainErrorMessage());
            });
        }

        [TestCase("John", "john@email.com", "Test has passed", 
            TestName = "Verify that if no errors are present on" +
            " the Feedback page then the user is able to successfully" +
            " submit the feedback form")]
        [Repeat(5)]
        public void ValidateSubmissionMessage(string Forename, string Email, String Message)
        {
            homePage.IsPageLoaded();
            var feedBackPage = topMenuRibbon.ClickContactButton();
            var submissionPage = feedBackPage.EnterForename(Forename)
                .EnterEmail(Email)
                .EnterMessage(Message)
                .ClickSubmitButton().Next;
            var SubmissionStatus = submissionPage.VerifySubmissionMessage(Forename);
            Assert.IsTrue(SubmissionStatus);
        }

        [TestCase("Funny Cow", 2,"Fluffy Bunny", 1,
            TestName = "Verify that shopping items can be added to the cart")]
        public void VerifyToyItemsCanBeAddedToCart(string item1, int quantity1, string item2, int quantity2)
        {
            homePage.IsPageLoaded();
            var CartPage = homePage.ClickShopButton()
                .AddItemsToTheCart(item1, quantity1)
                .AddItemsToTheCart(item2, quantity2)
                .ClickCartMenu();
            Assert.AreEqual(CartPage.IsItemPresentInCart(item1, quantity1), true, "2 Funny Cow are not present in the shopping cart");
            Assert.AreEqual(CartPage.IsItemPresentInCart(item2, quantity2), true, "1 Fluffy Bunny is not present in the shopping cart");
        }

        [TestCase("Stuffed Frog", 10.99, 2, "Fluffy Bunny", 9.99, 5, "Valentine Bear", 14.99, 3)]
        public void VerifyThePriceOfItemsInCart(
            string item1, double item1Price, int item1Quanity,
            string item2, double item2Price, int item2Quantity, 
            string item3, double item3Price, int item3Quantity)
        {
            homePage.IsPageLoaded();
            var shoppingPage = homePage.ClickShopButton();
            var cartPage = shoppingPage.AddItemsToTheCart(item1, item1Quanity)
                .AddItemsToTheCart(item2, item2Quantity)
                .AddItemsToTheCart(item3, item3Quantity)
                .ClickCartMenu();
            double oneStuffedFrogPrice = shoppingPage.GetSingleItemPrice(item1);
            double oneFluffyBearPrice = shoppingPage.GetSingleItemPrice(item2);
            double oneValentineBearPrice = shoppingPage.GetSingleItemPrice(item3);
            double subTotalStuffedFrogPrice = cartPage.GetItemSubTotalPrice(item1);
            double subTotalFluffyBunnyPrice = cartPage.GetItemSubTotalPrice(item2);
            double subTotalValentineBearPrice = cartPage.GetItemSubTotalPrice(item3);
            double totalPrice = subTotalStuffedFrogPrice + subTotalFluffyBunnyPrice + subTotalValentineBearPrice;
            Assert.Multiple(() =>
            {
                Assert.IsTrue(oneStuffedFrogPrice == item1Price);
                Assert.IsTrue(oneFluffyBearPrice == item2Price);
                Assert.IsTrue(oneValentineBearPrice == item3Price);
                Assert.IsTrue(subTotalStuffedFrogPrice == item1Price*item1Quanity);
                Assert.IsTrue(subTotalFluffyBunnyPrice == item2Price*item2Quantity);
                Assert.IsTrue(subTotalValentineBearPrice == item3Price*item3Quantity);
                Assert.IsTrue(totalPrice == cartPage.GetTotalPrice());
            });
        }

    }
}
