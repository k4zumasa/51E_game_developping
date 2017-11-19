using System;
using System.Collections.Generic;

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
}

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
    public int HpGettter()
    {
        return hp;
    }
    public int IntGettter()
    {
        return intelligence;
    }
    public string NameGettter()
    {
        return playername;
    }
    public Item ItemGetter(int i)
    {
        return itemlist[i];
    }
    public void ItemAdder(Item newitem)
    {
        itemlist.Add(newitem);
    }
    public void ItemRemover(Item lostitem)
    {
        itemlist.Remove(lostitem);
    }


}
public class BattlePlayer
{
    public Player player;
    //public BattleDirector director;
    public List<string> commandlist = new List<string> { "たたかう", "にげる", "ぼうぎょ", "どうぐ" };
    public BattlePlayer(Player player)//, BattleDirector director)
    {
        //this.director = director;
        this.player = player;
    }
}
