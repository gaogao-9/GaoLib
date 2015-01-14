# GaoLib
C#向けの、自分用便利なアレこれをまとめた奴のソースコード

### AIMP APIのサンプルコード
  C#
using System;
using System.IO;
using Aimp = GaoLib.Api.Aimp;

namespace Sample{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
          //再生
          Aimp::Remote.Play();
          //一時停止
          Aimp::Remote.PlayPause();
          //選択中の曲の長さ取得
          Console.WriteLine(Aimp::Remote.Duration);
          //音量を80%に指定
          Aimp::Remote.Volume = 80;
          //選択中の曲情報の取得
          Aimp::MusicInfo musicInfo = Aimp::Remote.MusicInfo;
          Console.WriteLine("曲名:{0}", musicInfo.Title);
          Console.WriteLine("歌手:{0}", musicInfo.Artist);
          Console.WriteLine("アルバム:{0}", musicInfo.Album);
          Console.WriteLine("ジャンル:{0}", musicInfo.Genre);
          Console.WriteLine("ビットレート:{0}", musicInfo.BitRate);
          Console.WriteLine("ファイルパス:{0}", musicInfo.FilePath);
          Console.WriteLine("ファイルサイズ:{0}", musicInfo.FileSize);
          Console.WriteLine("再生中かどうか:{0}", musicInfo.IsActive);
          //選択中の曲に埋め込まれたアルバムアートの取得(必ずpngで手に入る)
          byte[] art = Aimp::Remote.AlbumArt;
          using (var fs = new FileStream("art.png", FileMode.Create))
          {
              fs.Write(art, 0, art.Length);
          }
      }
      catch (Aimp::Exception.RemoteWindowNotFoundException)
      {
          Console.WriteLine("AIMPを起動してない時に発生するエラーの処理");
      }
      catch (Aimp::Exception.AimpException)
      {
          Console.WriteLine("何らかのエラーが発生した時の処理");
      }
    }
  }
}
