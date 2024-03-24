using ITS_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS_Library.Models
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
            {

                return new ValidationResult("Email is required!");
            }


            MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));

            if (_context.Users.Any(e => e.Email == value.ToString()))
            {

                return new ValidationResult("Email must be unique!");
            }
            else
            {

                return ValidationResult.Success;
            }
        }
    }
}

public class ExistingEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        if (value == null)
        {

            return new ValidationResult("Email is required!");
        }


        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));

        if (!_context.Users.Any(e => e.Email == value.ToString()))
        {

            return new ValidationResult("User not registered");
        }
        else
        {

            return ValidationResult.Success;
        }
    }
}
