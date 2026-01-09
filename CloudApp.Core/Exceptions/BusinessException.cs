namespace CloudApp.Core.Exceptions
{
    /// <summary>
    /// 业务异常基类
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
