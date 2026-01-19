using System;

namespace LPU_EXCEPTION
{
    /// <summary>
    /// Custom exception class created for LPU project
    /// Created by Mahesh Singh on 29 Dec
    /// </summary>
    public class Lpu_Exception : Exception
    {
        public Lpu_Exception() : base()
        {
        }

        public Lpu_Exception(string errMsg) : base(errMsg)
        {
        }

        public Lpu_Exception(string errMsg, Exception innerException)
            : base(errMsg, innerException)
        {
        }
    }
}
