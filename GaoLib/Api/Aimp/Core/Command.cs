namespace GaoLib.Api.Aimp.Core
{
    public enum Command : uint
    {
        /// <summary>
        /// 通知登録を行うことを示すコマンドです。
        /// </summary>
        REGISTER_NOTIFY = 11,
        /// <summary>
        /// 通知解除を行うことを示すコマンドです。
        /// </summary>
        UNREGISTER_NOTIFY = 12,

        /// <summary>
        /// 再生を行うことを示すコマンドです。
        /// </summary>
        PLAY = 13,
        /// <summary>
        /// 再生と一時停止の切り替えを行うことを示すコマンドです。
        /// </summary>
        PLAYPAUSE = 14,
        /// <summary>
        /// 一時停止を行うことを示すコマンドです。
        /// </summary>
        PAUSE = 15,
        /// <summary>
        /// 停止を行うことを示すコマンドです。
        /// </summary>
        STOP = 16,
        /// <summary>
        /// 次の曲へ移動を行うことを示すコマンドです。
        /// </summary>
        NEXT = 17,
        /// <summary>
        /// 前の曲へ移動を行うことを示すコマンドです。
        /// </summary>
        PREV = 18,
        /// <summary>
        /// AIMPの終了を行うことを示すコマンドです。
        /// </summary>
        QUIT = 21,

        /// <summary>
        /// 次の視覚効果へ移動を行うことを示すコマンドです。
        /// </summary>
        VISUAL_NEXT = 19,
        /// <summary>
        /// 前の視覚効果へ移動を行うことを示すコマンドです。
        /// </summary>
        VISUAL_PREV = 20,
        /// <summary>
        /// 視覚効果の開始を行うことを示すコマンドです。
        /// </summary>
        VISUAL_START = 30,
        /// <summary>
        /// 視覚効果の停止を行うことを示すコマンドです。
        /// </summary>
        VISUAL_STOP = 31,

        /// <summary>
        /// ファイル追加ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        ADD_FILES = 22,
        /// <summary>
        /// フォルダ追加ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        ADD_FOLDERS = 23,
        /// <summary>
        /// プレイリスト追加ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        ADD_PLAYLISTS = 24,
        /// <summary>
        /// URL追加ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        ADD_URL = 25,

        /// <summary>
        /// ファイル展開ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        OPEN_FILES = 26,
        /// <summary>
        /// フォルダ展開ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        OPEN_FOLDERS = 27,
        /// <summary>
        /// プレイリスト展開ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        OPEN_PLAYLISTS = 28,

        /// <summary>
        /// アルバムアートを取得することを示すコマンドです。
        /// このコマンドは32bitアプリケーションでのみ動作します。
        /// </summary>
        GET_ALBUMART = 29,
    }
}
