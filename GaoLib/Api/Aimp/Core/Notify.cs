namespace GaoLib.Api.Aimp.Core
{
    /// <summary>
    /// 通知の種類を示します。
    /// CommandのRESISTER_NOTIFYを用いて、通知登録をした際、飛んでくる通知の種類の判定に使います。
    /// </summary>
    public enum Notify : int
    {
        /// <summary>
        /// <para>再生中の楽曲情報が変更されたことを示す通知です。</para>
        /// <para>[wp:不使用,lp:{0:テキスト情報の更新,1:アルバムアートの更新},res:不使用]</para>
        /// </summary>
        TrackInfo = 1,
        /// <summary>
        /// <para>新たにストリーミング再生が開始されたことを示す通知です。</para>
        /// <para>(ネットラジオの再生トラックが変更された場合にも通知されます。)</para>
        /// <para>[wp:不使用,lp:不使用,res:不使用]</para>
        /// </summary>
        TrackStart = 2,
        /// <summary>
        /// <para>何らかのプロパティが変更されたことを示す通知です。</para>
        /// <para>[wp:不使用,lp:PropertyのID,res:不使用]</para>
        /// </summary>
        Property = 3,
    }
}
