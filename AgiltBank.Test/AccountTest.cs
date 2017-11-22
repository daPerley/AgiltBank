using AgiltBank.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AgiltBank.Test
{
    [TestClass]
    public class AccountTest
    {
        private Account _account;

        [TestInitialize]
        public void Initialize()
        {
            _account = new Account()
            {
                Id = 1,
                CustomerId = 1,
                Balance = 200
            };
        }

        #region Withdrawal
        [TestMethod]
        public void CanWithdrawMoneyFromAccount() => Assert.IsTrue(_account.Withdrawal(100));


        [TestMethod]
        public void WithdrawNegativeMoney_ShouldReturnFalse() => Assert.IsFalse(_account.Withdrawal(-100));

        [TestMethod]
        public void WithdrawMoreMoneyThanAccountBalance_ShouldReturnFalse() => Assert.IsFalse(_account.Withdrawal(10000));
        #endregion
    }
}
