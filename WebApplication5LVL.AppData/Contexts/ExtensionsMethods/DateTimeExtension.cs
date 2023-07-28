namespace WebApplication5LVL.AppData.Contexts.ExtensionsMethods
{
    /// <summary>
    /// Extension methods holding model, for <see cref="DateTime"/>  structure
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// Func to get next birthDay date of User
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
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
