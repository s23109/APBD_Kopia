using cw_7_ko_s23109.DTOs;
using cw_7_ko_s23109.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cw_7_ko_s23109.Services
{
    public interface ITripsService
    {

        public Task<IEnumerable<TripDto>> GetTrips();

        public Task AddClientToTrip(ClientPost clientPost);

        public Task AddClient(ClientPost clientPost);

    }
}
