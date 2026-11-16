using System;

namespace Lab15_SoftwareQuality
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
}