using System;
using System.Collections.Generic;
using UnityEngine;

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
    public virtual string Use(BattlePlayer player) {
        return null;

    }
}
public class RecoveryItem :Item
{
    private string itemname;
    private int amount;
    public RecoveryItem (string itemname,int amount):base(itemname)
    {
        this.amount = amount;
    }
    public override string Use(BattlePlayer player)
    {
        player.Recovery(amount);
        return player.player.NameGettter() + "のHPが" + amount + "回復した。";
    }
}
public class BuffItem : Item
{
    private string itemname;
    private int amount;
    public BuffItem(string itemname,int amount) : base(itemname)
    {
        this.amount = amount;
    }
    public override string Use(BattlePlayer player)
    {
        player.Buff(amount);
        return player.player.NameGettter() + "はこの戦闘でかしこさが" + amount + "上がった。";
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
    public int HpGetter()
    {
        return hp;
    }
    public int IntGetter()
    {
        return intelligence;
    }
    public void HpSetter(int additionalhp)
    {
        this.hp = hp + additionalhp;
    }
    public void IntSetter(int additionalint)
    {
        this.intelligence = intelligence + additionalint;
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
public class BattleCaracter
{
    protected int hp;
    private string name;
    public virtual void Attack(BattleCaracter caracter)
    {
        
    }
    public virtual string Damege(int amount)
    {
        return null;
    }
    public virtual int HpGetter()
    {
        return hp;
    }
}
public class Enemy :BattleCaracter
{
    //private int hp;
    private int power;
    private string name;
    private string actionlog=null;
    public Enemy(int hp,int power,string name)
    {
        this.name = name;
        this.hp = hp;
        this.power = power;
    }
    public override void Attack(BattleCaracter player) 
    {
        actionlog=player.Damege(power);
    }
    public override string Damege(int intelligence)
    {
        string log= name + "に" + intelligence + "のダメージ。";
        this.hp -= intelligence;
        if (hp < 0) {
            hp = 0;
            log += name + "を倒した。";
        }
        return log;
    }
    public int PowerGetter()
    {
        return power;
    }
    public string NameGettter()
    {
        return name;
    }
    public string ActionResult()
    {
        return actionlog;
    }
}
public class BattlePlayer: BattleCaracter
{
    public Player player;
    int currenthp;
    int currentintelligence;
    private int totaldamage = 0;
    private int totalexe=0;
    private string actionlog; 
    private Boolean Selected=false;
    private Boolean Defenced = false;
    private Boolean Escaped = false;
    int currentturn = 0;
    public List<string> commandlist = new List<string> { "たたかう", "にげる", "ぼうぎょ", "どうぐ" };
    public BattlePlayer(Player player)
    {
        this.player = player;
        this.currenthp = player.HpGetter();
        this.currentintelligence = player.IntGetter();
    }
    
    public override void Attack( BattleCaracter enemycaracter)
    {
        Enemy enemy = (Enemy)enemycaracter;
        actionlog = enemy.Damege(currentintelligence);
        if (enemy.HpGetter() == 0)
        {
            totalexe += enemy.PowerGetter();
        }
        Select();
    }
    public void Defence()
    {
        actionlog = "このターン"+player.NameGettter() + "のダメージは半分になる！";
        Defenced = true;
        Select();
    }
    public void Escape()
    {
        Escaped = true;
        actionlog = player.NameGettter() + "は逃げ出した。";
        Select();
    }
    public void Item(Item item)
    {
        actionlog=item.Use(this);
        Select();
    }
    public void Recovery(int amount)
    {
        if (currenthp + amount > player.HpGetter())
        {
            currenthp = player.HpGetter();
        }
        else
        {
            this.currenthp += amount;
        }
    }
    public void Buff(int amount)
    {
        currentintelligence += amount;
    }
    public override int HpGetter()
    {
        return currenthp;
    }
    public int IntGetter()
    {
        return currentintelligence;
    }
    public override string Damege(int power)
    {
        if (IsDefenced())
        {
            power = power / 2;
        }
        string log = player.NameGettter() + "に" + power + "のダメージ。";
        this.currenthp -= power;
        if (currenthp < 0)
        {
            currenthp = 0;
            log += player.NameGettter() + "は倒れた。";
        }
        return log;
    }
    void Select()
    {
        Selected = true;
    }
    public Boolean IsSelected()
    {
        return Selected;
    }
    public Boolean IsDefenced()
    {
        return Defenced;
    }
    public Boolean IsEscaped()
    {
        return Escaped;
    }
    public void NewTurn()
    {
        Defenced = false;
        currentturn++;
    }
    public string ActionResult()
    {
        Selected = false;
        return actionlog;
    }
}
