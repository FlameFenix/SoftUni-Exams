using System;
using System.Collections.Generic;
using System.Linq;

public class Olympics : IOlympics
{
    private Dictionary<int, Competition> competitions = new Dictionary<int, Competition>();

    private Dictionary<int, Competitor> competitors = new Dictionary<int, Competitor>();

    public void AddCompetition(int id, string name, int participantsLimit)
    {
        if (competitions.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        competitions.Add(id, new Competition(name, id, participantsLimit));
    }

    public void AddCompetitor(int id, string name)
    {
        if (competitors.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        competitors.Add(id, new Competitor(id, name));
    }

    public void Compete(int competitorId, int competitionId)
    {
        CheckCompetitorExist(competitorId);
        CheckCompetitionExist(competitionId);

        var competitor = competitors[competitorId];

        var competition = competitions[competitionId];

        competitor.TotalScore += competition.Score;

        competitions[competition.Id].Competitors.Add(competitor);
    }

    public int CompetitionsCount() => competitions.Count;

    public int CompetitorsCount() => competitors.Count;

    public bool Contains(int competitionId, Competitor comp)
    {
        CheckCompetitionExist(competitionId);

        var competitor = GetCompetition(competitionId).Competitors.Where(x => x.Equals(comp)).FirstOrDefault();

        return competitor != null;
    }

    public void Disqualify(int competitionId, int competitorId)
    {
        CheckCompetitionExist(competitionId);
        CheckCompetitorExist(competitorId);

        var competition = competitions[competitionId];

        var competitor = competitors[competitorId];

        competitor.TotalScore -= competition.Score;

        competitions[competition.Id].Competitors.Remove(competitor);
    }

    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
    => competitors.Values.Where(x => x.TotalScore > min && x.TotalScore <= max).OrderBy(x => x.Id).ToList();

    public IEnumerable<Competitor> GetByName(string name)
    {
        var competitorsList = competitors.Values.Where(x => x.Name == name).OrderBy(x => x.Id).ToList();

        if(competitorsList.Count == 0)
        {
            throw new ArgumentException();
        }

        return competitorsList;
    }

    public Competition GetCompetition(int id)
    {

        CheckCompetitionExist(id);

        var competition = competitions[id];

        return competition;
    }
      

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
        => competitors.Values.Where(x => x.Name.Length >= min && x.Name.Length <= max).OrderBy(x => x.Id).ToList();

    private void CheckCompetitorExist(int id)
    {
        if (!competitors.ContainsKey(id))
        {
            throw new ArgumentException();
        }
    }

    private void CheckCompetitionExist(int id)
    {
        if (!competitions.ContainsKey(id))
        {
            throw new ArgumentException();
        }
    }
}