using UnityEngine;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;

public class TMXLoader : MonoBehaviour {

    // レイヤー格納
    public class Layer2D {
        public int width; // 幅
        public int height; // 高さ
        private int[] _vals = null; // マップデータ

        // 作成
        public void Create(int width, int height) {
            this.width = width;
            this.height = height;
            _vals = new int[width * height];
        }

        // 値の取得
        // @param x X座標
        // @param y Y座標
        // @return 指定の座標の値 (領域外を指定したら-1)
        public int Get(int x, int y) {
            if (x < 0 || x >= width) { return -1; }
            if (y < 0 || y >= height) { return -1; }
            return _vals[y * width + x];
        }

        // 値の設定
        // @param x X座標
        // @param y Y座標
        // @param val 設定する値
        public void Set(int x, int y, int val) {
            if (x < 0 || x >= width) { return; }
            if (y < 0 || y >= height) { return; }
            _vals[y * width + x] = val;
        }

        // デバッグ出力
        public void Dump() {
            print("[Layer2D] (w,h)=(" + width + "," + height + ")");
            for (int y = 0; y < height; y++) {
                string s = "";
                for (int x = 0; x < width; x++) {
                    s += Get(x, y) + ",";
                }
                print(s);
            }
        }
    }

    // レベルデータを読み込む
    void Start() {
        // レイヤー生成
        Layer2D layer = new Layer2D();
        // レベルデータ取得
        TextAsset tmx = Resources.Load("Resources/samplemap_2017_9_6") as TextAsset;

        // XML解析開始
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(tmx.text);
        XmlNodeList mapList = xmlDoc.GetElementsByTagName("map");
        foreach (XmlNode map in mapList) {
            XmlNodeList childList = map.ChildNodes;
            foreach (XmlNode child in childList) {
                if (child.Name != "layer") { continue; } // layerノード以外は見ない

                // マップ属性を取得
                XmlAttributeCollection attrs = child.Attributes;
                int w = int.Parse(attrs.GetNamedItem("width").Value); // 幅を取得
                int h = int.Parse(attrs.GetNamedItem("height").Value); // 高さを取得
                // レイヤー生成
                layer.Create(w, h);
                XmlNode node = child.FirstChild; // 子ノードは<data>のみ
                XmlNode n = node.FirstChild; // テキストノードを取得
                string val = n.Value; // テキストを取得
                // CSV(マップデータ)を解析
                int y = 0;
                foreach (string line in val.Split('\n')) {
                    int x = 0;
                    foreach (string s in line.Split(',')) {
                        int v = 0;
                        // ","で終わるのでチェックが必要
                        if (int.TryParse(s, out v) == false) { continue; }
                        // 値を設定
                        layer.Set(x, y, v);
                        x++;
                    }
                    y++;
                }
            }
        }

        // デバッグ出力
        layer.Dump();

    }
}