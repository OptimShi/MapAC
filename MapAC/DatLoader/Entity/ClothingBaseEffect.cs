using System.Collections.Generic;
using System.IO;

namespace MapAC.DatLoader.Entity
{
    public class ClothingBaseEffect : IUnpackable
    {
        public List<CloObjectEffect> CloObjectEffects { get; } = new List<CloObjectEffect>();

        public void Unpack(BinaryReader reader)
        {
            CloObjectEffects.Unpack(reader);
        }
    }
}
