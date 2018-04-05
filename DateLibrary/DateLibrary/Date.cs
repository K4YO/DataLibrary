using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateLibrary
{
    public class Date
    {
        int day, month, year;
        int[] Months = new int[13] { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public int Day { get => day; }
        public int Month { get => month; }
        public int Year { get => year; }

        /// <summary>
        /// Initializes a new instance of the System.Date structure to the specified day, month and year.
        /// </summary>
        /// <param name="day">The day (1 through the number of days in month).</param>
        /// <param name="month">The month (1 through 12).</param>
        /// <param name="year">The year (1 through 9999).</param>
        public Date(int day, int month, int year)
        {
            if (ValidDate(day, month, year))
            {
                this.day = day;
                this.month = month;
                this.year = year;
            }

            else // Se a data não for válida, configura o dia = 01, month = 01 e ano = 01
            {
                this.day = 01;
                this.month = 01;
                this.year = 01;
            }
        }

        public Date(int day, int month, int year, int days)
        {
            if (ValidDate(day, month, year))
            {
                for (int i = 0; i < days; i++)
                {
                    if (month == 02 && LeapYear(year))
                    {
                        if (day < 29)
                        {
                            day++;
                        }
                        else
                        {
                            day = 1;
                            month++;
                        }
                    }
                    else if (day < Months[month])
                    {
                        day++;
                    }
                    else
                    {
                        if (month < 12)
                        {
                            month++;
                        }
                        else
                        {
                            day = 01;
                            month = 01;
                            year++;
                        }
                    }
                }

                this.day = day;
                this.month = month;
                this.year = year;
            }

            else // Se a data não for válida, configura o dia = 01, month = 01 e ano = 01
            {
                this.day = 01;
                this.month = 01;
                this.year = 01;
            }
        }

        /// <summary>
        /// Método que retorna true se a data for válida; caso contrário, false
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private bool ValidDate(int day, int month, int year)
        {
            if (year >= 01 && 9999 >= year)
            {
                if (LeapYear(year))
                {
                    if (month == 02)
                    {
                        if (day >= 01 && 29 >= month)
                        {
                            return true;
                        }
                    }

                    else if (month == 01 || month > 2 && 12 >= month)
                    {
                        if (day >= 01 && day <= Months[month])
                        {
                            return true;
                        }
                    }
                }

                else if (month >= 01 && 12 >= month)
                {
                    if (day >= 01 && day <= Months[month])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Método que retorna true se o ano for Bissexto; caso contrário, false
        /// </summary>
        /// <param name="year"></param>
        /// <returns>true if year is a leap year; otherwise, false</returns>
        private bool LeapYear(int year)
        {
            bool leapYear = false;

            if (year % 400 == 0)
            {
                return leapYear = true;
            }
            else if (year % 4 == 0 && year % 100 != 0)
            {
                return leapYear = true;
            }

            return leapYear;
        }

        /// <summary>
        /// Compares this instance with a specified Object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified Object.
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns>A signed number indicating the relative values of this instace and Obj. Value Type Condition that is less than zero, this instace is earlier than Obj.
        ///          Zero, this instace is the same as Obj.Greater than zero, this instace is later. </returns>        
        public int CompareTo(Date Obj)
        {
            if (this.Year < Obj.Year)
            {
                return -1;
            }
            else if (this.Year != Obj.Year)
            {
                return 1;
            }
            else
            {
                if (this.Month < Obj.Month)
                {
                    return -1;
                }

                else if (this.Month != Obj.Month)
                {
                    return 1;
                }

                else
                {
                    if (this.Day < Obj.Day)
                    {
                        return -1;
                    }
                    else if (this.Day != Obj.Day)
                    {
                        return 1;
                    }
                }
            }

            return 0;

        }

        /// <summary>
        /// Get days between dates
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns> if this instance is greater than Obj, returns difference in positive days; otherwise, returns difference in negative days</returns>
        public int GetDays(Date Obj)
        {
            int days;
            int auxDays;
            int auxMonth;
            int auxYear;

            if (this.CompareTo(Obj) < 0) // Esta instância antecede Obj 
            {
                auxDays = (this.Day - Obj.Day);

                days = auxDays * -1; // if  auxDays < 0 (negativo), days > 0 ; if auxDays > 0 (positivo), days < 0; Ou seja, days é o simétrico de auxDays 

                auxMonth = this.Month;
                auxYear = this.Year;
                return CalculateDays(days, this.Month, this.Year, Obj.Month, Obj.Year);

            }

            else if (this.CompareTo(Obj) > 0) // Esta instância sucede Obj
            {

                auxDays = (Obj.Day - this.Day);
                days = auxDays * -1; // if  auxDays < 0 (negativo), days > 0 ; if auxDays > 0 (positivo), days < 0; Ou seja, days é o simétrico de auxDays 

                return CalculateDays(days, Obj.Month, Obj.Year, this.Month, this.Year) * -1;
            }

            return days = 0; // Caso contrário, e logicamente, esta instância tem a mesma posição na ordem de classificação que Obj

        }

        /// <summary>
        /// Calculate difference in positive days between dates
        /// </summary>
        /// <param name="days"> Difference between the days of the dates</param>
        /// <param name="smallestMonth"> The month of smallest Date </param>
        /// <param name="smallestYear"> The year of smallest Date </param>
        /// <param name="BiggestMonth">The month of biggest date </param>
        /// <param name="BiggestYear"> The year of biggest date </param>
        /// <returns>returns difference in positive days</returns>
        private int CalculateDays(int days, int smallestMonth, int smallestYear, int BiggestMonth, int BiggestYear)
        {
            while (smallestYear < BiggestYear || smallestMonth > BiggestMonth || smallestMonth < BiggestMonth)
            {
                if (smallestMonth == 2 && LeapYear(smallestYear))
                {
                    days += 29;
                    smallestMonth++;

                }
                else
                {
                    days += Months[smallestMonth];
                    smallestMonth++;
                }
                if (smallestMonth > 12)
                {
                    smallestMonth = 1;
                    smallestYear++;
                }
            }

            return days;
        }
    }
}
