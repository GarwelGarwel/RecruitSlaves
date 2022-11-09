using RimWorld;
using System.Linq;
using UnityEngine;
using Verse;

namespace RecruitSlaves
{
    public class StatPart_AverageOpinionOfColonists : StatPart_Curve
    {
        protected override bool AppliesTo(StatRequest req) => req.Thing is Pawn pawn && pawn.needs?.mood?.thoughts != null;

        protected override float CurveXGetter(StatRequest req) =>
            (float)PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists_NoSlaves
            .Where(pawn => pawn != req.Thing)
            .Select(pawn => ((Pawn)req.Thing).needs.mood.thoughts.TotalOpinionOffset(pawn))
            .Average();

        protected override string ExplanationLabel(StatRequest req) => $"Opinion of colonists ({Mathf.RoundToInt(CurveXGetter(req)).ToStringWithSign()})";
    }
}
