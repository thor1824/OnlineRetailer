using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApi.Domain.Model.Messages
{
    public class GetBLRequest<T>
    {
        public int? Id { get; set; }
    }
    public class GetBLResponse<T>
    {
        public T Payload { get; set; }
    }

    public class GetDaoRequest<T>
    {
        public int? Id { get; set; }
    }
    public class GetDaoResponse<T>
    {
        public T Payload { get; set; }
    }
}
