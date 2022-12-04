using BarberShop;
using NUnit.Framework;
using System.Diagnostics;

public class Test_01
{
    private BarberShop.BarberShop barberShop;
    private Barber b1 = new Barber("a", 1, 1);
    private Barber b2 = new Barber("b", 2, 3);
    private Barber b3 = new Barber("c", 2, 2);

    private Client c1 = new Client("a", 1, Gender.MALE);
    private Client c2 = new Client("b", 1, Gender.FEMALE);
    private Client c3 = new Client("c", 2, Gender.FEMALE);
    private Client c4 = new Client("d", 6, Gender.FEMALE);
    private Client c5 = new Client("e", 5, Gender.FEMALE);

    [SetUp]
    public void Setup()
    {
        this.barberShop = new BarberShop.BarberShop();
    }

    [Test]
    public void TestAddBarber()
    {
        this.barberShop.AddBarber(b1);
        Assert.True(this.barberShop.Exist(b1));
    }

    [Test]
    public void TestClientExist()
    {
        this.barberShop.AddClient(c1);
        Assert.True(this.barberShop.Exist(c1));
    }

    [Test]
    public void TestClientNotExist()
    {
        this.barberShop.AddClient(c1);
        Assert.False(this.barberShop.Exist(c2));
    }

    [Test]
    public void TestClientExistNotAddingAnything()
    {
        Assert.False(this.barberShop.Exist(c1));
    }

    [Test]
    public void TestRemoveBarbersClients()
    {


        barberShop.AddBarber(b1);
        barberShop.AddClient(c1);
        barberShop.AddClient(c2);
        barberShop.AddClient(c3);

        barberShop.AssignClient(b1, c1);
        barberShop.AssignClient(b1, c2);
        barberShop.AssignClient(b1, c3);


        Assert.True(b1.Clients.Count == 3);

        barberShop.DeleteAllClientsFrom(b1);

        Assert.True(b1.Clients.Count == 0);
    }

    [Test]
    public void TestAddBarberPerf()
    {
        

        Stopwatch sw = new Stopwatch();
        sw.Start(); 
        for (int i = 0; i < 10000; i++)
        {
            this.barberShop.AddBarber(new Barber(i.ToString(), i, i));
        }

        this.barberShop.AddBarber(b1);

        sw.Stop();

        Assert.IsTrue(sw.ElapsedMilliseconds <= 20);
    }

}
