using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace RecruitSlaves
{
    public class JobDriver_RecruitSlave : JobDriver
    {
        Pawn Slave => job.GetTarget(TargetIndex.A).Pawn;

        public override bool TryMakePreToilReservations(bool errorOnFailed) => pawn.Reserve(Slave, job, errorOnFailed: errorOnFailed);

        protected override IEnumerable<Toil> MakeNewToils()
        {
            if (!ModLister.IdeologyInstalled)
            {
                Utility.Log($"Trying to MakeNewToils for {pawn} to recruit {Slave}, but there is no Ideology installed.", LogLevel.Error);
                yield return null;
            }

            Utility.Log($"Recruiting {Slave}.");
            this.FailOnDestroyedOrNull(TargetIndex.A);
            this.FailOn(() => Slave.guest.slaveInteractionMode != DefOf.Recruit);
            this.FailOnDowned(TargetIndex.A);
            this.FailOnAggroMentalState(TargetIndex.A);
            this.FailOnForbidden(TargetIndex.A);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch).FailOn(() => !Slave.IsSlaveOfColony || !Slave.guest.SlaveIsSecure).FailOnSomeonePhysicallyInteracting(TargetIndex.A);
        
            Toil toil = ToilMaker.MakeToil("TryRecruitSlave");
            toil.initAction = () => Utility.TryRecruit(pawn, Slave);
            toil.socialMode = RimWorld.RandomSocialMode.Off;
            toil.activeSkill = () => SkillDefOf.Social;
            toil.defaultCompleteMode = ToilCompleteMode.Delay;
            toil.defaultDuration = 350;
            yield return toil;
        }
    }
}
