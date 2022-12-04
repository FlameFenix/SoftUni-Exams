using System;
using System.Collections.Generic;
using System.Linq;

namespace TripAdministrations
{
    public class TripAdministrator : ITripAdministrator
    {
        private Dictionary<string, Company> companies = new Dictionary<string, Company>();

        private Dictionary<string, Trip> trips = new Dictionary<string, Trip>();

        public void AddCompany(Company c)
        {
            if (Exist(c))
            {
                throw new ArgumentException();
            }

            companies.Add(c.Name, c);
        }

        public void AddTrip(Company c, Trip t)
        {
            if(!Exist(c))
            {
                throw new ArgumentException();
            }

            companies[c.Name].Trips.Add(t);
            trips.Add(t.Id, t);
            trips[t.Id].Company = c;
        }

        public bool Exist(Company c) => companies.ContainsKey(c.Name);

        public bool Exist(Trip t) => trips.ContainsKey(t.Id);

        public void RemoveCompany(Company c)
        {
            if (!Exist(c))
            {
                throw new ArgumentException();
            }

            companies.Remove(c.Name);

            var tripsList = trips.Values.Where(x => x.Company.Name == c.Name).ToList();

            foreach (var trip in tripsList)
            {
                trips.Remove(trip.Id);
            }

        }

        public IEnumerable<Company> GetCompanies() => companies.Values.ToArray();

        public IEnumerable<Trip> GetTrips() => trips.Values.ToArray();

        public void ExecuteTrip(Company c, Trip t)
        {
            if(!Exist(c) || 
                !Exist(t) || 
                c.Name != t.Company.Name)
            {
                throw new ArgumentException();
            }

            companies[c.Name].Trips.Remove(t);
            trips.Remove(t.Id);
        }

        public IEnumerable<Company> GetCompaniesWithMoreThatNTrips(int n)
         => companies.Values.Where(x => x.Trips.Count > n).ToArray();

        public IEnumerable<Trip> GetTripsWithTransportationType(Transportation t)
            => trips.Values.Where(x => x.Transportation.Equals(t)).ToArray();

        public IEnumerable<Trip> GetAllTripsInPriceRange(int lo, int hi)
        => trips.Values.Where(x => x.Price >= lo && x.Price <= hi).ToArray();
    }
}
