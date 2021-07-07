using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services.Interface
{
    interface iCRUD<T> where T : class
    {
        T Get(int Id);
        int Add(T obj);
        int Delete(int Id);
    }
}
