using System;

namespace FlexibleInventorySystem.Exceptions
{
    /// <summary>
    /// Custom exception for inventory-specific errors
    /// </summary>
    public class InventoryException : Exception
    {
        /// <summary>
        /// Error code for categorizing exceptions
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public InventoryException() : base()
        {
        }

        /// <summary>
        /// Constructor with message
        /// </summary>
        /// <param name="message">Exception message</param>
        public InventoryException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor with message and inner exception
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public InventoryException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor with message and error code
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="errorCode">Error code</param>
        public InventoryException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Override Message property to include error code
        /// </summary>
        public override string Message
        {
            get
            {
                if (!string.IsNullOrEmpty(ErrorCode))
                {
                    return $"[{ErrorCode}] {base.Message}";
                }
                return base.Message;
            }
        }
    }
}
