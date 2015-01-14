namespace GaoLib.Api.Aimp.Core
{
    /// <summary>
    /// 外部からAIMPを操作するためのAPI用の定数値を収めています
    /// </summary>
    public static class Remote
    {
        /// <summary>
        /// AIMP側が提供するRemoteAPI用のウィンドウクラス名です。
        /// </summary>
        public const string accessClassName = "AIMP2_RemoteInfo";

        /// <summary>
        /// 再生中の曲情報を取得する際に用いる、File Mappingのサイズ。
        /// いかなる曲においても、曲情報はこのサイズに切り詰められる。
        /// </summary>
        public const uint accessMapFileSize = 2048;
    }
}
