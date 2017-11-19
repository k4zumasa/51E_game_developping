using System;
using System.Collections.Generic;
using UnityEngine;



public class Player
{



    
    private int hp;
    private int intelligence;
    private string playername;
    public List<Item> itemlist;
    public Player(string playername, int hp, int intelligence)
    
    {
        this.playername = playername;
        this.hp = hp;
        this.intelligence = intelligence;
        this.itemlist = new List<Item>();
    }

    public Player(string playername, int hp, int intelligence, List<Item> itemlist)
    {
        this.playername = playername;
        this.hp = hp;
        this.intelligence = intelligence;
        this.itemlist = itemlist;
    }
    public int GetHp()
    {
        return hp;
    }
    public int GetInt()
    {
        return intelligence;
    }
    public void SetHp(int additionalhp)
    {
        this.hp = hp + additionalhp;
    }
    public void SetInt(int additionalint)
    {
        this.intelligence = intelligence + additionalint;
    }
    public string GetName()
    {
        return playername;
    }
    public Item GetItem(int i)
    {
        return itemlist[i];
    }
    public void AddItem(Item newitem)
    {
        itemlist.Add(newitem);
    }
    public void RemoveItem(Item lostitem)
    {
        itemlist.Remove(lostitem);
    }


}
public class Item
{
    private string itemname;
    public Item(string itemname)
    {
        this.itemname = itemname;
    }
    public string NameGettter()
    {
        return itemname;
    }
    public virtual string Use(BattlePlayer player)
    {
        return "しかし何も起こらなかった！";

    }
}
public class RecoveryItem : Item
{
    private string itemname;
    private float amount;
    public RecoveryItem(string itemname, float amount) : base(itemname)
    {
        this.amount = amount;
    }
    public override string Use(BattlePlayer player)
    {
        player.Recovery((int)(player.player.GetHp()*amount));
        return player.player.GetName() + "のHPが" + (int)(player.player.GetHp()) * amount + "回復した。";
    }
}
public class BuffItem : Item
{
    private string itemname;
    private int amount;
    public BuffItem(string itemname, int amount) : base(itemname)
    {
        this.amount = amount;
    }
    public override string Use(BattlePlayer player)
    {
        player.Buff(amount);
        return player.player.GetName() + "はこの戦闘でかしこさが" + amount + "上がった。";
    }
}
