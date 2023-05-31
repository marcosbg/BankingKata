using System;
using System.Text;

namespace BankingKata.Models
{
	public class Account
	{
        private readonly ICalendar _calendar;

        public List<Transaction> Transactions { get; private set; }
		public int Balance { get; private set; }

		public Account(ICalendar calendar)
		{
            _calendar = calendar;
            Transactions = new List<Transaction>();
		}

        public void Deposit(int amount)
		{
			Balance += amount;
			Transactions.Add(Transaction.DepositOf(amount, Balance, _calendar.GetCurrentDate()));
		}

        public void Withdraw(int amount)
        {
			if (Balance == 0)
				throw new InvalidOperationException("Cannot withdraw zero balance.");

			if (amount > Balance)
				Balance = 0;
			else
				Balance -= amount;

            Transactions.Add(Transaction.WithdrawOf(amount, Balance, _calendar.GetCurrentDate()));
        }

        public string PrintStatement()
        {
			var statement = new StringBuilder();
            statement.AppendLine("Date       Amount Balance");

            foreach (var transaction in Transactions)
			{
                statement.Append( transaction.Date.ToString("d"));
                statement.Append(' ');
                statement.Append(transaction.Type == TransactionType.DEPOSIT ? "+" : "-");
                statement.Append(transaction.Amount);
				statement.Append("   ");
                statement.AppendLine(transaction.Balance.ToString());
			}

			return statement.ToString();
        }
    }
}

