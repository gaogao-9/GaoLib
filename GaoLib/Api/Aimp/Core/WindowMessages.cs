namespace GaoLib.Api.Aimp.Core
{
    /// <summary>
    /// 各種ウィンドウメッセージを示します。
    /// </summary>
    public enum WindowMessages : uint
    {
        /// <summary>
        /// コマンドを示すウインドウメッセージです。
        /// </summary>
        Command = (uint)Win32.WindowMessages.USER + 0x75,
        /// <summary>
        /// 通知を示すウインドウメッセージです。
        /// </summary>
        Notify = (uint)Win32.WindowMessages.USER + 0x76,
        /// <summary>
        /// プロパティを示すウインドウメッセージです。
        /// </summary>
        Property = (uint)Win32.WindowMessages.USER + 0x77,
        /// <summary>
        /// 飛んできたCOPYDATASTRUCTのdwDataがこのIDの値と等しい時に、アルバムアートを示します。
        /// </summary>
        CopyDataCoverId = 0x41495043,
    }
}
