using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Measuring_equipment.Models
{
    public interface ITypeRepository
    {
        IQueryable<Type> Types { get; }
        IQueryable<Type> TypesDT { get; }

        void SaveType(Type type);
        Type DeleteType(int typeId);
    }
}
