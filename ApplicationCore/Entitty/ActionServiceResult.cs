using System;
using System.Collections.Generic;
using System.Text;
using static ApplicationCore.Enums.Enumration;

namespace ApplicationCore.Entity
{
    public class ActionServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Code Code { get; set; }
        public object Data { get; set; }
        public int TotalRecords { get; set; }

        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        public ActionServiceResult()
        {
            Success = true;
            Message = "Success";
            Code = Code.Success;
            Data = null;
            TotalRecords = 0;
        }
    }
}
