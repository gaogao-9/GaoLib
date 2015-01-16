namespace GaoLib.Api.Aimp.Core
{
    public enum Command : uint
    {
        /// <summary>
        /// 通知登録を行うことを示すコマンドです。
        /// </summary>
        RegisterNotify = 11,
        /// <summary>
        /// 通知解除を行うことを示すコマンドです。
        /// </summary>
        UnRegisterNotify = 12,

        /// <summary>
        /// 再生を行うことを示すコマンドです。
        /// </summary>
        Play = 13,
        /// <summary>
        /// 再生と一時停止の切り替えを行うことを示すコマンドです。
        /// </summary>
        PlayPause = 14,
        /// <summary>
        /// 一時停止を行うことを示すコマンドです。
        /// </summary>
        Pause = 15,
        /// <summary>
        /// 停止を行うことを示すコマンドです。
        /// </summary>
        Stop = 16,
        /// <summary>
        /// 次の曲へ移動を行うことを示すコマンドです。
        /// </summary>
        Next = 17,
        /// <summary>
        /// 前の曲へ移動を行うことを示すコマンドです。
        /// </summary>
        Prev = 18,
        /// <summary>
        /// AIMPの終了を行うことを示すコマンドです。
        /// </summary>
        Quit = 21,

        /// <summary>
        /// 次の視覚効果へ移動を行うことを示すコマンドです。
        /// </summary>
        VisualNext = 19,
        /// <summary>
        /// 前の視覚効果へ移動を行うことを示すコマンドです。
        /// </summary>
        VisualPrev = 20,
        /// <summary>
        /// 視覚効果の開始を行うことを示すコマンドです。
        /// </summary>
        VisualStart = 30,
        /// <summary>
        /// 視覚効果の停止を行うことを示すコマンドです。
        /// </summary>
        VisualStop = 31,

        /// <summary>
        /// ファイル追加ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        AddFiles = 22,
        /// <summary>
        /// フォルダ追加ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        AddFolders = 23,
        /// <summary>
        /// プレイリスト追加ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        AddPlaylists = 24,
        /// <summary>
        /// URL追加ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        AddUrl = 25,

        /// <summary>
        /// ファイル展開ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        OpenFiles = 26,
        /// <summary>
        /// フォルダ展開ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        OpenFolders = 27,
        /// <summary>
        /// プレイリスト展開ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        OpenPlaylists = 28,

        /// <summary>
        /// アルバムアートを取得することを示すコマンドです。
        /// このコマンドは32bitアプリケーションでのみ動作します。
        /// </summary>
        GetAlbumart = 29,
    }
}
