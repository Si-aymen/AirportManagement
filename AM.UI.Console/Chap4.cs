using AM.ApplicationCore;
using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Services;
using AM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Console
{
    internal class Chap4
    {
        public static void Test1()
        {
            using(var unitOfWork = new UnitOfWork())
            {
                unitOfWork.Repository<Plane>().Add(InMemorySource.Boeing1);
                unitOfWork.Repository<Plane>().Add(InMemorySource.Boeing2);
                unitOfWork.Commit();
                unitOfWork.Repository<Plane>().Add(InMemorySource.Airbus);
            }
        }
        public static void Test2()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                ShowLine showLine = System.Console.WriteLine;

                new FlightService(unitOfWork).GetFlightsByDestination("Paris").ShowList("== GetFlightsByDestination ==", showLine);

                var planeService = new PlaneService(unitOfWork);

                planeService.GetOldPlanes().ShowList("== GetOldPlanes ==", showLine);
                planeService.GetFlights(200).ShowList("== GetFlights ==", showLine);
            }
        }
    }
}
