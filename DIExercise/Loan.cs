using System;
using System.Collections.Generic;
using System.Text;

namespace DIExercise
{
    public class Loan
    {
        private BIKChecker.BIKChecker bIKChecker;
        private ILoanService loanService;
        private ClientRepository clientRepository;

       
        public Loan(BIKChecker.BIKChecker bIKChecker, ILoanService loanService, ClientRepository clientRepository)
        {
            this.bIKChecker = bIKChecker;
            this.loanService = loanService;
            this.clientRepository = clientRepository;
        }


        public bool Get(string name, string pesel, string birthYearStr, string salaryStr, string requestedLoanAmountStr)
        {
            bool isLoanGranted;
            

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
                        
                        if (bIKChecker.Verify(pesel))
                        {
                            
                            var loanAmount = loanService.CalculateAmount(salary, requestedLoanAmount);
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

                                    clientRepository.AddClient(client);
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
