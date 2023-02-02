using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InsightAR.Internal;

namespace Insight
{
	public static class ScreenExtension
	{
		public static int getNotchHeight(this Screen screen) {
			return 0;
		}

		public static int ImageWidth()
		{
			return InsightARInterface.GetInsightARSceenWidth();
		}

		public static int ImageHeight()
		{
			return InsightARInterface.GetInsightARSceenHeight();
		}
	}
}

