using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApi.Domain.Model.Messages
{
    public class UpdateBLRequest<T>
    {
        public T Payload { get; set; }
    }
    public class UpdateBLResponse<T>
    {
    }

    public class UpdateDaoRequest<T>
    {
        public T Payload { get; set; }
    }
    public class UpdateDaoResponse<T>
    {
    }
}
