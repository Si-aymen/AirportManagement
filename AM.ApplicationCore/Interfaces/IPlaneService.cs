using AM.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Interfaces
{
    public interface IPlaneService : IService<Plane>
    {
        IEnumerable<Plane> GetOldPlanes();
        IEnumerable<Flight> GetFlights(int planeCapacity);
    }
}
