using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDirector : MonoBehaviour
{
    public GameObject CommandGenerator;
    public GameObject LogGenerator;
    string username = "Player";
    int hp = 100;
    int intelligence = 11;
    //List<string> itemlistname = new List<string> { "教科書", "過去問", "メガネ", "ゲーム" };
    //List<string> enemylistname = new List<string> { "物理の課題A", "物理の課題B", "実験レポート", "高電圧のプリント", "単語帳" };
    List<Item> itemlist = new List<Item> { new BuffItem("教科書", 10), new BuffItem("過去問", 20), new Item("メガネ"), new RecoveryItem("ゲーム", 0.3f) };
    List<Enemy> enemylist = new List<Enemy>();
    GameObject hptext;
    GameObject inttext;
    BattlePlayer player;
    Boolean OnBattle = false;
    Boolean OnSelecting = false;
    Boolean PlayerProcessing = false;
    Boolean EnemyProcessing = false;
    int i = 0;
    int Battlecount = 0;

    void Start()
    {
        this.player = new BattlePlayer(new Player(username, hp, intelligence, itemlist));
        this.LogGenerator = GameObject.Find("LogGenerator");
        this.CommandGenerator = GameObject.Find("CommandGenerator");
        this.inttext = GameObject.Find("かしこさ");
        this.hptext = GameObject.Find("HP");
        BattleStart(player);
    }
    public void DisplayCommand()
    {
        OnSelecting = true;
        CommandGenerator.GetComponent<CommandGenerator>().MakeWindow(player, enemylist);
        PlayerProcessing = true;
    }
    public void Display(string log)
    {
        LogGenerator.GetComponent<LogGenerator>().Display(log);
    }

    // Update is called once per frame
    void Update()
    {
        inttext.GetComponent<Text>().text = player.IntGetter().ToString();
        if (Battling())
        {
            if (player.IsSelected())
            {
                OnSelecting = false;
                if (IsDisplayingLog() == false)
                {
                    hptext.GetComponent<Text>().text = player.HpGetter().ToString();
                    
                    if (EnemyProcessing)
                    {
                        if (enemylist.Count > i)
                        {
                            enemylist[i].Attack(player);
                            Display( enemylist[i].ActionResult());
                            i++;
                            if (player.IsDied())
                            {
                                hptext.GetComponent<Text>().text = player.HpGetter().ToString();
                                Display(player.player.GetName() + "は負けてしまった");
                                BattleFinish();
                            }
                        }
                        else
                        {
                            i = 0;
                            EnemyProcessing = false;
                            player.NewTurn();
                        }
                    }
                    if (PlayerProcessing)
                    {
                        Display(player.ActionResult());
                        inttext.GetComponent<Text>().text = player.IntGetter().ToString();
                        if (player.IsEscaped())
                        {
                            BattleFinish();
                        }
                        else
                        {
                            for (int i = 0; i < enemylist.Count; i++)
                            {
                                if (enemylist[i].IsDied())
                                {
                                    enemylist.RemoveAt(i);
                                    if (enemylist.Count == 0)
                                    {
                                        Display("戦闘に勝利した！");
                                        Display(player.Win());
                                        BattleFinish();
                                        break;

                                    }
                                    i--;
                                }
                            }
                            PlayerProcessing = false;
                            EnemyProcessing = true;
                        }
                        
                    }
                }
            }
            else
            {
                if (IsDisplayingLog() == false)
                {
                    if (OnSelecting == false)
                    {
                        DisplayCommand();
                    }
                }
            }
        }
        else
        {
            if(IsDisplayingLog() == false)
            {
                if (player.IsDied())
                {
                    player.player = new Player(username, hp, intelligence, itemlist);
                    Battlecount = 0;
                }
                BattleStart(new BattlePlayer(player.player));
                Debug.Log("本体hp" + player.player.GetHp() + "int" + player.player.GetInt());
                Debug.Log("バトル用hp" + player.HpGetter() + "int" + player.IntGetter());
            }
        }

    }
    private void BattleFinish()
    {
        this.OnBattle = false; 
        OnSelecting = false;
        PlayerProcessing = false;
        EnemyProcessing = false;
        enemylist.Clear();
    }
    private void BattleStart(BattlePlayer player)
    {
        Battlecount++;
        this.player = player;
        this.OnBattle = true;/*foreach (string str in enemylistname)
        {
            enemylist.Add(new Enemy(
                (int)UnityEngine.Random.Range(player.HpGetter()*0.2f, player.HpGetter() * 0.6f),
                (int)UnityEngine.Random.Range(player.IntGetter() * 0.4f, player.IntGetter() * 0.8f),
                str
                ));
        }*/
        if (Battlecount > 6)
        {
            enemylist.Add(new Boss(10000, 200, "Type Boss", enemylist));
        }
        else
        {
            int enemynumber = BattleOnly.GetRandomIndex(0, 1, 2, 4, 4, 2);
            for (int i = 0; i < enemynumber; i++)
            {
                enemylist.Add(BattleOnly.MakeEnemy(enemylist, player));
            }
        }
        hptext.GetComponent<Text>().text = player.HpGetter().ToString();
        inttext.GetComponent<Text>().text = player.IntGetter().ToString();
        string enemys = "";
        for (int j = 0; j < enemylist.Count; j++)
        {
            if (j > 0)
            {
                enemys += ",";
            }
            enemys += enemylist[j].GetName(); ;
        }
        enemys += "が現れた。";
        Display(enemys);
    }
    public Boolean Battling()
    {
        if (OnBattle)
        {
            //Debug.Log("戦闘中だよ！");
        }
        else
        {
            Debug.Log("戦闘終了してるよ！");
        }
        return OnBattle;
    }
    public Boolean IsDisplayingLog()
    {
        if (LogGenerator.GetComponent<LogGenerator>().IsDisplaying())
        {
            //Debug.Log("ログの表示中だよ！");
        }
        else
        {
            //Debug.Log("コマンド選択中だよ！");
        }

        return LogGenerator.GetComponent<LogGenerator>().IsDisplaying();
    }
}
