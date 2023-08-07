 public static class DateOnlyExtensions
{
    public static int CalculateAge(this DateTime dob)
    {
          DateTime today = DateTime.UtcNow; //  UtcNow to ensure consistent calculations
            int age = today.Year - dob.Year;

            if (dob > today.AddYears(-age))  
            {
                age--;
            }

            return age;
    }
}
   