namespace TheTankGame.Entities.Miscellaneous
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Parts.Contracts;
    using TheTankGame.Entities.Parts;

    public class VehicleAssembler : IAssembler
    {
        private readonly IList<IAttackModifyingPart> arsenalParts;
        private readonly IList<IDefenseModifyingPart> shellParts;
        private readonly IList<IHitPointsModifyingPart> enduranceParts;

        public VehicleAssembler()
        {
            this.arsenalParts = new List<IAttackModifyingPart>();
            this.shellParts = new List<IDefenseModifyingPart>();
            this.enduranceParts = new List<IHitPointsModifyingPart>();
        }

        public IReadOnlyCollection<IAttackModifyingPart> ArsenalParts
                                => this.arsenalParts.ToList().AsReadOnly();

        public IReadOnlyCollection<IDefenseModifyingPart> ShellParts
                                => this.shellParts.ToList().AsReadOnly();

        public IReadOnlyCollection<IHitPointsModifyingPart> EnduranceParts
                                => this.enduranceParts.ToList().AsReadOnly();

        public double TotalWeight
                         => ArsenalParts.Count > 0 ? this.ArsenalParts.Max(p => p.Weight) : 0 +
                            ShellParts.Count > 0 ? this.ShellParts.Max(p => p.Weight) : 0 +
                            EnduranceParts.Count > 0 ? this.EnduranceParts.Max(p => p.Weight) : 0;

        public decimal TotalPrice
                         => ArsenalParts.Count > 0 ? this.ArsenalParts.Max(p => p.Price) : 0 +
                            ShellParts.Count > 0 ? this.ShellParts.Max(p => p.Price) : 0 +
                            EnduranceParts.Count > 0 ? this.EnduranceParts.Max(p => p.Price) : 0;

        public long TotalAttackModification
             => ArsenalParts.Count > 0 ? this.ArsenalParts.Max(p => p.AttackModifier) : 0;

        public long TotalDefenseModification
             => ShellParts.Count > 0 ? this.ShellParts.Max(p => p.DefenseModifier) : 0;

        public long TotalHitPointModification
             => EnduranceParts.Count > 0 ? this.EnduranceParts.Max(p => p.HitPointsModifier) : 0;

        public void AddArsenalPart(IPart arsenalPart)
        {
            this.arsenalParts.Add((IAttackModifyingPart)arsenalPart);
        }

        public void AddShellPart(IPart shellPart)
        {
            this.shellParts.Add((IDefenseModifyingPart) shellPart);
        }

        public void AddEndurancePart(IPart endurancePart)
        {
            this.enduranceParts.Add((IHitPointsModifyingPart)endurancePart);
        }
    }
}