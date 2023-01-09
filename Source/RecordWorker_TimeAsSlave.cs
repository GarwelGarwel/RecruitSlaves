using RimWorld;
using Verse;

namespace RecruitSlaves
{
    public class RecordWorker_TimeAsColonySlave : RecordWorker
    {
        public override bool ShouldMeasureTimeNow(Pawn pawn) => pawn.IsSlaveOfColony;
    }
}
