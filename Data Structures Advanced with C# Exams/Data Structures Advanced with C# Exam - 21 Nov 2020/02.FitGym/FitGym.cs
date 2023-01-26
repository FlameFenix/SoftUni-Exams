namespace _02.FitGym
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FitGym : IGym
    {
        private Dictionary<int, Member> members;
        private Dictionary<int, Trainer> trainers;

        public FitGym()
        {
            members = new Dictionary<int, Member>();
            trainers = new Dictionary<int, Trainer>();
        }

        public void AddMember(Member member)
        {
            if (members.ContainsKey(member.Id))
            {
                throw new ArgumentException();
            }

            members.Add(member.Id, member);
        }

        public void HireTrainer(Trainer trainer)
        {
            if (trainers.ContainsKey(trainer.Id))
            {
                throw new ArgumentException();
            }

            trainers.Add(trainer.Id, trainer);
        }

        public void Add(Trainer trainer, Member member)
        {
            if (!members.ContainsKey(member.Id))
            {
                members.Add(member.Id, member);
            }

            if(member.Trainer != null || !trainers.ContainsKey(trainer.Id))
            {
                throw new ArgumentException();
            }

            member.Trainer = trainer;
        }

        public bool Contains(Member member) => members.ContainsKey(member.Id);

        public bool Contains(Trainer trainer) => trainers.ContainsKey(trainer.Id);

        public Trainer FireTrainer(int id)
        {
            if (!trainers.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var trainer = trainers[id];

            trainers.Remove(id);

            return trainer;
        }

        public Member RemoveMember(int id)
        {
            if (!members.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var member = members[id];

            members.Remove(id);

            return member;
        }

        public int MemberCount => members.Count;
        public int TrainerCount => trainers.Count;

        public IEnumerable<Member>
            GetMembersInOrderOfRegistrationAscendingThenByNamesDescending()
        => members.Values.OrderBy(x => x.RegistrationDate).ThenBy(x => x.Name);

        public IEnumerable<Trainer> GetTrainersInOrdersOfPopularity()
            => trainers.Values.OrderBy(x => x.Popularity);

        public IEnumerable<Member>
            GetTrainerMembersSortedByRegistrationDateThenByNames(Trainer trainer)
            => members.Values.Where(x => x.Trainer.Equals(trainer))
                             .OrderBy(x => x.RegistrationDate)
                             .ThenBy(x => x.Name);

        public IEnumerable<Member> 
            GetMembersByTrainerPopularityInRangeSortedByVisitsThenByNames(int lo, int hi)
            => members.Values.Where(x => x.Trainer.Popularity >= lo && x.Trainer.Popularity <= hi)
                             .OrderBy(x => x.Visits)
                             .ThenBy(x => x.Name);

        public Dictionary<Trainer, HashSet<Member>> 
            GetTrainersAndMemberOrderedByMembersCountThenByPopularity()
        {
            var trainers = new Dictionary<Trainer, HashSet<Member>>();

            foreach (var member in members)
            {
                if (!trainers.ContainsKey(member.Value.Trainer))
                {
                    trainers.Add(member.Value.Trainer, new HashSet<Member>());
                }

                trainers[member.Value.Trainer].Add(member.Value);
            }

            trainers.OrderBy(x => x.Value.Count).ThenBy(x => x.Key.Popularity);

            return trainers;
        }
    }
}