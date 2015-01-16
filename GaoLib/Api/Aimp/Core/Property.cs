namespace GaoLib.Api.Aimp.Core
{
    /// <summary>
    /// 各種設定値を取得したり設定したりできます。
    /// Notifyで飛んでくる通知プロパティの判定にも使います。
    /// </summary>
    public enum Property : uint
    {
        /// <summary>
        /// <para>(読み取り専用)プレイヤーのバージョンを示すプロパティです</para>
        /// <para>[下2桁:マイナーバージョン,残り:メジャーバージョン]</para>
        /// </summary>
        Version = 0x10,

        /// <summary>
        /// <para>再生中の曲の位置を示すプロパティです(単位:ミリ秒)</para>
        /// </summary>
        Position = 0x20,
        /// <summary>
        /// <para>(読み取り専用)再生中の曲の長さを示すプロパティです(単位:ミリ秒)</para>
        /// </summary>
        Duration = 0x30,
        /// <summary>
        /// <para>(読み取り専用)曲の再生状態を示すプロパティです</para>
        /// <para>[0:停止中,1:一時停止中,2:再生中]</para>
        /// </summary>
        State = 0x40,

        /// <summary>
        /// <para>音量を示すプロパティです(単位:%)</para>
        /// <para>(GET:[lp:不使用,res:0-100])</para>
        /// <para>(SET:[lp:0-100,res:{0以外:成功,0:失敗}])</para>
        /// </summary>
        Volume = 0x50,
        /// <summary>
        /// <para>ミュートを示すプロパティです</para>
        /// <para>(GET:[lp:不使用,res:{0以外:ミュート,0:非ミュート}])</para>
        /// <para>(SET:[lp:{0以外:ミュート,0:非ミュート},res:不使用])</para>
        /// </summary>
        IsMute = 0x60,

        /// <summary>
        /// <para>リピート再生を示すプロパティです</para>
        /// <para>(GET:[lp:不使用,res:{0以外:リピート再生,0:一巡再生}])</para>
        /// <para>(SET:[lp:{0以外:リピート再生,0:一巡再生},res:不使用])</para>
        /// </summary>
        IsRepeat = 0x70,
        /// <summary>
        /// <para>シャッフル再生を示すプロパティです</para>
        /// <para>(GET:[lp:不使用,res:{0以外:シャッフル再生,0:順次再生}])</para>
        /// <para>(SET:[lp:{0以外:シャッフル再生,0:順次再生},res:不使用])</para>
        /// </summary>
        IsShuffle = 0x80,
        /// <summary>
        /// <para>インターネットラジオの録音状態を示すプロパティです</para>
        /// <para>(GET:[lp:不使用,res:{0以外:録音,0:録音してない}])</para>
        /// <para>(SET:[lp:{0以外:録音,0:録音してない},res:不使用])</para>
        /// </summary>
        IsRadioCapture = 0x90,
        /// <summary>
        /// <para>全画面表示を示すプロパティです</para>
        /// <para>(GET:[lp:不使用,res:{0以外:全画面表示,0:通常表示}])</para>
        /// <para>(SET:[lp:{0以外:全画面表示,0:通常表示},res:不使用])</para>
        /// </summary>
        IsVisualFullScreen = 0xA0,
    }
}
