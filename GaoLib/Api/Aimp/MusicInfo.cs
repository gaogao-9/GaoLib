namespace GaoLib.Api.Aimp
{
    public class MusicInfo
    {
        public bool IsActive { set; get; }
        public uint BitRate { set; get; }
        public Channel Channel { set; get; }
        public uint Duration { set; get; }
        public ulong FileSize { set; get; }
        public uint SampleRate { set; get; }
        public uint TrackNumber { set; get; }

        public string Album { set; get; }
        public string Artist { set; get; }
        public string Year { set; get; }
        public string FilePath { set; get; }
        public string Genre { set; get; }
        public string Title { set; get; }
    }
}
