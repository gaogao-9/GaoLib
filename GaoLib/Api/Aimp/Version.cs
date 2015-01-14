namespace GaoLib.Api.Aimp
{
    public struct VersionInfo
    {
        /// <summary>
        /// 開発番号を示します。
        /// </summary>
        public short Build;
        /// <summary>
        /// メジャーバージョンを示します。
        /// </summary>
        public short Major;
        /// <summary>
        /// マイナーバージョンを示します。
        /// 値は0~99の範囲です。
        /// </summary>
        public byte Minor;
        public VersionInfo(int val)
        {
            Build = (short)(val & 0x0000FFFF);
            Major = (short)(val >> 16);
            Minor = (byte)(Major % (short)100);
            Major /= (short)100;
        }

        public VersionInfo(short build, short major, byte minor)
        {
            Build = build;
            Major = major;
            Minor = minor;
        }
    }
}
