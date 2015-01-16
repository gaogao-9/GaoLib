namespace GaoLib.Api.Aimp.Core
{
    /// <summary>
    /// プロパティの方式を指定します。
    /// </summary>
    [System.Flags]
    public enum PropValue : uint
    {
        /// <summary>
        /// プロパティを取得する値です。
        /// プロパティIDと｢|｣(OR演算)で結びつけます。
        /// </summary>
        Get = 0,
        /// <summary>
        /// プロパティを設定する値です。
        /// プロパティIDと｢|｣(OR演算)で結びつけます。
        /// </summary>
        Set = 1,

        /// <summary>
        /// プロパティ方式を抽出する際に用いるビットマスクです。
        /// </summary>
        Mask = 0xFFFFFFF0,
    }
}
