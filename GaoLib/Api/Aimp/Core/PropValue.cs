namespace GaoLib.Api.Aimp.Core
{
    /// <summary>
    /// プロパティの方式を指定します。
    /// </summary>
    public static class PropValue
    {
        /// <summary>
        /// プロパティを取得する値です。
        /// プロパティIDと｢|｣(OR演算)で結びつけます。
        /// </summary>
        public const uint GET = 0;
        /// <summary>
        /// プロパティを設定する値です。
        /// プロパティIDと｢|｣(OR演算)で結びつけます。
        /// </summary>
        public const uint SET = 1;

        /// <summary>
        /// プロパティ方式を抽出する際に用いるビットマスクです。
        /// </summary>
        public const uint MASK = 0xFFFFFFF0;
    }
}
