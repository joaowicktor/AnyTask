using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnyTask.API.Helpers
{
    public class ErrorMessage
    {
        public bool Error { get => true; }
        public string Message { get; set; }

        public ErrorMessage(string msg)
        {
            Message = msg;
        }
    }
}
