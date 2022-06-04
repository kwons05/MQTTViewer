using System.IO;
using System.Xml.Serialization;

namespace UMApp.Utility
{
    #region XML Serialize
    public static class XmlSerialize
    {

        /// <summary>
        /// シリアル化処理（ILastModifyTime インタフェースを実装した場合は日時を記録）
        /// </summary>
        /// <typeparam name="T">クラス型</typeparam>
        /// <param name="file">ファイル名</param>
        /// <param name="t">クラスインスタンス</param>
        /// <param name="writeTime">日時を記録の有効・無効</param>
        public static void Save<T>(string file, T t) where T : class
        {
            using (var stream = File.CreateText(file)) SaveStream<T>(stream, t);
        }

        /// <summary>
        /// シリアル化処理（ILoadedFileInfo インタフェースを実装した場合はファイル情報を記録）
        /// </summary>
        /// <typeparam name="T">クラス型</typeparam>
        /// <param name="stream">ストリーム</param>
        /// <param name="t">オブジェクト</param>
        /// <param name="writeTime">日時を記録の有効・無効</param>
        public static void SaveStream<T>(StreamWriter stream, T t) where T : class
        {
            new XmlSerializer(typeof(T)).Serialize(stream, t);
        }

        /// <summary>
        /// デシリアル化処理（ファイル指定）
        /// </summary>
        /// <typeparam name="T">クラス型</typeparam>
        /// <param name="file">ファイル名</param>
        /// <returns>クラスインスタンス</returns>
        public static T Load<T>(string file) where T : class
        {
            using (var s = File.OpenText(file)) return LoadStream<T>(s, file);
        }

        /// <summary>
        /// デシリアル化処理（ILoadedFileInfo インタフェースを実装した場合はファイル情報を記録）
        /// </summary>
        /// <typeparam name="T">クラス型</typeparam>
        /// <param name="stream">ストリーム</param>
        /// <param name="file">ファイル名</param>
        /// <returns>クラスインスタンス</returns>
        public static T LoadStream<T>(StreamReader stream, string file) where T : class
        {
            return (new XmlSerializer(typeof(T))).Deserialize(stream) as T;
        }

    }
    #endregion
}
