using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchool.Domain.Pattern.MementoPattern
{
    public interface IMemento
    {
        string GetState();
        string GetName();
    }
}
