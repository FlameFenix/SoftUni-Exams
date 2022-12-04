using System;
using TripAdministrations;

namespace Tests
{
    class Program
    {

        static void Main(string[] args)
        {
            Company c1 = new Company("a", 2);
            Company c2 = new Company("b", 1);
            Company c3 = new Company("c", 1);
            Company c4 = new Company("d", 2);

            Trip t1 = new Trip("a", 1, Transportation.NONE, 1);
            Trip t2 = new Trip("b", 1, Transportation.BUS, 1);
            Trip t3 = new Trip("c", 1, Transportation.BUS, 1);
            Trip t4 = new Trip("d", 1, Transportation.BUS, 1);

            TripAdministrator tripAdministrator = new TripAdministrator();

            tripAdministrator.AddCompany(c1);
            tripAdministrator.AddCompany(c2);

            tripAdministrator.AddTrip(c1, t1);

            Console.WriteLine(string.Join(' ', tripAdministrator.GetCompanies()));
            Console.WriteLine(string.Join(' ', tripAdministrator.GetTripsWithTransportationType(Transportation.NONE)));
        }
    }
}
