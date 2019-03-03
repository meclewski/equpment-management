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

        IQueryable<Producer> Producers { get; }
        IQueryable<Laboratory> Laboratories { get; }
        IQueryable<Verification> Verifications { get; }
      

        void SaveType(Type type);
        
        Task<Type> DeleteTypeAsync(int typeId);
    }
}
