using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApi.Domain.Model.Messages
{
    public class DeleteBLRequest<T>
    {
        public int Id { get; set; }
    }
    public class DeleteBLResponse<T>
    {
    }

    public class DeleteDaoRequest<T>
    {
        public int Id { get; set; }
    }
    public class DeleteDaoResponse<T>
    {
    }
}
