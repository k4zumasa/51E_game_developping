using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDirector : MonoBehaviour {
    public GameObject CommandGenerator;
    public GameObject LogGenerator;
    string username = "テスト";
    int hp = 100;
    int intelligence = 11;
    //List<string> itemlistname = new List<string> { "教科書", "過去問", "メガネ", "ゲーム" };
    List<string> enemylistname = new List<string> { "物理の課題A", "物理の課題B", "実験レポート", "高電圧のプリント", "単語帳" };
    List<string> logs = new List<string>();
    List<Item> itemlist = new List<Item> { new BuffItem("教科書", 10), new BuffItem("過去問", 20), new Item("メガネ"), new RecoveryItem("ゲーム", 30) };
    List<Enemy> enemylist = new List<Enemy>();
    GameObject hptext;
    GameObject inttext;
    BattlePlayer player;
    Boolean IsNotDisplaying = true;
  

    // Use this for initialization
    void Start()
    {
        this.player = new BattlePlayer(new Player(username, hp, intelligence, itemlist));
        foreach (string str in enemylistname)
        {
            enemylist.Add(new Enemy(
                (int)UnityEngine.Random.Range(player.HpGetter()*0.2f, player.HpGetter() * 0.6f),
                (int)UnityEngine.Random.Range(player.IntGetter() * 0.4f, player.IntGetter() * 0.8f),
                str
                ));
        }
        this.hptext = GameObject.Find("HP");
        hptext.GetComponent<Text>().text = player.HpGetter().ToString();
        this.inttext = GameObject.Find("かしこさ");
        inttext.GetComponent<Text>().text = player.IntGetter().ToString();
        this.LogGenerator = GameObject.Find("LogGenerator");
        this.CommandGenerator = GameObject.Find("CommandGenerator");
        string enemys="";
        for(int j=0;j<enemylist.Count;j++)
        {
            if (j > 0)
            {
                enemys += ",";
            }
            enemys += enemylist[j].NameGettter(); ;
        }
        enemys += "が現れた。";
        LogGenerator.GetComponent<LogGenerator>().BattleStart();
        AddLog(enemys);
        DisplayLog();

    }
    public void DisplayCommand()
    {
        IsNotDisplaying = true;
        player.NewTurn();
        CommandGenerator.GetComponent<CommandGenerator>().MakeWindow(player, enemylist);
    }
    public void AddLog(string log)
    {
        logs.Add(log);
        
    }
    public void DisplayLog()
    {
        IsNotDisplaying =false;
        LogGenerator.GetComponent<LogGenerator>().MakeWindow(logs);
    }

    // Update is called once per frame
	void Update () {
        hptext.GetComponent<Text>().text = player.HpGetter().ToString();
        inttext.GetComponent<Text>().text = player.IntGetter().ToString();
        if (IsNotDisplaying)
        {
            
            
        }
        if (player.IsSelected())
        {
            AddLog(player.ActionResult());
            if (player.IsEscaped())
            {
                LogGenerator.GetComponent<LogGenerator>().BattleFinish();
            }
            else
            {
                foreach (Enemy enemy in enemylist)
                {
                    AddLog(enemy.NameGettter() + "の攻撃");
                    enemy.Attack(player);
                    AddLog(enemy.ActionResult());
                }if (player.HpGetter() == 0)
                {
                LogGenerator.GetComponent<LogGenerator>().BattleFinish();
                AddLog(player.player.NameGettter() + "は負けてしまった");
                }
            }
            for (int i = 0; i < enemylist.Count; i++)
            {
                if (enemylist[i].HpGetter() == 0)
                {
                    enemylist.RemoveAt(i);
                    if (enemylist.Count == 0)
                    {
                        AddLog("戦闘に勝利した！");
                        LogGenerator.GetComponent<LogGenerator>().BattleFinish();
                        break;

                    }
                    i--;
                }
            }

            DisplayLog();

        }
        
	}
}
