using System.Collections.Generic;

namespace Tulpar.Core.ModelsBase
{
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public T Data { get; set; }
        public ApiResult()
        {
            ErrorMessages = new List<string>();
        }
    }

    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Data { get; set; }

        public ApiResult()
        {
            ErrorMessages = new List<string>();
        }
    }
}
