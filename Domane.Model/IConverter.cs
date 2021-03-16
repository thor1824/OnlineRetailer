using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Or.Domain.Model
{
    public interface IConverter<T, U>
    {
        T Convert(U model);
        U Convert(T model);
    }
}
