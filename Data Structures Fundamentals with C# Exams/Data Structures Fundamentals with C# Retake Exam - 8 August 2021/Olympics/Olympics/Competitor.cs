using System;

public class Competitor
{
    public Competitor(int id, string name)
    {
        this.Id = id;
        this.Name = name;
        this.TotalScore = 0;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public long TotalScore { get; set; }

    public override bool Equals(object obj)
    {
        return base.Equals(obj as Competitor);
    }

    public bool Equals(Competitor obj)
    {
        return Id == obj.Id;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, TotalScore);
    }
}
