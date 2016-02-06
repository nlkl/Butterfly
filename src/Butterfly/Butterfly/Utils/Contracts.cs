using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.Utils
{
    internal static class Contracts
    {
        public static void ArgNotNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static T EnsureNotNull<T>(T value, string errorMessage)
        {
            if (value == null)
            {
                throw new ButterflyContractException(errorMessage);
            }

            return value;
        }
    }

    public class ButterflyContractException : Exception
    {
        public ButterflyContractException(string message) : base(message)
        {
        }
    }
}
