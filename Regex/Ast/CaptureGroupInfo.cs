namespace RE
{
    public class CaptureGroupInfo
    {
        public CaptureGroupInfo(string captureName, int groupNumber)
        {
            CaptureName = captureName;
            GroupNumber = groupNumber;
        }

        public string CaptureName { get; set; }
        public int GroupNumber { get; }

        public CaptureGroupInfo Clone()
        {
            return new CaptureGroupInfo(CaptureName, GroupNumber);
        }
    }
}