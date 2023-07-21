namespace WebApplication5LVL.AppData.Contexts.ExtensionsMethods
{
    public static class DateTimeExtension
    {
        public static DateTime GetNextBirthdayDate(this DateTime birthDate)
        {
            DateTime now = DateTime.Now;

            int year = now.Month > birthDate.Month || now.Month == birthDate.Month &&
                now.Day > birthDate.Day ?
                now.Year + 1 : now.Year;

            return new DateTime(year, birthDate.Month, birthDate.Day);
        }
    }
}
