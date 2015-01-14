namespace GaoLib.Api.Aimp.Core
{
    /// <summary>
    /// 各種ウィンドウメッセージを示します。
    /// </summary>
    public static class WindowMessages
    {
        /// <summary>
        /// コマンドを示すウインドウメッセージです。
        /// </summary>
        public const uint COMMAND = (uint)Win32.WindowMessages.USER + 0x75;
        /// <summary>
        /// 通知を示すウインドウメッセージです。
        /// </summary>
        public const uint NOTIFY = (uint)Win32.WindowMessages.USER + 0x76;
        /// <summary>
        /// プロパティを示すウインドウメッセージです。
        /// </summary>
        public const uint PROPERTY = (uint)Win32.WindowMessages.USER + 0x77;
        /// <summary>
        /// 飛んできたCOPYDATASTRUCTのdwDataがこのIDの値と等しい時に、アルバムアートを示します。
        /// </summary>
        public const uint COPYDATA_COVER_ID = 0x41495043;
    }
}
