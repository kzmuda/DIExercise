using System;
using System.Collections.Generic;
using System.Text;

namespace DIExercise
{
    public interface ILoanService
    {
        int CalculateAmount(int salary, int requestedLoanAmount);
    }
}
