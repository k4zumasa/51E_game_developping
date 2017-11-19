using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BattleOnly {
    public static Enemy MakeEnemy(List<Enemy> enemylist, BattlePlayer player) {
        int EnemyType = BattleOnly.GetRandomIndex(8, 1, 1, 1);
        Enemy enemy = null;
        switch (EnemyType) {
            case 0:
                enemy = new Enemy(
                (int)UnityEngine.Random.Range(player.IntGetter() * 4.0f, player.IntGetter() * 6.0f),
                (int)UnityEngine.Random.Range(player.IntGetter() * 0.4f, player.IntGetter() * 0.8f),
                "UNKNOWN TYPE M"
                );
                break;
            case 1:
                enemy = new Professor(
                    (int)UnityEngine.Random.Range(player.IntGetter() * 4.0f, player.IntGetter() * 7.0f),
                    (int)UnityEngine.Random.Range(player.IntGetter() * 0.2f, player.IntGetter() * 0.5f),
                    "UNKNOWN TYPE P",
                    (int)UnityEngine.Random.Range(player.IntGetter() * 4.0f, player.IntGetter() * 6.0f),
                    enemylist
                );
                break;
            case 2:
                enemy = new Wizard(
                   (int)UnityEngine.Random.Range(player.IntGetter() * 4.0f, player.IntGetter() * 7.0f),
                   (int)UnityEngine.Random.Range(player.IntGetter() * 0.2f, player.IntGetter() * 0.5f),
                   "UNKNOWN TYPE W",
                   (int)UnityEngine.Random.Range(player.IntGetter() * 5.0f, player.IntGetter() * 7.0f)
               );
                break;
            case 3:
                enemy = new Supporter(
                    (int)UnityEngine.Random.Range(player.IntGetter() * 3.0f, player.IntGetter() * 5.0f),
                    (int)UnityEngine.Random.Range(player.IntGetter() * 0.2f, player.IntGetter() * 0.5f),
                    "UNKNOWN TYPE S",
                    (int)UnityEngine.Random.Range(player.IntGetter() * 4.0f, player.IntGetter() * 6.0f),
                    enemylist
                );
                break;
        }
        string name = enemy.GetName();
        int i = 0;
        int j = 2;
        foreach (Enemy previousEnemy in enemylist) {
            if (previousEnemy.GetName().StartsWith(name)) {
                if (previousEnemy.GetName().Equals(name)) {
                    enemylist[i].SetName(name + "その1");
                    enemy.SetName(name + "その2");
                }
                else {
                    enemy.SetName(name + "その" + j);
                    j++;
                }
            }
            i++;
        }
        return enemy;
    }
    public static int GetRandomIndex(params int[] weightTable) {
        var totalWeight = weightTable.Sum();
        var value = UnityEngine.Random.Range(1, totalWeight + 1);
        var retIndex = -1;
        for (var i = 0; i < weightTable.Length; ++i) {
            if (weightTable[i] >= value) {
                retIndex = i;
                break;
            }
            value -= weightTable[i];
        }
        return retIndex;
    }
}
public class BattleCaracter {
    protected int hp;
    protected string name;
    protected Boolean Defenced = false;
    protected string actionlog = null;
    protected Boolean Died = false;
    protected int currentturn = 1;
    public virtual void Attack(BattleCaracter caracter) {

    }
    public virtual string Damege(int amount) {
        return null;
    }
    public virtual void Defence() {
        actionlog = "このターン" + name + "のダメージは半分になる！";
        Defenced = true;
    }
    public virtual int HpGetter() {
        return hp;
    }
    public Boolean IsDefenced() {
        return Defenced;
    }
    public string ActionResult() {
        return actionlog;
    }
    public string GetName() {
        return name;
    }
    public Boolean IsDied() {
        return Died;
    }

}
public class Enemy : BattleCaracter {
    protected int power;
    public Enemy(int hp, int power, string name) {
        this.name = name;
        this.hp = hp;
        this.power = power;
    }
    public override void Attack(BattleCaracter caracter) {
        Defenced = false;
        BattlePlayer player = (BattlePlayer)caracter;
        switch (BattleOnly.GetRandomIndex(8, 1)) {
            case 0:
                actionlog = name + "のこうげき\n" + player.Damege(power);
                break;
            case 1:
                Defence();
                actionlog = name + "は防御を固めた！\n" + actionlog;
                break;
        }

        currentturn++;
    }
    public override string Damege(int intelligence) {
        if (IsDefenced()) {
            intelligence = intelligence / 2;
        }
        string log = name + "に" + intelligence + "のダメージ。";
        this.hp -= intelligence;
        if (hp < 0) {
            hp = 0;
            Died = true;
            log += name + "を倒した。";

        }
        return log;
    }
    public int PowerGetter() {
        return power;
    }
    public string Recovery(int amount) {
        this.hp += amount;
        return name + "のHPが" + amount + "増加した";
    }
    public string PowerUp(int amount) {
        this.power += amount;
        return name + "がいっそう手ごわくなった気がする！";
    }
    public void SetName(string name) {
        this.name = name;
    }
}
public class Professor : Enemy {
    private int mp;
    private List<Enemy> enemylist;
    public Professor(int hp, int power, string name, int mp, List<Enemy> enemylist) : base(hp, power, name) {
        this.mp = mp;
        this.enemylist = enemylist;
    }
    public override void Attack(BattleCaracter caracter) {
        Defenced = false;
        BattlePlayer player = (BattlePlayer)caracter;
        switch (BattleOnly.GetRandomIndex(4, 4, 2, 1)) {
            case 0:
                Enemy enemy = enemylist[(int)UnityEngine.Random.Range(0, enemylist.Count - 1)];
                if (mp > 10) {
                    actionlog = name + "は" + enemy.GetName() + "の問題を追加した\n" + enemy.Recovery(power * 4);
                    mp -= 10;
                }
                else {
                    actionlog = name + "は" + enemy.GetName() + "を回復させようとした！\nしかしMPが足りない！";
                }

                break;
            case 1:
                actionlog = name + "は杖でたたいてきた！\n" + player.Damege(power);
                break;
            case 2:
                Defence();
                actionlog = name + "は防御を固めた！" + actionlog;
                break;
            case 3:
                actionlog = name + "はザオリクをとなえた！\nしかしMPがたりない！";
                break;
        }
        currentturn++;
    }
}
public class Wizard : Enemy {
    private int mp;
    public Wizard(int hp, int power, string name, int mp) : base(hp, power, name) {
        this.mp = mp;
    }
    public override void Attack(BattleCaracter caracter) {
        Defenced = false;
        BattlePlayer player = (BattlePlayer)caracter;
        switch (BattleOnly.GetRandomIndex(5, 3, 2, 1)) {
            case 0:
                if (mp > 10) {
                    actionlog = name + "はめらぞーまをとなえた\n" + player.Damege(power * 3);
                    mp -= 10;
                }
                else {
                    actionlog = name + "はめらぞーまをとなえた\nしかしMPが足りない！";
                }
                break;
            case 1:
                actionlog = name + "は杖でたたいてきた！\n" + player.Damege(power);
                break;
            case 2:
                Defence();
                actionlog = name + "は防御を固めた！" + actionlog;
                break;
            case 3:
                actionlog = name + "はイオナズンをとなえた！\nしかしMPがたりない！";
                break;
        }
        currentturn++;
    }
}
public class Supporter : Enemy {

    private int mp;
    private List<Enemy> enemylist;
    public Supporter(int hp, int power, string name, int mp, List<Enemy> enemylist) : base(hp, power, name) {
        this.mp = mp;
        this.enemylist = enemylist;
    }
    public override void Attack(BattleCaracter caracter) {
        Defenced = false;
        BattlePlayer player = (BattlePlayer)caracter;
        switch (BattleOnly.GetRandomIndex(3, 2, 2, 2)) {
            case 0:
                actionlog = name + "は杖でたたいてきた！\n" + player.Damege(power);
                break;
            case 1:
                Defence();
                actionlog = name + "は防御を固めた！" + actionlog;
                break;
            case 2:
                if (mp > 10) {
                    actionlog = name + "は天地魔闘の構えをとった\n" + player.Foolish();
                    mp -= 10;
                }
                else {
                    actionlog = name + "は天地魔闘の構えをとった\nしかし何も起こらなかった！";
                }
                break;
            case 3:
                Enemy enemy = enemylist[(int)UnityEngine.Random.Range(0, enemylist.Count - 1)];
                if (mp > 10) {
                    actionlog = name + "は" + enemy.GetName() + "の問題を強化した\n" + enemy.PowerUp(power);
                    mp -= 10;
                }
                else {
                    actionlog = name + "は" + enemy.GetName() + "を強化させようとした！\nしかしMPが足りない！";
                }

                break;
        }
    }
}
public class Boss : Enemy {
    private List<Enemy> enemylist;
    public Boss(int hp, int power, string name, List<Enemy> enemylist) : base(hp, power, name) {
        this.enemylist = enemylist;
    }
    public override void Attack(BattleCaracter caracter) {
        Defenced = false;
        BattlePlayer player = (BattlePlayer)caracter;
        switch (BattleOnly.GetRandomIndex(5, 2, 1, 4, 2, 2, 1)) {
            case 0:
                actionlog = name + "の強烈な一撃！\n" + player.Damege((int)UnityEngine.Random.Range(power * 0.8f, power * 1.2f));
                break;
            case 1:
                Defence();
                actionlog = name + "は防御を固めた！" + actionlog;
                break;
            case 2:
                actionlog = name + "は天地魔闘の構えをとった\n" + player.Foolish();
                break;
            case 3:
                actionlog = name + "はめらぞーまをとなえた\n" + player.Damege(power * 3);
                break;
            case 4:
                actionlog = name + "は自分自身を強化した\n" + this.PowerUp(power);
                break;
            case 5:
                actionlog = name + "は自分を回復させた\n" + this.Recovery(power * 4);
                break;
            case 6:
                actionlog = name + "は仲間を呼んだ\n" + CreateEnemy(player);
                break;
        }
    }
    public string CreateEnemy(BattlePlayer player) {
        Enemy enemy = BattleOnly.MakeEnemy(enemylist, player);
        enemylist.Add(enemy);
        return enemy.GetName() + "があらわれた！";

    }
}
public class BattlePlayer : BattleCaracter {
    public Player player;

    private int currentintelligence = 0;
    private int totaldamage = 0;

    private int totalexe = 0;
    private Boolean Selected = false;
    private Boolean Escaped = false;
    private Boolean Winner = false;
    public List<string> commandlist = new List<string> { "たたかう", "にげる", "ぼうぎょ", "どうぐ" };
    public BattlePlayer(Player player) {
        this.player = player;
        this.hp = player.GetHp();
        this.currentintelligence = player.GetInt();
    }

    public override void Attack(BattleCaracter enemycaracter) {
        Enemy enemy = (Enemy)enemycaracter;
        actionlog = enemy.Damege(currentintelligence);
        if (enemy.HpGetter() == 0) {
            totalexe += enemy.PowerGetter();
        }
        Select();
    }
    public override void Defence() {
        actionlog = "このターン" + player.GetName() + "のダメージは半分になる！";
        Defenced = true;
        Select();
    }
    public void Escape() {
        Escaped = true;
        actionlog = player.GetName() + "は逃げ出した。";
        Select();
    }
    public void Item(Item item) {
        actionlog = item.Use(this);
        Select();
    }
    public void Recovery(int amount) {
        if (hp + amount > player.GetHp()) {
            hp = player.GetHp();
        }
        else {
            this.hp += amount;
        }
    }
    public string Foolish() {
        int amount = (int)player.GetInt() / 10;
        this.currentintelligence -= amount;
        return player.GetName() + "のかしこさが" + amount + "下がった！";
    }
    public void Buff(int amount) {
        currentintelligence += amount;
    }
    public override int HpGetter() {
        return hp;
    }
    public int IntGetter() {
        return currentintelligence;
    }
    public override string Damege(int power) {
        if (IsDefenced()) {
            power = power / 2;
        }
        string log = player.GetName() + "に" + power + "のダメージ。";
        this.totaldamage += power;
        this.hp -= power;
        if (hp < 0) {
            hp = 0;
            Die();
            log += player.GetName() + "は倒れた。";
        }
        return log;
    }
    private void Die() {
        Died = true;
    }
    public string Win() {
        Winner = true;
        this.player.SetHp(totaldamage);
        this.player.SetInt(totalexe);
        return player.GetName() + "はHPが" + totaldamage + "、かしこさが" + totalexe + "上がった！";
    }
    void Select() {
        Selected = true;
    }
    public Boolean IsSelected() {
        return Selected;
    }
    public Boolean IsWin() {
        return Winner;
    }

    public Boolean IsEscaped() {
        return Escaped;
    }
    public void NewTurn() {
        Defenced = false;
        Selected = false;
        currentturn++;
    }
}