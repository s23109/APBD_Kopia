using cw_7_ko_s23109.DTOs;
using cw_7_ko_s23109.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw_7_ko_s23109.Services
{
    public class TripsService : ITripsService
    {

        private readonly MasterContext _context;

        public TripsService (MasterContext context)
        {
            _context = context;
        }

        

      public async Task<IEnumerable<TripDto>> GetTrips()
        {
            return await _context.Trips.OrderBy(e => e.DateFrom).Select(e => new TripDto {
                DateFrom = e.DateFrom,
                DateTo = e.DateTo,
                Description = e.Description,
                Name = e.Name,
                MaxPeople = e.MaxPeople,
                Countries = e.CountryTrips.Select( e => new DTOs.Country
                {
                    Name = e.IdCountryNavigation.Name,
                }).ToList(),
                Clients = e. ClientTrips.Select( e => new DTOs.Client
                {
                    FirstName = e.IdClientNavigation.FirstName,
                    LastName = e.IdClientNavigation.LastName,
                }).ToList()
            }).ToListAsync();
        }


        public async Task AddClient(ClientPost clientPost)
        {
            var client = new Models.Client
            {
                FirstName = clientPost.FirstName,
                LastName = clientPost.LastName,
                Email = clientPost.Email,
                Pesel = clientPost.Pesel,
                Telephone = clientPost.Telephone,

            };

            _context.Clients.Add(client);

            await _context.SaveChangesAsync();

        }

        public async Task AddClientToTrip(ClientPost newClient)
        {
            // czy pesel istnieje  +
            // czy klient nie ma już zapisanej tej wycieczki +
            // czy wycieczka istnieje +

            var clientsWithPesel = _context.Clients.Where(e => e.Pesel.Equals(newClient.Pesel));
            
            if (clientsWithPesel.Any())
            {
                await AddClient(newClient);
            }

           

            var trips = _context.Trips.Where(e => e.IdTrip == newClient.IdTrip);

            if (!trips.Any())
            {
                throw new Exception("Wycieczka nie istnieje");
            }

            var clientTrips = _context.ClientTrips.Join(_context.Clients ,cliT => cliT.IdClient , cli => cli.IdClient , (cliT,cli) => new
            {
               cli.Pesel,
               cliT.IdTrip
            }
            ).Where( e =>  e.IdTrip == newClient.IdTrip  && e.Pesel.Equals(newClient.Pesel));

            if (clientTrips.Any())
            {
                throw new Exception("Wycieczka już przypisana");
            }



            var newClientTrip = new ClientTrip
            {
                IdTrip = newClient.IdTrip,
                IdClient = _context.Clients.Where(e => e.Pesel == newClient.Pesel).Select(e => e.IdClient).ToList()[0],
                RegisteredAt = DateTime.Now,
                PaymentDate = newClient.PaymentDate,
            };

            _context.ClientTrips.Add(newClientTrip);

            await _context.SaveChangesAsync();




        }

       
    }
}
