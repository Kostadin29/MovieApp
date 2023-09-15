using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Shared
{
    public class MovieException : Exception
    {
        public MovieException(string message) : base(message)
        {
            
        }
    }
}
