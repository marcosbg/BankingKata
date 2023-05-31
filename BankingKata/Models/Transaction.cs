using System;
namespace BankingKata.Models
{
	public class Transaction
	{
		public TransactionType Type { get; }
		public DateTime Date { get; }
		public int Amount { get; }
		public int Balance { get; }

		public Transaction(int amount, TransactionType type, int balance, DateTime date)
		{
			Type = type;
			Amount = amount;
			Balance = balance;
			Date = date;
		}

		public static Transaction DepositOf(int amount, int balance, DateTime date)
		{
			return new Transaction(amount, TransactionType.DEPOSIT, balance, date);
		}

        public static Transaction WithdrawOf(int amount, int balance, DateTime date)
        {
            return new Transaction(amount, TransactionType.WITHDRAW, balance, date);
        }
    }
}

