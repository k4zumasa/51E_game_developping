using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDirector : MonoBehaviour {
    
    List<Item> itemlist;
    List<string> enemylist;

    // Use this for initialization
    void Start () {
        
	}
    public void SelectCommand(BattlePlayer player,List<string> enemylist)
    {
        this.enemylist = enemylist;
        this.itemlist = player.player.itemlist;
        
    }
    
}
