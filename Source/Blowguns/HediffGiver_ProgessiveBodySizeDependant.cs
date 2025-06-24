using Verse;

namespace Blowguns;

public class HediffGiver_ProgessiveBodySizeDependant : HediffGiver
{
    private readonly float maxSeverityFactor = 1f;

    private readonly float minSeverityFactor = 0f;

    private readonly float severityPerDayRecovering = -1f;

    private readonly float severityPerSeverityPerDay = 1f;
    public string dependantHediffDef;

    public string partToAffect;

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
            var minSeverity = minSeverityFactor * bodySize;
            var maxSeverity = maxSeverityFactor * firstHediffOfDef.Severity / bodySize;
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
                if (firstHediffOfDef.Severity > minSeverity && firstHediffOfDef2.Severity <= maxSeverity)
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