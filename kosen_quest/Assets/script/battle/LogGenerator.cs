using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogGenerator : MonoBehaviour
{
    public GameObject BattleDirector;
    public GameObject WindowPrefab3;
    public GameObject logPrefab;
    public GameObject Text;
    public GameObject wi;
    private Boolean OnBattle;
    public List<string> scenarios;
    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.08f;	// 1文字の表示にかかる時間

    private int currentLine = 0;
    private string currentText = string.Empty;  // 現在の文字列
    private float timeUntilDisplay = 0;     // 表示にかかる時間
    private float timeElapsed = 1;          // 文字列の表示を開始した時間
    private int lastUpdateCharacter = -1;		// 表示中の文字数
    private Boolean displaying = false;

    void Update()
    {
        //Debug.Log("LogGeneratorの存在確認");
        if (IsDisplaying())
        {
            //Debug.Log("表示中");
            // 文字の表示が完了してるなら次の行を表示する
            if (IsCompleteDisplayText)
            {
                if (currentLine < scenarios.Count)
                {
                    //System.Threading.Thread.Sleep(1000);
                    SetNextLine();
                }
                else
                {
                    ClearWindow();
                    return;

                }
            }
            else
            {
                // 完了してないなら文字をすべて表示する
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    timeUntilDisplay = 0;
                }
            }
            // クリックから経過した時間が想定表示時間の何%か確認し、表示文字数を出す
            int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
            if (displayCharacterCount != lastUpdateCharacter)
            {
                Text.GetComponent<Text>().text = currentText.Substring(0, displayCharacterCount);
                lastUpdateCharacter = displayCharacterCount;
            }
        }
    }
    public void Display(string log)
    {
        Debug.Log(log+"を追加するよ");
        if (IsDisplaying())
        {
            scenarios.Add(log);
        }
        else
        {
            MakeWindow(log);
        }
    }
    private void Start() {
        scenarios.Clear();
    }

    public void MakeWindow(string log)
    {
        if (log.Contains("\n"))
        {
            string[] logs = log.Split('\n');
            foreach(string str in logs)
            {
                scenarios.Add(str);
            }
        }
        else
        {
            scenarios.Add(log);
        }
        Debug.Log("テキスト画面作ったで！");
        GameObject canvas = GameObject.Find("Canvas");
        Text = Instantiate(logPrefab) as GameObject;
        Text.transform.SetParent(canvas.transform, false);
        Text.GetComponent<Text>().text = "";
        wi = Instantiate(WindowPrefab3) as GameObject;
        currentLine = 0;
        displaying = true;
    }
    public void ClearWindow()
    {
        //System.Threading.Thread.Sleep(1000);
        Destroy(Text);
        Text = null;
        Destroy(wi);
        scenarios.Clear();
        currentLine = 0;
        displaying = false;
    }
    void SetNextLine()
    {
        currentText = scenarios[currentLine];
        Debug.Log(currentText);
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        currentLine++;
        lastUpdateCharacter = -1;
    }
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay + 1; }
    }

    public Boolean IsDisplaying()
    {
        return displaying;
    }
}
