using System;

namespace BIKChecker
{
    /// <summary>
    /// DO NOT CHANGE !!!
    /// It is of some external library :)
    /// </summary>
    public class BIKChecker
    {
        public bool Verify(string pesel)
        {
            return new Random().Next(2) == 0;
        }
    }
}
