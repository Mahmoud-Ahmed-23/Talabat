using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exception
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string Message) : base(Message)
        {
            
        }

    }
}
