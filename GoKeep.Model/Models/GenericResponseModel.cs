using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Model
{
    public class GenericResponseModel<T>
    {
        public int ResponseCode { get; set; }
        public string Description { get; set; }
        public T Data { get; set; }
        public GenericResponseModel(T data)
        {
            Data = data;
        }
    }
    public class GenericResponseModel
    {
        public int ResponseCode { get; set; }
        public string Description { get; set; }
    }
}
