using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Services
{
    public class TicketService : Service<Ticket>, ITicketService
    {
        public TicketService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {}
        public IEnumerable<Traveller> GetVIPTravellers()
        {
            return GetMany(e => e.VIP).Select(e => e.Passenger).OfType<Traveller>();
        }
    }
}
