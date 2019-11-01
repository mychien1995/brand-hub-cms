using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Models
{
    public class OperationResult
    {
        public OperationResult()
        {

        }
        public OperationResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public OperationResult(string message)
        {
            IsSuccess = false;
            Message = message;
        }
        public OperationResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

    }

    public class OperationResult<T> : OperationResult
    {
        public OperationResult() : base()
        {

        }

        public OperationResult(T data) : base()
        {
            this.Data = data;
        }
        public OperationResult(T data, string message) : base(message)
        {
            this.Data = data;
        }
        public OperationResult(T data, bool isSuccess, string message) : base(isSuccess, message)
        {
            this.Data = data;
        }
        public OperationResult(T data, bool isSuccess) : base(isSuccess)
        {
            this.Data = data;
        }

        public OperationResult(string message) : base(message)
        {
        }
        public OperationResult(bool isSuccess, string message) : base(isSuccess, message)
        {
        }
        public OperationResult(bool isSuccess) : base(isSuccess)
        {
        }
        public T Data { get; set; }
    }
}
