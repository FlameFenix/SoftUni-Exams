using System;
using System.Collections.Generic;
using System.Linq;

namespace BarberShop
{

    public class BarberShop : IBarberShop
    {
        private Dictionary<string, Barber> barbers = new Dictionary<string, Barber>();
        private Dictionary<string, Client> clients = new Dictionary<string, Client>();

        public void AddBarber(Barber b)
        {
            if (barbers.ContainsKey(b.Name))
            {
                throw new ArgumentException();
            }

            barbers.Add(b.Name, b);
        }

        public void AddClient(Client c)
        {
            if (clients.ContainsKey(c.Name))
            {
                throw new ArgumentException();
            }

            clients.Add(c.Name, c);
        }

        public bool Exist(Barber b) => barbers.ContainsKey(b.Name);

        public bool Exist(Client c) => clients.ContainsKey(c.Name);

        public IEnumerable<Barber> GetBarbers() => barbers.Values.ToArray();

        public IEnumerable<Client> GetClients() => clients.Values.ToArray();

        public void AssignClient(Barber b, Client c)
        {
            if (!Exist(b) || !Exist(c))
            {
                throw new ArgumentException();
            }

            barbers[b.Name].Clients.Add(c);
            clients[c.Name].Barber = b;

        }

        public void DeleteAllClientsFrom(Barber b)
        {
            if (!Exist(b))
            {
                throw new ArgumentException();
            }

            barbers[b.Name].Clients = new HashSet<Client>();

            var list = clients.Values.Where(x => x.Barber.Name == b.Name).ToList();

            foreach (var client in list)
            {
                client.Barber = null;
            }
        }

        public IEnumerable<Client> GetClientsWithNoBarber()
            => clients.Values.Select(x => x).Where(x => x.Barber == null).ToArray();

        public IEnumerable<Barber> GetAllBarbersSortedWithClientsCountDesc()
         => barbers.Values.Select(x => x).OrderByDescending(x => x.Clients.Count).ToArray();



        public IEnumerable<Barber> GetAllBarbersSortedWithStarsDecsendingAndHaircutPriceAsc()
        => barbers.Values.Select(x => x).OrderByDescending(x => x.Stars).ThenBy(x => x.HaircutPrice).ToArray();

        public IEnumerable<Client> GetClientsSortedByAgeDescAndBarbersStarsDesc()
        => clients.Values.Select(x => x).OrderByDescending(x => x.Age).ThenByDescending(x => x.Barber.Stars).ToArray();
    }
}
