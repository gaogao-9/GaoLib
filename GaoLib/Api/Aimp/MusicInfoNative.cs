namespace GaoLib.Api.Aimp
{
    internal class MusicInfoNative
    {
        public uint HeaderSize { set; get; }
        public uint Mask { set; get; }

        public uint AlbumStringLength { set; get; }
        public uint ArtistStringLength { set; get; }
        public uint YearStringLength { set; get; }
        public uint FilePathStringLength { set; get; }
        public uint GenreStringLength { set; get; }
        public uint TitleStringLength { set; get; }
    }
}
