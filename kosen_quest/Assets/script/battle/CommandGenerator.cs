using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandGenerator : MonoBehaviour
{
    public GameObject BattleDirector;
    public GameObject CommandGenerator2;
    public GameObject EnemySelector;
    public GameObject LogGenerator;

    public GameObject ArrowPrefab;
    public GameObject CommandPrefab;
    public GameObject WindowPrefab;
    public GameObject WindowPrefab2;
    public GameObject NamePrefab;
    public GameObject ar;
    public GameObject co;
    public GameObject wi;
    public GameObject wi2;
    public GameObject en;
    List<string> commandlist;
    List<Enemy> enemylist;
    List<GameObject> commandtextlist;
    List<GameObject> enemytextlist;
    BattlePlayer player;
    int i;

    public System.Boolean flag;
    private void Start()
    {
        this.CommandGenerator2 = GameObject.Find("CommandGenerator2");
        this.BattleDirector = GameObject.Find("BattleDirector");
        this.EnemySelector = GameObject.Find("EnemySelector");
        this.LogGenerator = GameObject.Find("LogGenerator");
        flag = false;
        i = 0;
    }
    private void Update()
    {
        if (BattleDirector.GetComponent<BattleDirector>().IsDisplayingLog()==false)
        {

            if (flag)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    ar.GetComponent<CommandController>().Left();
                    flag = false;
                }
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.Return))
                {
                    ar.GetComponent<CommandController>().Right();
                    flag = true;
                    Action(commandlist[i]);
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (i == 0)
                    {
                        ar.GetComponent<CommandController>().Last(commandlist.Count - 1);
                        i = commandlist.Count - 1;
                    }
                    else
                    {
                        ar.GetComponent<CommandController>().Up();
                        i -= 1;
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (i == (commandlist.Count - 1))
                    {
                        ar.GetComponent<CommandController>().First();
                        i = 0;
                    }
                    else
                    {
                        ar.GetComponent<CommandController>().Down();
                        i += 1;
                    }
                }
            }
        }


    }
    public void Action(string command)
    {
        switch (command)
        {
            case "たたかう":
                EnemySelect();
                break;
            case "にげる":
                BattleDirector.GetComponent<BattleDirector>().Display(player.player.GetName() + "は逃げ出そうとした。");
                player.Escape();
                ClearWindow();
                break;
            case "ぼうぎょ":
                BattleDirector.GetComponent<BattleDirector>().Display(player.player.GetName() + "は防御を固めた。");
                player.Defence();
                ClearWindow();
                break;
            case "どうぐ":
                ItemSelect();
                break;
            default:
                Debug.Log("選択ミス");
                break;

        }
    }
    public void EnemySelect()
    {
        EnemySelector.GetComponent<EnemySelector>().MakeArrow(enemylist, player);
    }
    public void ItemSelect()
    {
        CommandGenerator2.GetComponent<CommandGenerator2>().MakeWindow(player);
    }
    public void MakeWindow(BattlePlayer player, List<Enemy> enemylist)
    {
        this.player = player;
        enemytextlist = new List<GameObject>();
        commandtextlist = new List<GameObject>();
        this.enemylist = enemylist;
        this.commandlist = player.commandlist;
        GameObject canvas = GameObject.Find("Canvas");
        ar = Instantiate(ArrowPrefab) as GameObject;
        wi = Instantiate(WindowPrefab) as GameObject;
        wi2 = Instantiate(WindowPrefab2) as GameObject;

        for (int j = 0; j < commandlist.Count; j++)
        {
            co = Instantiate(CommandPrefab) as GameObject;
            co.transform.SetParent(canvas.transform, false);
            co.transform.localPosition = new Vector3(-1350, -140 - (240 * j), 0);
            co.GetComponent<Text>().text = commandlist[j];
            commandtextlist.Add(co);
        }
        for (int k = 0; k < this.enemylist.Count; k++)
        {
            en = Instantiate(NamePrefab) as GameObject;
            en.transform.SetParent(canvas.transform, false);
            en.transform.localPosition = new Vector3(500, -100 - (200 * k), 0);
            en.GetComponent<Text>().text = this.enemylist[k].GetName();
            Debug.Log(this.enemylist[k].GetName());
            enemytextlist.Add(en);
        }
        i = 0;
        flag = false;

    }
    public void ClearWindow()
    {
        Destroy(ar);
        ar = null;
        Destroy(wi);
        Destroy(wi2);
        for (int j = 0; j < commandtextlist.Count; j++)
        {
            //Debug.Log(colist[j].GetComponent<Text>().text + "を削除するよ！");
            Destroy(commandtextlist[j]);
        }
        commandtextlist.Clear();
        for (int j = 0; j < enemytextlist.Count; j++)
        {
            //Debug.Log(colist[j].GetComponent<Text>().text + "を削除するよ！");
            Destroy(enemytextlist[j]);
        }
        enemytextlist.Clear();
        i = 0;
    }

}