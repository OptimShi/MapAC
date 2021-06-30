using MapAC.DatLoader.FileTypes;
using System;
using System.IO;

namespace MapAC.DatLoader
{
    public static class DatManager
    {
        private static string DatFile;
        public static string Iteration { get { return GetIteration(); } }

        private static int count;

        // A padding given to all ACDM portal.dat items exported to avoid any potential conflicts
        public const uint ACDM_OFFSET = 0x10000;

        public static CellDatDatabase CellDat { get; private set; }
        public static DatDatabaseType DatType;
        public static DatVersionType DatVersion;
        private static readonly uint DAT_HEADER_OFFSET_TOD = 0x140;
        private static readonly uint DAT_HEADER_OFFSET_ACDM = 0x12C;

        public static bool Initialize(string cellDatFile)
        {
            try
            {
                DatFile = cellDatFile;
                return GetDatVersion();
            }
            catch (FileNotFoundException ex)
            {
                return false;
            }
        }

        public static void ReadDatFile()
        {
            System.Collections.Generic.List<uint> temp = new System.Collections.Generic.List<uint>();
            using (FileStream stream = new FileStream(DatFile, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream, System.Text.Encoding.Default, true)) {
                for (var i = 0; i < 512; i++)
                {
                    temp.Add(reader.ReadUInt32());
                }
            }
            }

            CellDat = new CellDatDatabase(DatFile, false);
            count = CellDat.AllFiles.Count;

            switch (CellDat.Blocksize)
            {
                case 0x100:
                    DatType = DatDatabaseType.Cell;
                    break;
                default:
                    DatType = DatDatabaseType.Portal;
                    break;
            }

        }

        private static bool GetDatVersion()
        {
            using (FileStream stream = new FileStream(DatFile, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream, System.Text.Encoding.Default, true))
                {
                    stream.Seek(DAT_HEADER_OFFSET_TOD, SeekOrigin.Begin);
                    uint TB = reader.ReadUInt32();
                    if (TB == 0x5442)
                    {
                        DatVersion = DatVersionType.ACTOD;
                        return true;
                    }
                    else
                    {
                        stream.Seek(DAT_HEADER_OFFSET_ACDM, SeekOrigin.Begin);
                        TB = reader.ReadUInt32();
                        if (TB == 0x5442)
                        {
                            DatVersion = DatVersionType.ACDM;
                            return true;
                        }
                    }
                }
                stream.Close();
                stream.Dispose();
            }
            return false;

        }

        /// <summary>
        /// Gets the latest iteration from the dat file (basically, it functions like internal versioning system)
        ///
        /// Per InterrogationResponse generated by the client:
        /// PORTAL.DAT is 2072
        /// client_English.dat (idDatFile.Type = 1, idDatFile.Id = 3) = 994
        /// Cell.dat is 982
        ///
        /// This returns:
        /// Cell: 538
        /// portal: 2072
        /// highres: 470
        /// Language: 875
        /// </summary>
        /// <returns></returns>
        private static string GetIteration()
        {
            if (DatVersion == DatVersionType.ACDM)
            {
                return CellDat.Header_ACDM.Iteration.ToString(); ;
            }
            else if(DatVersion == DatVersionType.ACTOD)
            {
                var iteration = CellDat.ReadFromDat<Iteration>(FileTypes.Iteration.FILE_ID);
                return iteration.ToString();
            }

            return "";
        }
    }
}
