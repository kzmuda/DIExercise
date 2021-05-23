using System;
using System.Collections.Generic;
using System.Text;

namespace DIExercise
{
    public class LoanService
    {
        public int CalculateAmount(int salary, int requestedLoanAmount)
        {
            if (salary < 100)
            {
                return 0;
            }

            // some very complicated calculations
            if (salary > requestedLoanAmount)
            {
                return requestedLoanAmount;
            }

            return requestedLoanAmount - salary;
        }
    }
}
