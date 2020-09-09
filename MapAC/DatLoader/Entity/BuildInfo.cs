using System.Collections.Generic;
using System.IO;

namespace MapAC.DatLoader.Entity
{
    public class BuildInfo : IUnpackable
    {
        /// <summary>
        /// 0x01 or 0x02 model of the building
        /// </summary>
        public uint ModelId { get; set; }

        /// <summary>
        /// specific @loc of the model
        /// </summary>
        public Frame Frame { get; } = new Frame();

        /// <summary>
        /// unsure what this is used for
        /// </summary>
        public uint NumLeaves { get; set; }

        /// <summary>
        /// portals are things like doors, windows, etc.
        /// </summary>
        public List<CBldPortal> Portals { get; } = new List<CBldPortal>();

        public void Unpack(BinaryReader reader)
        {
            ModelId = reader.ReadUInt32();

            Frame.Unpack(reader);

            NumLeaves = reader.ReadUInt32();

            Portals.Unpack(reader);
        }

        public void Pack(BinaryWriter writer)
        {
            if(ModelId < 0x02000000)
                writer.WriteOffset(ModelId, Enum.ACDMOffset.GfxObj);
            else
                writer.WriteOffset(ModelId, Enum.ACDMOffset.Setup);

            Frame.Pack(writer);

            writer.Write(NumLeaves);

            Portals.Pack(writer);
        }

    }
}
