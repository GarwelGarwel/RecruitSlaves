using RimWorld;
using Verse;
using Verse.AI;

namespace RecruitSlaves
{
    public class WorkGiver_RecruitSlave : WorkGiver_Warden
    {
        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!ShouldTakeCareOfSlave(pawn, t))
                return null;
            if (pawn.IsSlave)
                return null;
            Pawn target = t as Pawn;
            if (target.guest.slaveInteractionMode != DefOf.Recruit || target.Downed || !target.Awake())
                return null;
            if (!target.guest.Recruitable)
            {
                Utility.Log($"{target} is not recruitable. Disabling Recruit slave interaction mode.");
                target.guest.slaveInteractionMode = SlaveInteractionModeDefOf.Suppress;
                Messages.Message($"{target} is not recruitable.", target, MessageTypeDefOf.RejectInput, false);
                return null;
            }
            if (target.mindState.lastSlaveSuppressedTick > Find.TickManager.TicksGame - Settings.RecruitmentAttemptCooldownTicks)
                return null;
            Job job = JobMaker.MakeJob(DefOf.Job_RecruitSlave, target);
            job.count = 1;
            return job;
        }
    }
}
