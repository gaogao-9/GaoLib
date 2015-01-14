using System;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace GaoLib.Api.Aimp
{
    public static class Remote
    {
        private static IntPtr _remoteWindowHandle = IntPtr.Zero;
        private static IntPtr _recieverWindowHandle = IntPtr.Zero;
        private static IntPtr _hInstance = IntPtr.Zero;
        private static byte[] _albumArt = null;

        static Remote()
        {
            Module[] modules = Assembly.GetEntryAssembly().GetModules();
            _hInstance = Marshal.GetHINSTANCE(modules[0]);
        }

        /// <summary>
        /// (読み取り専用)AIMP側が提供するAPIにアクセスするために用意されたウインドウハンドル。
        /// 普段は直接触れなくてもいいはずです。
        /// <exception cref="GaoLib.Api.Aimp.Exception.RemoteWindowNotFoundException"></exception>
        /// </summary>
        public static IntPtr RemoteWindowHandle
        {
            private set { }
            get
            {
                if (_remoteWindowHandle == IntPtr.Zero || !NativeMethods.IsWindow(_remoteWindowHandle))
                {
                    _remoteWindowHandle = NativeMethods.FindWindow(Core.Remote.accessClassName, null);
                }
                if (_remoteWindowHandle == IntPtr.Zero)
                {
                    throw new Exception.RemoteWindowNotFoundException("リモートウインドウが見つかりません。");
                }
                return _remoteWindowHandle;
            }
        }

        /// <summary>
        /// 主にウインドウプロシジャを受け取るための見えないウインドウのウインドウハンドル。
        /// </summary>
        private static IntPtr RecieverWindowHandle
        {
            set { }
            get
            {
                if (_recieverWindowHandle == IntPtr.Zero)
                {
                    _recieverWindowHandle = CreateRecieverWindow();
                }
                if (_recieverWindowHandle == IntPtr.Zero)
                {
                    throw new Exception.RecieverWindowException("RecieverWindowが作成できませんでした。");
                }
                return _recieverWindowHandle;
            }
        }

        #region プロパティ各種
        /// <summary>
        /// (読み取り専用)プレイヤーのバージョンを示すプロパティです。
        /// <exception cref="GaoLib.Api.Aimp.Exception.RemoteWindowNotFoundException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.RecieverWindowException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.MessageTimeoutException"></exception>
        /// </summary>
        public static VersionInfo Version
        {
            private set { }
            get
            {
                return new VersionInfo(GetProperty(Core.Property.VERSION).ToInt32());
            }
        }
        /// <summary>
        /// (読み取り専用)AIMPが起動しているかどうかを示すプロパティです。
        /// </summary>
        public static bool IsOpen
        {
            private set { }
            get
            {
                try
                {
                    var wnd = RemoteWindowHandle;
                }
                catch (Exception.RemoteWindowNotFoundException)
                {
                    return false;
                }

                return true;
            }
        }
        /// <summary>
        /// 曲の長さを示すプロパティです。単位は[ms]です。
        /// <exception cref="GaoLib.Api.Aimp.Exception.RemoteWindowNotFoundException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.RecieverWindowException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.MessageTimeoutException"></exception>
        /// </summary>
        public static int Duration
        {
            set
            {
                SetProperty(Core.Property.PLAYER_DURATION, new IntPtr(value));
            }
            get
            {
                return GetProperty(Core.Property.PLAYER_DURATION).ToInt32();
            }
        }
        /// <summary>
        /// 曲の再生位置を示すプロパティです。単位は[ms]です。
        /// <exception cref="GaoLib.Api.Aimp.Exception.RemoteWindowNotFoundException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.RecieverWindowException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.MessageTimeoutException"></exception>
        /// </summary>
        public static int Position
        {
            set
            {
                SetProperty(Core.Property.PLAYER_POSITION, new IntPtr(value));
            }
            get
            {
                return GetProperty(Core.Property.PLAYER_POSITION).ToInt32();
            }
        }
        /// <summary>
        /// プレイヤーの再生状態を示すプロパティです。
        /// <exception cref="GaoLib.Api.Aimp.Exception.RemoteWindowNotFoundException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.RecieverWindowException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.MessageTimeoutException"></exception>
        /// </summary>
        public static PlayerState State
        {
            private set { }
            get
            {
                return (PlayerState)GetProperty(Core.Property.PLAYER_STATE).ToInt32();
            }
        }
        /// <summary>
        /// 音量を示すプロパティです。値は0-100の範囲で、単位は[%]です。
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.RemoteWindowNotFoundException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.RecieverWindowException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.MessageTimeoutException"></exception>
        /// </summary>
        public static int Volume
        {
            set
            {
                if ((value < 0) || (value > 100))
                {
                    new ArgumentOutOfRangeException("音量は0～100[%]の範囲で指定してください。");
                }
                var _volume = Math.Max(0, Math.Min(value, 100));
                SetProperty(Core.Property.VOLUME, new IntPtr(_volume));
            }
            get
            {
                return GetProperty(Core.Property.VOLUME).ToInt32();
            }
        }
        /// <summary>
        /// ミュート状態を示すプロパティです。
        /// <exception cref="GaoLib.Api.Aimp.Exception.RemoteWindowNotFoundException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.RecieverWindowException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.MessageTimeoutException"></exception>
        /// </summary>
        public static bool Mute
        {
            set
            {
                SetProperty(Core.Property.MUTE, new IntPtr(value ? 1 : 0));
            }
            get
            {
                return GetProperty(Core.Property.MUTE) != IntPtr.Zero;
            }
        }
        /// <summary>
        /// リピート再生を示すプロパティです。
        /// <exception cref="GaoLib.Api.Aimp.Exception.RemoteWindowNotFoundException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.RecieverWindowException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.MessageTimeoutException"></exception>
        /// </summary>
        public static bool Repeat
        {
            set
            {
                SetProperty(Core.Property.TRACK_REPEAT, new IntPtr(value ? 1 : 0));
            }
            get
            {
                return GetProperty(Core.Property.TRACK_REPEAT) != IntPtr.Zero;
            }
        }
        /// <summary>
        /// ランダム再生を示すプロパティです。
        /// <exception cref="GaoLib.Api.Aimp.Exception.RemoteWindowNotFoundException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.RecieverWindowException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.MessageTimeoutException"></exception>
        /// </summary>
        public static bool Shuffle
        {
            set
            {
                SetProperty(Core.Property.TRACK_SHUFFLE, new IntPtr(value ? 1 : 0));
            }
            get
            {
                return GetProperty(Core.Property.TRACK_SHUFFLE) != IntPtr.Zero;
            }
        }
        /// <summary>
        /// インターネットラジオ録音を示すプロパティです。
        /// <exception cref="GaoLib.Api.Aimp.Exception.RemoteWindowNotFoundException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.RecieverWindowException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.MessageTimeoutException"></exception>
        /// </summary>
        public static bool RadioCapture
        {
            set
            {
                SetProperty(Core.Property.RADIOCAP, new IntPtr(value ? 1 : 0));
            }
            get
            {
                return GetProperty(Core.Property.RADIOCAP) != IntPtr.Zero;
            }
        }
        /// <summary>
        /// 全画面表示を示すプロパティです。
        /// <exception cref="GaoLib.Api.Aimp.Exception.RemoteWindowNotFoundException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.RecieverWindowException"></exception>
        /// <exception cref="GaoLib.Api.Aimp.Exception.MessageTimeoutException"></exception>
        /// </summary>
        public static bool VisualFullScreen
        {
            set
            {
                SetProperty(Core.Property.VISUAL_FULLSCREEN, new IntPtr(value ? 1 : 0));
            }
            get
            {
                return GetProperty(Core.Property.VISUAL_FULLSCREEN) != IntPtr.Zero;
            }
        }
        /// <summary>
        /// (読み取り専用)再生中の曲名やアーティスト名、ビットレートやサンプリングレート等々の情報を取得します。
        /// </summary>
        public static MusicInfo MusicInfo
        {
            private set { }
            get
            {
                string infoStr;
                var dataInfo = new MusicInfoNative();
                var musicInfo = new MusicInfo();

                try
                {
                    var mmf = MemoryMappedFile.OpenExisting(
                        Core.Remote.accessClassName,
                        MemoryMappedFileRights.ReadWrite,
                        HandleInheritability.Inheritable);

                    using (var mmvs = mmf.CreateViewStream(0, Core.Remote.accessMapFileSize))
                    {
                        byte[] buff;
                        buff = new byte[8];
                        mmvs.Read(buff, 0, 4);
                        dataInfo.HeaderSize = BitConverter.ToUInt32(buff, 0);
                        mmvs.Read(buff, 0, 4);
                        musicInfo.IsActive = BitConverter.ToUInt32(buff, 0) != 0;
                        mmvs.Read(buff, 0, 4);
                        musicInfo.BitRate = BitConverter.ToUInt32(buff, 0);
                        mmvs.Read(buff, 0, 4);
                        musicInfo.Channel = (Channel)BitConverter.ToUInt32(buff, 0);
                        mmvs.Read(buff, 0, 4);
                        musicInfo.Duration = BitConverter.ToUInt32(buff, 0);
                        mmvs.Read(buff, 0, 8);
                        musicInfo.FileSize = BitConverter.ToUInt64(buff, 0);
                        mmvs.Read(buff, 0, 4);
                        dataInfo.Mask = BitConverter.ToUInt32(buff, 0);
                        mmvs.Read(buff, 0, 4);
                        musicInfo.SampleRate = BitConverter.ToUInt32(buff, 0);
                        mmvs.Read(buff, 0, 4);
                        musicInfo.TrackNumber = BitConverter.ToUInt32(buff, 0);

                        mmvs.Read(buff, 0, 4);
                        dataInfo.AlbumStringLength = BitConverter.ToUInt32(buff, 0);
                        mmvs.Read(buff, 0, 4);
                        dataInfo.ArtistStringLength = BitConverter.ToUInt32(buff, 0);
                        mmvs.Read(buff, 0, 4);
                        dataInfo.YearStringLength = BitConverter.ToUInt32(buff, 0);
                        mmvs.Read(buff, 0, 4);
                        dataInfo.FilePathStringLength = BitConverter.ToUInt32(buff, 0);
                        mmvs.Read(buff, 0, 4);
                        dataInfo.GenreStringLength = BitConverter.ToUInt32(buff, 0);
                        mmvs.Read(buff, 0, 4);
                        dataInfo.TitleStringLength = BitConverter.ToUInt32(buff, 0);

                        mmvs.Position = dataInfo.HeaderSize;
                        buff = new byte[Core.Remote.accessMapFileSize - dataInfo.HeaderSize];
                        mmvs.Read(buff, 0, buff.Length);
                        infoStr = Encoding.Unicode.GetString(buff);
                    }

                    using (StringReader sr = new StringReader(infoStr))
                    {
                        char[] buff;
                        int len;

                        len = (int)(dataInfo.AlbumStringLength & 0x7FFFFFFF);
                        buff = new char[len];
                        sr.Read(buff, 0, len);
                        musicInfo.Album = new string(buff);
                        len = (int)(dataInfo.ArtistStringLength & 0x7FFFFFFF);
                        buff = new char[len];
                        sr.Read(buff, 0, len);
                        musicInfo.Artist = new string(buff);
                        len = (int)(dataInfo.YearStringLength & 0x7FFFFFFF);
                        buff = new char[len];
                        sr.Read(buff, 0, len);
                        musicInfo.Year = new string(buff);
                        len = (int)(dataInfo.FilePathStringLength & 0x7FFFFFFF);
                        buff = new char[len];
                        sr.Read(buff, 0, len);
                        musicInfo.FilePath = new string(buff);
                        len = (int)(dataInfo.GenreStringLength & 0x7FFFFFFF);
                        buff = new char[len];
                        sr.Read(buff, 0, len);
                        musicInfo.Genre = new string(buff);
                        len = (int)(dataInfo.TitleStringLength & 0x7FFFFFFF);
                        buff = new char[len];
                        sr.Read(buff, 0, len);
                        musicInfo.Title = new string(buff);
                    }
                }
                catch (FileNotFoundException)
                {
                    throw new Exception.RecieverWindowException("ファイルマッピング先を開くことが出来ませんでした");
                }
                catch (IOException)
                {
                    throw new Exception.RecieverWindowException("ファイルマッピングに失敗しました。");
                }
                return musicInfo;
            }
        }
        /// <summary>
        /// (読み取り専用)再生中のアルバムアートを取得します。
        /// </summary>
        public static byte[] AlbumArt
        {
            private set { }
            get
            {
                if(SendCommand(Core.Command.GET_ALBUMART,RecieverWindowHandle) == IntPtr.Zero)
                {
                    return null;
                }
                return _albumArt;
            }
        }
        #endregion

        //誰か氏～～～～～～～～ｗｗｗｗｗｗｗｗ誰か氏僕の代わりに<summary>付けてくれ～～～～～～ｗｗｗｗｗｗｗｗｗｗ
        #region コマンド各種
        public static void Show()
        {
            _Show();
        }
        public static void ShowSync(int delay = 100)
        {
            using (var p = _Show())
            {
                // アイドル状態を待つといったな？
                p.WaitForInputIdle();

                // アレは嘘だ。正しくはこれも書かなければならない。
                while (p.MainWindowHandle == IntPtr.Zero && p.HasExited == false)
                {
                    System.Threading.Thread.Sleep(1);
                    p.Refresh();
                }

                // アイドル移行直後に完了してしまうと、Playerの状態が不安定っぽいので、多少多めにdelayを挟むといい
                // 闇コードなのでいい解決方法探してるとこ。うーむ。
                System.Threading.Thread.Sleep(delay);
            }
        }
        public static void Play()
        {
            PostCommand(Core.Command.PLAY);
        }
        public static void PlayPause()
        {
            PostCommand(Core.Command.PLAYPAUSE);
        }
        public static void Pause()
        {
            PostCommand(Core.Command.PAUSE);
        }
        public static void Stop()
        {
            PostCommand(Core.Command.STOP);
        }
        public static void Next()
        {
            PostCommand(Core.Command.NEXT);
        }
        public static void Prev()
        {
            PostCommand(Core.Command.PREV);
        }
        public static void Close()
        {
            PostCommand(Core.Command.QUIT);
        }
        public static void VisualNext()
        {
            PostCommand(Core.Command.VISUAL_NEXT);
        }
        public static void VisualPrev()
        {
            PostCommand(Core.Command.VISUAL_PREV);
        }
        public static void VisualStart()
        {
            PostCommand(Core.Command.VISUAL_START);
        }
        public static void VisualStop()
        {
            PostCommand(Core.Command.VISUAL_STOP);
        }
        public static void AddFile()
        {
            PostCommand(Core.Command.ADD_FILES);
        }
        public static void AddDirectory()
        {
            PostCommand(Core.Command.ADD_FOLDERS);
        }
        public static void AddPlaylist()
        {
            PostCommand(Core.Command.ADD_PLAYLISTS);
        }
        public static void AddUri()
        {
            PostCommand(Core.Command.ADD_URL);
        }
        public static void OpenFile()
        {
            PostCommand(Core.Command.OPEN_FILES);
        }
        public static void OpenDirectory()
        {
            PostCommand(Core.Command.OPEN_FOLDERS);
        }
        public static void OpenPlaylist()
        {
            PostCommand(Core.Command.OPEN_PLAYLISTS);
        }
        #endregion

        #region 補助用のprivateメソッド各種
        private static Process _Show()
        {
            var filePath = ((string)Microsoft.Win32.Registry.GetValue("HKEY_CLASSES_ROOT\\Applications\\AIMP3.exe\\shell\\open\\command", null, null)).Split(new char[] { '"' })[1];
            if (String.IsNullOrEmpty(filePath))
            {
                throw new FileNotFoundException("AIMP3.exeが見つかりませんでした。インストールされてない可能性があります");
            }
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("AIMP3.exeが見つかりませんでした。ファイルが移動したか、削除された可能性があります");
            }
            var psi = new ProcessStartInfo();
            psi.FileName = filePath;
            psi.UseShellExecute = false;

            return Process.Start(psi);
        }
        private static IntPtr GetProperty(uint propId)
        {
            return SendProperty(propId, Core.PropValue.GET, IntPtr.Zero);
        }

        private static IntPtr SetProperty(uint propId, IntPtr value)
        {
            return SendProperty(propId, Core.PropValue.SET, value);
        }

        private static IntPtr SendProperty(uint propId, uint mode, IntPtr value)
        {
            return Remote.SendMessage(
                Core.WindowMessages.PROPERTY,
                new IntPtr(propId | mode),
                value);
        }

        private static void SendCommand(uint commandId)
        {
            SendCommand(commandId, IntPtr.Zero);
        }

        private static IntPtr SendCommand(uint commandId, IntPtr value)
        {
            return Remote.SendMessage(
                Core.WindowMessages.COMMAND,
                new IntPtr(commandId),
                value);
        }

        private static IntPtr SendMessage(uint wm, IntPtr param, IntPtr value)
        {
            IntPtr output;
            var res = NativeMethods.SendMessageTimeout(
                RemoteWindowHandle,
                wm,
                param,
                value,
                Win32.SendMessageTimeoutFlags.SMTO_NORMAL,
                1000,
                out output);

            if (res == IntPtr.Zero)
            {
                var res2 = Marshal.GetLastWin32Error();
                if (Marshal.GetLastWin32Error() == 0)
                {
                    throw new Exception.MessageTimeoutException("AIMPとの通信に失敗しました(タイムアウト)");
                }
                throw new Exception.MessageException("AIMPとの通信に失敗しました(原因不明)");
            }

            return output;
        }

        private static void PostCommand(uint commandId)
        {
            PostCommand(commandId, IntPtr.Zero);
        }

        private static void PostCommand(uint commandId, IntPtr value)
        {
            PostMessage(
                Core.WindowMessages.COMMAND,
                new IntPtr(commandId),
                value);
        }

        private static void PostMessage(uint wm, IntPtr param, IntPtr value)
        {
            if (!NativeMethods.PostMessage(RemoteWindowHandle, wm, param, value))
            {
                throw new Exception.MessageException("AIMPとの通信に失敗しました(原因不明)");
            }
        }
        #endregion

        /// <summary>
        /// 初回利用時に、受信用の見えないウインドウを作成します。
        /// </summary>
        private static IntPtr CreateRecieverWindow()
        {
            var className = "AIMPRemoteRecieverWindow{35476947-77B0-45DA-B648-062B0C0DB63F}";
            var wndClass = new NativeMethods.WindowClass();
            wndClass.lpfnWndProc = new NativeMethods.WndProc((hWnd, msg, wp, lp) =>
            {
                switch (msg)
                {
                    case Win32.WindowMessages.COPYDATA:
                        _albumArt = null;
                        var cds = new NativeMethods.CopyDataStruct();
                        cds = Marshal.PtrToStructure<NativeMethods.CopyDataStruct>(lp);
                        if (cds.dwData != new IntPtr(Core.WindowMessages.COPYDATA_COVER_ID)) break;
                        _albumArt = new byte[cds.cbData];
                        Marshal.Copy(cds.lpData, _albumArt, 0, (int)cds.cbData);
                        break;
                }
                return NativeMethods.DefWindowProc(hWnd, msg, wp, lp);
            });
            wndClass.cbClsExtra = wndClass.cbWndExtra = 0;
            wndClass.hInstance = _hInstance;
            wndClass.lpszMenuName = null;
            wndClass.lpszClassName = className;

            var classAtom = NativeMethods.RegisterClass(ref wndClass);

            if (classAtom == 0)
            {
                throw new Exception.RecieverWindowException("ウィンドウクラス名の登録に失敗しました");
            }

            return NativeMethods.CreateWindowEx(
                Win32.WindowStylesEx.WS_EX_LEFT,
                classAtom,
                "AIMPRemoteRecieverWindow",
                Win32.WindowStyles.WS_OVERLAPPEDWINDOW,
                0, 0, 100, 100,
                IntPtr.Zero, IntPtr.Zero, _hInstance, IntPtr.Zero
            );
        }

        /// <summary>
        /// Win32API関係のメソッドはここに打ち込むのが一般的らしい
        /// </summary>
        private static class NativeMethods
        {
            public delegate IntPtr WndProc(IntPtr hWnd, Win32.WindowMessages msg, IntPtr wParam, IntPtr lParam);

            /// <summary>
            /// ウインドウクラス
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct WindowClass
            {
                public Win32.ClassStyles style;
                [MarshalAs(UnmanagedType.FunctionPtr)]
                public WndProc lpfnWndProc;
                public int cbClsExtra;
                public int cbWndExtra;
                public IntPtr hInstance;
                public IntPtr hIcon;
                public IntPtr hCursor;
                public IntPtr hbrBackground;
                [MarshalAs(UnmanagedType.LPTStr)]
                public string lpszMenuName;
                [MarshalAs(UnmanagedType.LPTStr)]
                public string lpszClassName;
            }

            /// <summary>
            /// コピーデータ構造体。
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            internal struct CopyDataStruct
            {
                public IntPtr dwData;
                public UInt32 cbData;
                public IntPtr lpData;
            }

            /// <summary>
            /// 指定したウインドウハンドルが現在もなお存在しているかどうかを判定する関数です。
            /// </summary>
            /// <param name="hWnd">調べたいウインドウハンドル</param>
            /// <returns>存在すればtrue,無ければfalse</returns>
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool IsWindow(IntPtr hWnd);
            /// <summary>
            /// 指定したウィンドウのクラス名もしくはウィンドウ名を探し出し、HITしたウィンドウのハンドルを返します。
            /// </summary>
            /// <param name="lpClassName">クラス名</param>
            /// <param name="lpWindowName">ウィンドウ名(クラス名指定時null指定で全ウィンドウ対象)</param>
            /// <returns>ヒットしたウィンドウハンドル。見つからなかった時はIntPtr.Zeroを返す。</returns>
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            /// <summary>
            /// SendMessageにタイムアウト処理を付与した便利なSendMessageです。
            /// </summary>
            /// <param name="hWnd">送信先のウインドウハンドル</param>
            /// <param name="Msg">対象となるウインドウメッセージ</param>
            /// <param name="wParam">パラメータその1</param>
            /// <param name="lParam">パラメータその2</param>
            /// <param name="flags">タイムアウト方式を指定します。BLOCKは非推奨です。基本的にはNORMAL</param>
            /// <param name="timeout">タイムアウトまでの時間[単位:ms]</param>
            /// <param name="result">SendMessageで言うところの戻り値が得られます。</param>
            /// <returns>
            /// <para>成功したら0以外、失敗したら0、タイムアウトしたら0</para>
            /// <para>この値が0かつ、GetLastErrorで0だったらタイムアウトしたことになる。</para></returns>
            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessageTimeout(
                IntPtr hWnd,
                uint Msg,
                IntPtr wParam,
                IntPtr lParam,
                Win32.SendMessageTimeoutFlags flags,
                uint timeout,
                out IntPtr result);

            /// <summary>
            /// 非同期版SendMessageです。制御を即座に返します。
            /// </summary>
            /// <param name="hWnd">送信先のウインドウハンドル</param>
            /// <param name="Msg">対象となるウインドウメッセージ</param>
            /// <param name="wParam">パラメータその1</param>
            /// <param name="lParam">パラメータその2</param>
            /// <returns>
            /// <para>メッセージ依存のパラメータが帰ってきます</returns>
            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern bool PostMessage(
                IntPtr hWnd,
                uint Msg,
                IntPtr wParam,
                IntPtr lParam);

            /// <summary>
            /// 指定したウインドウクラスを、登録します。
            /// </summary>
            /// <param name="lpWndClass">登録したいウインドウクラス</param>
            /// <returns>成功したら0以外のウインドウクラスアトム値、失敗したら0</returns>
            [DllImport("user32.dll")]
            public static extern ushort RegisterClass([In] ref WindowClass lpWndClass);

            /// <summary>
            /// 指定したスタイルでウインドウを新たに作成します。
            /// </summary>
            /// <param name="dwExStyle">ウインドウスタイルのすごいやつ</param>
            /// <param name="lpClassName">ウインドウクラス名</param>
            /// <param name="lpWindowName">ウインドウ名(タイトル)</param>
            /// <param name="dwStyle">ウインドウスタイルのふるいやつ</param>
            /// <param name="x">表示位置(x)</param>
            /// <param name="y">表示位置(y)</param>
            /// <param name="nWidth">横幅</param>
            /// <param name="nHeight">縦幅</param>
            /// <param name="hWndParent">親ウインドウのハンドル</param>
            /// <param name="hMenu">メニューウインドウのハンドル</param>
            /// <param name="hInstance">ハンドルインスタンス</param>
            /// <param name="lpParam">パラメータ(基本Zero)</param>
            /// <returns>成功したらウインドウハンドル、失敗したら0を返す</returns>
            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr CreateWindowEx(
                Win32.WindowStylesEx dwExStyle,
                string lpClassName,
                string lpWindowName,
                Win32.WindowStyles dwStyle,
                int x, int y,
                int nWidth, int nHeight,
                IntPtr hWndParent,
                IntPtr hMenu,
                IntPtr hInstance,
                IntPtr lpParam);
            /// <summary>
            /// 指定したスタイルでウインドウを新たに作成します。
            /// </summary>
            /// <param name="dwExStyle">ウインドウスタイルのすごいやつ</param>
            /// <param name="lpClassName">ウインドウクラスアトム</param>
            /// <param name="lpWindowName">ウインドウ名(タイトル)</param>
            /// <param name="dwStyle">ウインドウスタイルのふるいやつ</param>
            /// <param name="x">表示位置(x)</param>
            /// <param name="y">表示位置(y)</param>
            /// <param name="nWidth">横幅</param>
            /// <param name="nHeight">縦幅</param>
            /// <param name="hWndParent">親ウインドウのハンドル</param>
            /// <param name="hMenu">メニューウインドウのハンドル</param>
            /// <param name="hInstance">ハンドルインスタンス</param>
            /// <param name="lpParam">パラメータ(基本Zero)</param>
            /// <returns>成功したらウインドウハンドル、失敗したら0を返す</returns>
            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr CreateWindowEx(
                Win32.WindowStylesEx dwExStyle,
                ushort lpAtom,
                string lpWindowName,
                Win32.WindowStyles dwStyle,
                int x, int y,
                int nWidth, int nHeight,
                IntPtr hWndParent,
                IntPtr hMenu,
                IntPtr hInstance,
                IntPtr lpParam);

            /// <summary>
            /// WindowProcedureを利用したら、おまじないのようにこいつをreturnしてください。
            /// ユーザー側で処理後に、デフォルトで処理されるはずだったWindowMessageの処理をします。
            /// </summary>
            /// <param name="hWnd">WndProcに使用したhWnd</param>
            /// <param name="uMsg">WndProcに使用したuMsg</param>
            /// <param name="wParam">WndProcに使用したwParam</param>
            /// <param name="lParam">WndProcに使用したlParam</param>
            /// <returns>WindowProcedureに対する戻り値</returns>
            [DllImport("user32.dll")]
            public static extern IntPtr DefWindowProc(IntPtr hWnd, Win32.WindowMessages uMsg, IntPtr wParam, IntPtr lParam);
        }
    }
}
