using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yom.Web.Services
{
    public class OperationResult
    {
        private OperationResult()
        { }

        public static OperationResult Error(string message)
        {
            return new OperationResult
            {
                Message = message,
                IsSuccessful = false,
            };
        }

        public static OperationResult Success
        {
            get
            {
                return new OperationResult
                {
                    Message = string.Empty,
                    IsSuccessful = true,
                };
            }
        }

        public bool IsSuccessful { get; private set; }

        public string Message { get; private set; }
    }
}