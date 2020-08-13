using System;
using System.IO;

namespace MapAC.DatLoader.Entity
{
    public class PlacementType : IUnpackable
    {
        public AnimationFrame AnimFrame { get; } = new AnimationFrame();

        /// <summary>
        /// You must use the Unpack(BinaryReader reader, int numParts) method.
        /// </summary>
        /// <exception cref="NotSupportedException">You must use the Unpack(BinaryReader reader, int numParts) method.</exception>
        public void Unpack(BinaryReader reader)
        {
            throw new NotSupportedException();
        }

        public void Unpack(BinaryReader reader, uint numParts)
        {
            AnimFrame.Unpack(reader, numParts);
        }

        public void Pack(BinaryWriter writer)
        {
            AnimFrame.Pack(writer);
        }

    }
}
