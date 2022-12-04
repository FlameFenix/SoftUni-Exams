using System;
using System.Collections.Generic;

namespace TripAdministrations
{
    public class Company
    {
        public Company(string name, int tripOrganizationLimit)
        {
            this.Name = name;
            this.TripOrganizationLimit = tripOrganizationLimit;
            Trips = new HashSet<Trip>();
        }

        public string Name { get; set; }

        public int TripOrganizationLimit { get; set; }

        public int CurrentTrips { get; set; }

        public HashSet<Trip> Trips { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
