using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.viewDto
{
    public class OperationResult
    {
        public OperationResult(OperationResultType resultType)
        {
            this.ResultType = resultType;
        }

        public OperationResult(OperationResultType resultType,string message) 
            : this(resultType)
        {
            this.Message = message;
        }

        public OperationResult(OperationResultType resultType, string message, object data)
            :this(resultType,message)
        {
            this.Data = data;
        }

        public OperationResult(OperationResultType resultType, string message,string logmessage)
            :this(resultType,message)
        {
            this.LogMessage = logmessage;
        }

        public OperationResult(OperationResultType resultType,string message,string logmessage,object data)
            :this(resultType,message,logmessage)
        {
            this.Data = data;
        }

        public OperationResultType ResultType { get; set; }
        public string Message { get; set; }
        public string LogMessage { get; set; }
        public object Data { get; set; }
    }
}