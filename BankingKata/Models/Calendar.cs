namespace BankingKata.Models
{
    public class Calendar : ICalendar
    {
        public Calendar()
        {
        }

        public DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }
    }
}