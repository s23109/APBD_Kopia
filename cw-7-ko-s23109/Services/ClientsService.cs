using cw_7_ko_s23109.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace cw_7_ko_s23109.Services
{
    public class ClientsService : IClientsService
    {

        private readonly MasterContext _context;

        public ClientsService(MasterContext context)
        {
            _context = context;
        }

       public async Task DeleteClient(int idClient)
        {

            var clientTrips = _context.ClientTrips.Where(e => e.IdClient == idClient);

            if (clientTrips.Any())
            {
                throw new Exception();
            }

            var client = new Client
            {
                IdClient = idClient
            };

            _context.Remove(client);
            await _context.SaveChangesAsync();

            

        }
    }
}
