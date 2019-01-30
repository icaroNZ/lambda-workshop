using System;
using System.Collections.Generic;
using System.Text;

namespace HelloLambda
{
    public static class MyUseCaseCommon
    {
        public static void ValidateMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new Exception("Message cannot be empty.");
            }

            if (message.Length > 200)
            {
                throw new Exception("Message cannot contain more than 200 characters.");
            }
        }

        public static void ValidateId(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Id must be greater than zero");
            }            
        }
    }
}
