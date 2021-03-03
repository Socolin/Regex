using System.Collections.Generic;
using System.Text;

namespace RE
{
	public class CaptureGroupContext
	{
		public Dictionary<int, string> CaptureGroups { get; } = new Dictionary<int, string>();
		public Dictionary<int, int> CaptureGroupStartPosition { get; } = new Dictionary<int, int>();
		public Dictionary<int, string> CaptureGroupName { get; } = new Dictionary<int, string>();
		public HashSet<int> ActiveCaptureGroups { get; } = new HashSet<int>();

		public void StartCapture(CaptureGroupInfo captureGroupInfo, long contextPosition)
		{
			var groupNumber = captureGroupInfo.GroupNumber;
			if (ActiveCaptureGroups.Contains(groupNumber))
				return;
			ActiveCaptureGroups.Add(groupNumber);
			CaptureGroupStartPosition[groupNumber] = (int) contextPosition;
			if (captureGroupInfo.CaptureName != null)
				CaptureGroupName.Add(groupNumber, captureGroupInfo.CaptureName);
		}

		public void EndCapture(CaptureGroupInfo captureGroupInfo, StringBuilder capture)
		{
			var groupNumber = captureGroupInfo.GroupNumber;
			if (!ActiveCaptureGroups.Contains(groupNumber))
				return;
			var startCapturePosition = CaptureGroupStartPosition[groupNumber];
			CaptureGroupStartPosition.Remove(groupNumber);
			CaptureGroups[groupNumber] = capture.ToString(startCapturePosition, capture.Length - startCapturePosition);
		}

		public void Clear()
		{
			CaptureGroups.Clear();
			CaptureGroupStartPosition.Clear();
			ActiveCaptureGroups.Clear();
			CaptureGroupName.Clear();
		}
	}
}