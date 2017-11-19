using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandGenerator2 : MonoBehaviour
{
    public GameObject BattleDirector;
    public GameObject CommandGenerator;
    public GameObject ArrowPrefab;
    public GameObject CommandPrefab;
    public GameObject WindowPrefab;
    public GameObject ar;
    public GameObject it;
    public GameObject wi;
    List<GameObject> itlist;
    List<Item> itemlist;
    int i;
    BattlePlayer player;
    Boolean selected = false;

    private void Start()
    {
        itlist = new List<GameObject>();
        this.CommandGenerator = GameObject.Find("CommandGenerator");
        this.BattleDirector = GameObject.Find("BattleDirector");
        i = 0;
    }
    private void Update()
    {
        if (ar != null)
        {
            if (selected) { ar.transform.Translate(0.1f, 0, 0); }
            if (ar.transform.position.x > -4.4f)
            {
                Debug.Log(player.player.NameGettter());
                BattleDirector.GetComponent<BattleDirector>().AddLog(player.player.NameGettter() + "は"+itemlist[i].NameGettter()+"を使った。");
                Debug.Log(itemlist[i]);
                player.Item(itemlist[i]);
                CommandGenerator.GetComponent<CommandGenerator>().ClearWindow();
                ClearWindow();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
             {
                ar.GetComponent<CommandController>().Left();
                ClearWindow();
            }
            if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Return))
            {
                selected = true;
                //ar.GetComponent<CommandController>().Right();

            }
            
           if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (i == 0)
                    {
                        ar.GetComponent<CommandController>().Last(itemlist.Count - 1);
                        i = itemlist.Count - 1;
                    }
                    else
                    {
                        ar.GetComponent<CommandController>().Up();
                        i -= 1;
                    }
                }
            if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (i == (itemlist.Count - 1))
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
    public void MakeWindow(BattlePlayer player)
    {
        this.itemlist = player.player.itemlist;
        this.player = player;
        GameObject canvas = GameObject.Find("Canvas");
        ar = Instantiate(ArrowPrefab) as GameObject;
        ar.transform.localPosition = new Vector3(-4.7f, -0.6f, 0);
        //ar.transform.localPosition = new Vector3(-5f, -0.6f, 0);
        wi = Instantiate(WindowPrefab) as GameObject;
        wi.transform.localPosition = new Vector3(-3.33f, -2.237f, 0);
        for (int j = 0; j < itemlist.Count; j++)
        {
            it = Instantiate(CommandPrefab) as GameObject;
            it.transform.SetParent(canvas.transform, false);
            it.transform.localPosition = new Vector3(-650, -140 - (240 * j), 0);
            it.GetComponent<Text>().text = itemlist[j].NameGettter();
            itlist.Add(it);
        }
        i = 0;
        selected = false;

    }
    public void ClearWindow()
    {
        Destroy(ar);
        ar = null;
        Destroy(wi);
        for (int j = 0; j < itlist.Count; j++)
        {
            //Debug.Log(colist[j].GetComponent<Text>().text + "を削除するよ！");
            Destroy(itlist[j]);
        }
       itlist.Clear();
        i = 0;
    }
}
