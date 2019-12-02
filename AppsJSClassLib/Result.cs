using System;
using System.Collections.Generic;
using System.Text;

namespace AppsJSClassLibStandard
{
    public class Result
    {
        public Result()
        {
            Logs = new List<string>();
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public List<string> Logs { get; set; }
    }
}
