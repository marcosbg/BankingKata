using System.Text;
using System.Transactions;
using BankingKata.Models;
using Moq;

namespace BankingKata.Tests;

public class AccountTests
{
    Mock<ICalendar> mockCalendar;
    Account account;
    DateTime transactionDate;

    [SetUp]
    public void Setup()
    {
        mockCalendar = new Mock<ICalendar>();
        transactionDate = new DateTime(2023, 5, 9);
        mockCalendar.Setup(m => m.GetCurrentDate()).Returns(transactionDate);
        account = new Account(mockCalendar.Object);
    }

    [Test]
    public void Should_Increase_Balance_After_Deposit()
    {
        account.Deposit(100);
        Assert.That(account.Balance, Is.EqualTo(100));
    }

    [Test]
    public void Should_Increase_Balance_After_Two_Deposits()
    {
        account.Deposit(100);
        account.Deposit(300);
        Assert.That(account.Balance, Is.EqualTo(400));
    }

    [Test]
    public void Should_Decrease_Balance_After_Withdraw()
    {
        account.Deposit(300);
        account.Withdraw(100);
        Assert.That(account.Balance, Is.EqualTo(200));
    }

    [Test]
    public void Should_Not_Allow_Withdraw_When_Balance_Is_Zero()
    {
        Assert.Throws(Is.TypeOf<InvalidOperationException>().
            And.Message.EqualTo("Cannot withdraw zero balance."),
            () => account.Withdraw(100));
    }

    [Test]
    public void Should_Not_Negativate_Balance_After_Withdraw()
    {
        account.Deposit(50);
        account.Withdraw(100);
        Assert.That(account.Balance, Is.EqualTo(0));
    }

    [Test]
    public void Should_Print_Transactions_History_With_Balance()
    {
        account.Deposit(500);
        account.Withdraw(100);
        account.Withdraw(200);

        var statement = new StringBuilder();
        statement.AppendLine("Date       Amount Balance");
        statement.AppendLine("09/05/2023 +500   500");
        statement.AppendLine("09/05/2023 -100   400");
        statement.AppendLine("09/05/2023 -200   200");

        Assert.That(account.PrintStatement(), Is.EqualTo(statement.ToString()));
    }

    [Test]
    public void Should_Print_Transactions_History_With_Date()
    {
        account.Deposit(500);

        var statement = new StringBuilder();
        statement.AppendLine("Date       Amount Balance");
        statement.AppendLine("09/05/2023 +500   500");

        Assert.That(account.PrintStatement(), Is.EqualTo(statement.ToString()));
    }

    [Test]
    public void Should_Print_Transactions_History_With_Date_In_Different_Days()
    {
        
        account.Deposit(500);

        mockCalendar.Setup(m => m.GetCurrentDate()).Returns(new DateTime(2023, 5, 12));

        account.Deposit(300);

        var statement = new StringBuilder();
        statement.AppendLine("Date       Amount Balance");
        statement.AppendLine("09/05/2023 +500   500");
        statement.AppendLine("12/05/2023 +300   800");

        Assert.That(account.PrintStatement(), Is.EqualTo(statement.ToString()));
    }

    [Test]
    public void Should_Print_Statement_With_Headers()
    {

        account.Deposit(500);

        mockCalendar.Setup(m => m.GetCurrentDate()).Returns(new DateTime(2023, 5, 12));

        account.Deposit(300);

        var statement = new StringBuilder();
        statement.AppendLine("Date       Amount Balance");
        statement.AppendLine("09/05/2023 +500   500");
        statement.AppendLine("12/05/2023 +300   800");

        Assert.That(account.PrintStatement(), Is.EqualTo(statement.ToString()));
    }
}