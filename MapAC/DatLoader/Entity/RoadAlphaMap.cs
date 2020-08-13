using System.IO;

namespace MapAC.DatLoader.Entity
{
    public class RoadAlphaMap : IUnpackable
    {
        public uint RCode { get; private set; }
        public uint RoadTexGID { get; private set; }

        public void Unpack(BinaryReader reader)
        {
            RCode = reader.ReadUInt32();
            RoadTexGID = reader.ReadUInt32();
        }

        public void Pack(BinaryWriter writer)
        {
            writer.Write(RCode);
            writer.Write(RoadTexGID);
        }
    }
}
