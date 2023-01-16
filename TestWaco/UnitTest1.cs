using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Collections.Generic;
using System.Xml.Linq;
using WACO;


namespace TestWaco
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void TestValidateUserCreationWithDuplicateValues()
        {
            List<User> users = new List<User>();
            users.Add(new User (345431,"Pedro","Vargas" ));
            users.Add(new User (123412,"Pablo","Camacho"));
            users.Add(new User (544323,"Jorge","Venegas"));

            WacoController myList = new WacoController(users);

            Assert.IsTrue(myList.ExistsUserWithCI(544323));
        }

        [TestMethod]
        public void TestValidateMoreThanOneLectureByPeriod()
        {
         
            List<Consumption> consumptions = new List<Consumption>();
            consumptions.Add(new Consumption("01/2023", 200));
            consumptions.Add(new Consumption("03/2023", 400));
            consumptions.Add(new Consumption("04/2023", 600));
            consumptions.Add(new Consumption("01/2023", 800));

            User myUser1 = new User(345431, "Pedro", "Vargas", consumptions);

            Assert.IsTrue(myUser1.VerifyLecture("01/2023"));
        }

        [TestMethod]
        public void TestValidateUserExistenceInReadingConsumptionReturnErrorMessage()
        {
            string message = string.Empty;

            List<User> users = new List<User>();
            users.Add(new User(345431, "Pedro", "Vargas"));
            users.Add(new User(123412, "Pablo", "Camacho"));
            users.Add(new User(544323, "Jorge", "Venegas"));

            WacoController myList = new WacoController(users);

            try
            {
                myList.FindUser(898746);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            Assert.AreEqual(message, "The CI doesn't Exist");
        }
        [TestMethod]
        public void TestValidateGetTotalDebtReturnCorrectSum()//correct debt
        {
            List<Consumption> consumptions = new List<Consumption>();
            consumptions.Add(new Consumption("01/2023", 20));//20
            consumptions.Add(new Consumption("02/2023", 30));//20
            consumptions.Add(new Consumption("03/2023", 40));//20
            consumptions.Add(new Consumption("04/2023", 70));//40 Total 100

            User myUser1 = new User(345431, "Pedro", "Vargas", consumptions);

            Assert.AreEqual(myUser1.TotalDebt(consumptions), 120);
        }
        [TestMethod]
        public void TestVerifyIfUserPaidIsUpdatedTo0InTotalPaid()
        {
            List<Consumption> consumptions = new List<Consumption>();
            consumptions.Add(new Consumption("01/2023", 20));//20
            consumptions.Add(new Consumption("02/2023", 30));//20
            consumptions.Add(new Consumption("03/2023", 40));//20
            consumptions.Add(new Consumption("04/2023", 70));//40 Total 100

            User myUser1 = new User(345431, "Pedro", "Vargas", consumptions);

            Assert.AreEqual(myUser1.PaidTotalDebt(consumptions), 0);
        }

        [TestMethod]
        public void TestPaidPartialDebtEnteringNumberofPeriods()
        {
            string message = string.Empty;

            List<Consumption> consumptions = new List<Consumption>();
            consumptions.Add(new Consumption("01/2023", 20));
            consumptions.Add(new Consumption("02/2023", 30));
            consumptions.Add(new Consumption("03/2023", 40));
            consumptions.Add(new Consumption("04/2023", 70));

            User myUser1 = new User(345431, "Pedro", "Vargas", consumptions);

            try
            {
                myUser1.PaidPartialDebt(5, consumptions);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            Assert.AreEqual(message, "Wrong number of periods");
        }


    }
}