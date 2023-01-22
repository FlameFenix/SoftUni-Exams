using System.Collections.Generic;

namespace Exam.MoovIt
{
    public class Route
    {
        public string Id { get; set; }

        public double Distance { get; set; }

        public int Popularity { get; set; }

        public bool IsFavorite { get; set; }

        public List<string> LocationPoints { get; set; } = new List<string>();

        public Route(string id, double distance, int popularity, bool isFavorite, List<string> locationPoints)
        {
            this.Id = id;
            this.Distance = distance;
            this.Popularity = popularity;
            this.IsFavorite = isFavorite;
            this.LocationPoints = locationPoints;
        }

        public override int GetHashCode()
        {
            return Distance.GetHashCode() * LocationPoints[0].GetHashCode() * LocationPoints[LocationPoints.Count - 1].GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Route;

            return other.Distance == this.Distance && other.Popularity == this.Popularity;
        }
    }
}
