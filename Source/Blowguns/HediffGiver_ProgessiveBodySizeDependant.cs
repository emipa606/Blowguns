using System;
using Verse;

namespace Blowguns
{
	// Token: 0x02000003 RID: 3
	public class HediffGiver_ProgessiveBodySizeDependant : HediffGiver
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002150 File Offset: 0x00000350
		public override void OnIntervalPassed(Pawn pawn, Hediff cause)
		{
			HediffSet hediffSet = pawn.health.hediffSet;
			Hediff firstHediffOfDef = hediffSet.GetFirstHediffOfDef(HediffDef.Named(this.dependantHediffDef), false);
			float sevOffset = this.severityPerDayRecovering * 0.00333333341f;
			bool flag = hediffSet.HasHediff(HediffDef.Named(this.dependantHediffDef), false);
			if (flag)
			{
				float num = this.severityPerSeverityPerDay * 0.00333333341f;
				float severity = firstHediffOfDef.Severity;
				float bodySize = pawn.BodySize;
				float sevOffset2 = severity * num / bodySize;
				float num2 = this.minSeverityFactor * bodySize;
				float num3 = this.maxSeverityFactor * firstHediffOfDef.Severity / bodySize;
				bool flag2 = !pawn.Dead;
				bool flag3 = hediffSet.HasHediff(HediffDef.Named(this.dependantHediffDef), false);
				bool flag4 = flag2 && flag3 && !hediffSet.HasHediff(this.hediff, false);
				if (flag4)
				{
					HealthUtility_ProgessiveBodySizeDependant.AdjustSeverityPerSeverity(pawn, this.hediff, sevOffset2, this.partToAffect);
				}
				else
				{
					bool flag5 = flag2 && flag3;
					if (flag5)
					{
						Hediff firstHediffOfDef2 = hediffSet.GetFirstHediffOfDef(this.hediff, false);
						bool flag6 = firstHediffOfDef.Severity > num2 && firstHediffOfDef2.Severity <= num3;
						if (flag6)
						{
							HealthUtility_ProgessiveBodySizeDependant.AdjustSeverityPerSeverity(pawn, this.hediff, sevOffset2, this.partToAffect);
						}
					}
				}
			}
			else
			{
				HealthUtility_ProgessiveBodySizeDependant.AdjustSeverityPerSeverity(pawn, this.hediff, sevOffset, this.partToAffect);
			}
		}

		// Token: 0x04000001 RID: 1
		public string dependantHediffDef;

		// Token: 0x04000002 RID: 2
		public string partToAffect;

		// Token: 0x04000003 RID: 3
		public float severityPerSeverityPerDay = 1f;

		// Token: 0x04000004 RID: 4
		public float minSeverityFactor = 0f;

		// Token: 0x04000005 RID: 5
		public float maxSeverityFactor = 1f;

		// Token: 0x04000006 RID: 6
		public float severityPerDayRecovering = -1f;
	}
}
