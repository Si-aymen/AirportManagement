using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Services
{
    public class PlaneService : Service<Plane>, IPlaneService
    {
        public PlaneService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {}
        public IEnumerable<Plane> GetOldPlanes()
        {
            return GetMany(e => e.ManufactureDate < DateTime.Now.AddDays(-365 * 3));
        }
        public IEnumerable<Flight> GetFlights(int planeCapacity)
        {
            var planes = GetMany(e => e.Capacity == planeCapacity).ToList();
            return planes.SelectMany(p => UnitOfWork.Repository<Flight>().GetMany(f => f.Plane == p));
        }
    }
}
