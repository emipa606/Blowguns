using Verse;

namespace Blowguns
{
    // Token: 0x02000003 RID: 3
    public class HediffGiver_ProgessiveBodySizeDependant : HediffGiver
    {
        // Token: 0x04000001 RID: 1
        public string dependantHediffDef;

        // Token: 0x04000005 RID: 5
        public float maxSeverityFactor = 1f;

        // Token: 0x04000004 RID: 4
        public float minSeverityFactor = 0f;

        // Token: 0x04000002 RID: 2
        public string partToAffect;

        // Token: 0x04000006 RID: 6
        public float severityPerDayRecovering = -1f;

        // Token: 0x04000003 RID: 3
        public float severityPerSeverityPerDay = 1f;

        // Token: 0x06000004 RID: 4 RVA: 0x00002150 File Offset: 0x00000350
        public override void OnIntervalPassed(Pawn pawn, Hediff cause)
        {
            var hediffSet = pawn.health.hediffSet;
            var firstHediffOfDef = hediffSet.GetFirstHediffOfDef(HediffDef.Named(dependantHediffDef));
            var sevOffset = severityPerDayRecovering * 0.00333333341f;
            if (hediffSet.HasHediff(HediffDef.Named(dependantHediffDef)))
            {
                var num = severityPerSeverityPerDay * 0.00333333341f;
                var severity = firstHediffOfDef.Severity;
                var bodySize = pawn.BodySize;
                var sevOffset2 = severity * num / bodySize;
                var num2 = minSeverityFactor * bodySize;
                var num3 = maxSeverityFactor * firstHediffOfDef.Severity / bodySize;
                if (!pawn.Dead && hediffSet.HasHediff(HediffDef.Named(dependantHediffDef)) &&
                    !hediffSet.HasHediff(hediff))
                {
                    HealthUtility_ProgessiveBodySizeDependant.AdjustSeverityPerSeverity(pawn, hediff, sevOffset2,
                        partToAffect);
                }
                else
                {
                    if (pawn.Dead || !hediffSet.HasHediff(HediffDef.Named(dependantHediffDef)))
                    {
                        return;
                    }

                    var firstHediffOfDef2 = hediffSet.GetFirstHediffOfDef(hediff);
                    if (firstHediffOfDef.Severity > num2 && firstHediffOfDef2.Severity <= num3)
                    {
                        HealthUtility_ProgessiveBodySizeDependant.AdjustSeverityPerSeverity(pawn, hediff,
                            sevOffset2, partToAffect);
                    }
                }
            }
            else
            {
                HealthUtility_ProgessiveBodySizeDependant.AdjustSeverityPerSeverity(pawn, hediff, sevOffset,
                    partToAffect);
            }
        }
    }
}