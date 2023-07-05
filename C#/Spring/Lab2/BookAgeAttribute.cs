using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BookAgeAttribute : ValidationAttribute
    {
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public BookAgeAttribute(int minYear, int maxYear)
        {
            MinYear = minYear;
            MaxYear = maxYear;
        }
        public override bool IsValid(object? value)
        {

            if (value is DateTime date)
            {
                if (date.Year >= MinYear && date.Year <= MaxYear)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
