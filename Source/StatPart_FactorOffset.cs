using RimWorld;
using Verse;

namespace RecruitSlaves
{
    public abstract class StatPart_FactorOffset : StatPart
    {
        public float factor = 1;
        public float offset;

        protected abstract bool AppliesTo(StatRequest req);

        protected abstract string ExplanationLabel(StatRequest req);

        public override void TransformValue(StatRequest req, ref float val)
        {
            if (AppliesTo(req))
                val = val * factor + offset;
        }

        public override string ExplanationPart(StatRequest req)
        {
            if (AppliesTo(req))
            {
                string res = $"{ExplanationLabel(req)}: ";
                if (factor != 1)
                    res += $"x{factor.ToStringPercent()} ";
                if (offset != 0)
                    res += offset.ToStringWithSign();
                return res;
            }
            return null;
        }
    }
}
