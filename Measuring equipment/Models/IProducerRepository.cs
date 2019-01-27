using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Measuring_equipment.Models
{
    public interface IProducerRepository
    {
        IQueryable<Producer> Producers { get; }
    }
}
