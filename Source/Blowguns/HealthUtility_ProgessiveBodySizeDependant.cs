using System;
using Verse;

namespace Blowguns
{
	// Token: 0x02000002 RID: 2
	internal class HealthUtility_ProgessiveBodySizeDependant
	{
		// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		public static void AdjustSeverityPerSeverity(Pawn pawn, HediffDef hdDef, float sevOffset, string partToAffect)
		{
			bool flag = sevOffset != 0f;
			if (flag)
			{
				Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(hdDef, false);
				bool flag2 = hediff != null;
				if (flag2)
				{
					Hediff hediff2 = hediff;
					hediff2.Severity += sevOffset;
				}
				else
				{
					bool flag3 = sevOffset > 0f;
					if (flag3)
					{
						hediff = HediffMaker.MakeHediff(hdDef, pawn, null);
						hediff.Severity = sevOffset;
						DamageInfo? damageInfo = null;
						pawn.health.AddHediff(hediff, pawn.RaceProps.body.AllParts.Find((BodyPartRecord x) => x.def == DefDatabase<BodyPartDef>.GetNamed(partToAffect, true)), damageInfo);
					}
				}
			}
		}
	}
}
