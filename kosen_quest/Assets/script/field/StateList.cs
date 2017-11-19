using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateList{


    public static int hp=30;
    public static int attack=20;
    public static int intelligence=20;


    public int Hp() {
        return hp;
    }

    public int Attack() {
        return attack;
    }

    public int Intelligence() {
        return intelligence;
    }

    public void  Plus(int delta) {
        hp = hp + delta;
    }

    public void  Minus(int delta) {
        hp = hp - delta;
        
    }

    public void Show() {
        Debug.Log("hp =" + hp + "  attack =" + attack + "  interigence==" + intelligence);
    }


}

