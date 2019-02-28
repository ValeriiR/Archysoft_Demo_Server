

using System;

namespace D1.Model.Exceptions
{
    public class BusinessException : Exception
    {

        public int Status { get; set; }
        public string Description { get; set; }
        public BusinessException(string message, int status) : base(message)
        {
            this.Description = message;
            this.Status = status;
        }
    }
}
