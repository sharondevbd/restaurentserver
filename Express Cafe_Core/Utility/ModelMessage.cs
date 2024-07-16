using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Express_Cafe_Core.Utility
{
    //public interface IModelMessage
    //{

    //    public string Message { get; set; }
    //    public object EntityModel { get; set; }
    //}
    public class ModelMessage
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";


    }
}
