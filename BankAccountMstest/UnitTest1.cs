using System.Data;

namespace BankAccountMstest
{
    [TestClass]
    public class UnitTest1
    {  
        //Deposit
        [TestMethod]
        [DataRow(2500, 2500)]
       

        public void ValidAmount_increaseBalance(double amtToTest, double expectedResult)
        {
            //arrange
            BankAccount bankAccount = new BankAccount();

            //action 
            bankAccount.Deposit(amtToTest);
            double actualResult = bankAccount.GetBalance();
            

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [DataRow(0)]   
        [DataRow(-1000)] 
        public void InvalidAmount_ThrowsArgumentException(double amtToTest)
        {
            // Arrange
            BankAccount bankAccount = new BankAccount();

            // Act & Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => bankAccount.Deposit(amtToTest));
            Assert.AreEqual("Deposit amount must be positive.", ex.Message);
        }



        //withdraw
        [TestMethod]
        [DataRow(1000, 500, 500)]
        [DataRow(20000, 10000, 10000)]
        public void ValidAmount_WithDrawBalance(double initialDeposit, double amtToWithDraw, double expectedResult)
        {
            //arrange
            BankAccount bankAccount = new BankAccount();
            bankAccount.Deposit(initialDeposit);

            //action 
            bankAccount.Withdraw(amtToWithDraw);
            double actualResult = bankAccount.GetBalance();

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [DataRow(500, 1000)]  // Deposit 500, try to withdraw 1000 - should throw "Insufficient funds"
        public void InValidAmt_WithDraw_ThrowsInsufficientFundsException(double initialDeposit, double amtToWithDraw)
        {
            // Arrange
            BankAccount bankAccount = new BankAccount();
            bankAccount.Deposit(initialDeposit);

            // Act & Assert
            var ex = Assert.ThrowsException<InvalidOperationException>(() => bankAccount.Withdraw(amtToWithDraw));
            Assert.AreEqual("Insufficient funds.", ex.Message);
        }

        [TestMethod]
        [DataRow(-500)]
        public void WithdrawingNegativeAmountThrowsException(double amtToTest)
        {
            // Arrange
            BankAccount bankAccount = new BankAccount();

            //Act and assert
            var ex = Assert.ThrowsException<ArgumentException>(()=> bankAccount.Withdraw(amtToTest));
            Assert.AreEqual("Withdrawal amount must be positive.", ex.Message);

        }

        //Transfer
        [TestMethod]
        [DataRow(1000, 2000)]
        

        public void TestingTransferToValidAccounts(double amountToTest, double initialDeposit)
        {
            //Arrange
            BankAccount bankAccount1 = new BankAccount();
            bankAccount1.Deposit(initialDeposit);
            BankAccount bankAccount2 = new BankAccount();
            bankAccount2.Deposit(initialDeposit);

            //Action
            bankAccount1.Withdraw(amountToTest); //withdrawn 1000
            bankAccount2.Deposit(amountToTest); //deposited 1000

            //Assert
            Assert.AreEqual(initialDeposit-amountToTest, bankAccount1.GetBalance());
            Assert.AreEqual(initialDeposit+amountToTest, bankAccount2.GetBalance());
            
        }

        [TestMethod]
        [DataRow(1000)]

        public void TestingTransferToAccountDoesNotExist(double amountToTest)
        {
            //Arrange
            BankAccount account = new BankAccount();
            //Action
            account.Deposit(amountToTest);
            //Assert
            var ex = Assert.ThrowsException<ArgumentNullException>(() => account.Transfer(null, amountToTest));
            Assert.AreEqual("Value cannot be null. (Parameter 'toAccount')", ex.Message);
        }

    }
}

