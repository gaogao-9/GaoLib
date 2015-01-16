namespace GaoLib.Api.Aimp.Core
{
    public static class Command
    {
        /// <summary>
        /// コマンドの基本値です。
        /// 外部から見える必要はないです。
        /// </summary>
        private const uint BASE = 10;

        /// <summary>
        /// 通知登録を行うことを示すコマンドです。
        /// </summary>
        public const uint REGISTER_NOTIFY = Command.BASE + 1;
        /// <summary>
        /// 通知解除を行うことを示すコマンドです。
        /// </summary>
        public const uint UNREGISTER_NOTIFY = Command.BASE + 2;

        /// <summary>
        /// 再生を行うことを示すコマンドです。
        /// </summary>
        public const uint PLAY = Command.BASE + 3;
        /// <summary>
        /// 再生と一時停止の切り替えを行うことを示すコマンドです。
        /// </summary>
        public const uint PLAYPAUSE = Command.BASE + 4;
        /// <summary>
        /// 一時停止を行うことを示すコマンドです。
        /// </summary>
        public const uint PAUSE = Command.BASE + 5;
        /// <summary>
        /// 停止を行うことを示すコマンドです。
        /// </summary>
        public const uint STOP = Command.BASE + 6;
        /// <summary>
        /// 次の曲へ移動を行うことを示すコマンドです。
        /// </summary>
        public const uint NEXT = Command.BASE + 7;
        /// <summary>
        /// 前の曲へ移動を行うことを示すコマンドです。
        /// </summary>
        public const uint PREV = Command.BASE + 8;
        /// <summary>
        /// AIMPの終了を行うことを示すコマンドです。
        /// </summary>
        public const uint QUIT = Command.BASE + 11;

        /// <summary>
        /// 次の視覚効果へ移動を行うことを示すコマンドです。
        /// </summary>
        public const uint VISUAL_NEXT = Command.BASE + 9;
        /// <summary>
        /// 前の視覚効果へ移動を行うことを示すコマンドです。
        /// </summary>
        public const uint VISUAL_PREV = Command.BASE + 10;
        /// <summary>
        /// 視覚効果の開始を行うことを示すコマンドです。
        /// </summary>
        public const uint VISUAL_START = Command.BASE + 20;
        /// <summary>
        /// 視覚効果の停止を行うことを示すコマンドです。
        /// </summary>
        public const uint VISUAL_STOP = Command.BASE + 21;

        /// <summary>
        /// ファイル追加ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        public const uint ADD_FILES = Command.BASE + 12;
        /// <summary>
        /// フォルダ追加ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        public const uint ADD_FOLDERS = Command.BASE + 13;
        /// <summary>
        /// プレイリスト追加ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        public const uint ADD_PLAYLISTS = Command.BASE + 14;
        /// <summary>
        /// URL追加ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        public const uint ADD_URL = Command.BASE + 15;

        /// <summary>
        /// ファイル展開ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        public const uint OPEN_FILES = Command.BASE + 16;
        /// <summary>
        /// フォルダ展開ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        public const uint OPEN_FOLDERS = Command.BASE + 17;
        /// <summary>
        /// プレイリスト展開ダイアログの表示を行うことを示すコマンドです。
        /// </summary>
        public const uint OPEN_PLAYLISTS = Command.BASE + 18;

        /// <summary>
        /// アルバムアートを取得することを示すコマンドです。
        /// このコマンドは32bitアプリケーションでのみ動作します。
        /// </summary>
        public const uint GET_ALBUMART = Command.BASE + 19;
    }
}
