using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelector : MonoBehaviour
{

    public GameObject BattleDirector;
    public GameObject CommandGenerator;
    public GameObject ArrowPrefab;
    public GameObject ar = null;
    List<Enemy> enemylist;
    int i;
    Boolean selected = false;
    BattlePlayer player;
    // Use this for initialization
    void Start()
    {
        i = 0;
        this.CommandGenerator = GameObject.Find("CommandGenerator");
        this.BattleDirector = GameObject.Find("BattleDirector");
    }

    // Update is called once per frame
    void Update()
    {
        if (ar != null)
        {
            if (selected) { ar.transform.Translate(0.1f, 0, 0); }
            if (ar.transform.position.x > -1.4f)
            {
                BattleDirector.GetComponent<BattleDirector>().Display(player.player.GetName() + "は" + enemylist[i].GetName() + "に攻撃した。");
                player.Attack(enemylist[i]);
                Debug.Log(enemylist[i].GetName());
                CommandGenerator.GetComponent<CommandGenerator>().ClearWindow();
                ClearArrow();

            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ClearArrow();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Return))
            {
                selected = true;
                //ar.GetComponent<CommandController>().Right();

            }


            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (i == 0)
                {
                    ar.GetComponent<CommandController>().Last2(enemylist.Count - 1);
                    i = enemylist.Count - 1;
                }
                else
                {
                    ar.GetComponent<CommandController>().Up2();
                    i -= 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (i == (enemylist.Count - 1))
                {
                    ar.GetComponent<CommandController>().First2();
                    i = 0;
                }
                else
                {
                    ar.GetComponent<CommandController>().Down2();
                    i += 1;
                }
            }

        }
    }
    public void MakeArrow(List<Enemy> list, BattlePlayer player)
    {
        Debug.Log(list.ToString());
        this.player = player;
        this.enemylist = list;
        Debug.Log("arrowを生成");
        ar = Instantiate(ArrowPrefab) as GameObject;
        ar.transform.localPosition = new Vector3(-1.7f, -0.4f, 0);
        //ar.transform.localPosition = new Vector3(-2.0f, -0.4f, 0);
        i = 0;
        selected = false;

    }
    public void ClearArrow()
    {
        Destroy(ar);
        ar = null;
        i = 0;
    }
}
