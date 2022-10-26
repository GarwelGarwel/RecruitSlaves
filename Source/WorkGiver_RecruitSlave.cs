using RimWorld;
using Verse;
using Verse.AI;

namespace RecruitSlaves
{
    public class WorkGiver_RecruitSlave : WorkGiver_Warden
    {
        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!ModLister.IdeologyInstalled)
                return null;
            if (!ShouldTakeCareOfSlave(pawn, t))
                return null;
            Pawn target = t as Pawn;
            if (target.guest.slaveInteractionMode != DefOf.Recruit || target.Downed || !target.Awake())
                return null;
            Job job = JobMaker.MakeJob(DefOf.Job_RecruitSlave, target);
            job.count = 1;
            return job;
        }
    }
}
