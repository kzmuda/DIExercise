using System;
using System.Collections.Generic;
using System.Text;

namespace DIExercise
{
    public class Loan
    {
        public bool Get()
        {
            bool isLoanGranted;
            Console.Out.WriteLine("Podaj imię i nazwisko:");
            string name = Console.ReadLine();

            Console.Out.WriteLine("Podaj pesel:");
            string pesel = Console.ReadLine();

            Console.Out.WriteLine("Podaj rok urodzenia:");
            string birthYearStr = Console.ReadLine();

            Console.Out.WriteLine("Podaj zarobki:");
            string salaryStr = Console.ReadLine();

            Console.Out.WriteLine("Podaj kwotę pożyczki:");
            string requestedLoanAmountStr = Console.ReadLine();

            int birthYear;
            if (int.TryParse(birthYearStr, out birthYear))
            {
                var age = DateTime.Now.Year - birthYear;
                if (age >= 18)
                {
                    int requestedLoanAmount, salary = 0;
                    if (!int.TryParse(requestedLoanAmountStr, out requestedLoanAmount)
                        || !int.TryParse(salaryStr, out salary))
                    {
                        var bikChecker = new BIKChecker.BIKChecker();
                        if (bikChecker.Verify(pesel))
                        {
                            var service = new LoanService();
                            var loanAmount = service.CalculateAmount(salary, requestedLoanAmount);
                            if (loanAmount == 0)
                            {
                                Console.Out.WriteLine("Nie przyznano pożyczki");
                                isLoanGranted = false;
                            }
                            else
                            {
                                Console.Out.WriteLine($"Przyznano pożyczkę w wysokości {loanAmount} zł");
                                var client = new Client
                                {
                                    Name = name, 
                                    Pesel = pesel, 
                                    LoanAmount = loanAmount
                                };

                                try
                                {
                                    var repo = new ClientRepository();
                                    repo.AddClient(client);
                                    isLoanGranted = true;
                                }
                                catch (Exception e)
                                {
                                    isLoanGranted = false;
                                    Console.WriteLine("Błąd podczas zapisu do bazy");
                                    throw;
                                }
                            }
                        }
                        else
                        {
                            isLoanGranted = false;
                            Console.Out.WriteLine("Negatywna weryfikacja w BIK");
                        }

                    }
                    else
                    {
                        isLoanGranted = false;
                        Console.Out.WriteLine("Niepoprawna kwota pożyczki lub zarobków");
                    }

                }
                else
                {
                    isLoanGranted = false;
                    Console.Out.WriteLine("Jesteś za młody");
                }
            }
            else
            {
                isLoanGranted = false;
                Console.Out.WriteLine("Niepoprawny rok urodzenia");
            }

            return isLoanGranted;
        }
    }
}
