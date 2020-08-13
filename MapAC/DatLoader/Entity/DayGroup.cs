using System.Collections.Generic;
using System.IO;

namespace MapAC.DatLoader.Entity
{
    public class DayGroup : IUnpackable
    {
        public float ChanceOfOccur { get; private set; }
        public string DayName { get; private set; }
        public List<SkyObject> SkyObjects { get; } = new List<SkyObject>();
        public List<SkyTimeOfDay> SkyTime { get; } = new List<SkyTimeOfDay>();

        public void Unpack(BinaryReader reader)
        {
            ChanceOfOccur   = reader.ReadSingle();
            DayName         = reader.ReadPString();
            reader.AlignBoundary();

            SkyObjects.Unpack(reader);
            SkyTime.Unpack(reader);
        }

        public void Pack(BinaryWriter writer)
        {
            writer.Write(ChanceOfOccur);
            writer.WritePString(DayName); writer.AlignBoundary();

            SkyObjects.Pack(writer);
            SkyTime.Pack(writer);
        }

    }
}
