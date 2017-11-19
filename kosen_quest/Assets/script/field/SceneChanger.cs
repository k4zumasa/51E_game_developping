using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneChanger : MonoBehaviour {

    public string objname="none";
    public string type;
    public GameObject Player;
    public GameObject loggenerator;
    public Collision2D others;
    public Vector3 objpotision = new Vector3(20000, 20000, 0);
    public StateList statelist = new StateList();

    //オブジェクトの座標を取得
    public Vector3 GetPosition2D(GameObject objects) {
        
        Vector3 objpotision = objects.transform.position;
        objpotision.z = 0;

        return objpotision;
    }


    //衝突相手の座標を取得
    public Vector3 GetPosition2D(Collision2D objects) {
        Vector3 objpotision = objects.gameObject.transform.position;
        objpotision.z = 0;

        return objpotision;
    }

    //接触判定
    public bool TouchJudge2D(Vector3 object1, Vector3 object2) {
        bool touch = false;

        double x1 = object1.x;
        double y1 = object1.y;

        double x2 = object2.x;
        double y2 = object2.y;

        double judgex = System.Math.Abs(x1 - x2);
        double judgey = System.Math.Abs(y1 - y2);

        if(judgex<=1.2 || judgey <= 1.2) {
            touch = true;
        }

        return touch;


    }


    //衝突時に呼び出す
    public void OnCollisionEnter2D(Collision2D others) {
        //衝突したオブジェクト名の取得
        this.others = others;
        objname = others.gameObject.name;

        //座標取得
        this.objpotision = GetPosition2D(this.others);
        Vector3 playerpotision = GetPosition2D(Player);

        //このときHpを減らす
        statelist.Minus(1);
        this.loggenerator = GameObject.Find("LogGenerator");
        loggenerator.GetComponent<LogGenerator>().Display("たおたおたお");
       
    }






    private void Update() {

        //Aを押したときステースと場所表示
        if (Input.GetKeyUp(KeyCode.A)) {
            int hp = statelist.Hp();
            int attack = statelist.Attack();
            int intelligence = statelist.Intelligence();

            statelist.Show();
            
            Debug.Log("今ここ：   "+SceneManager.GetActiveScene().name);

        }

        //プレイヤーの位置取得
        Vector3 playerpotision = GetPosition2D(Player);

        //接触判定
        bool touch = TouchJudge2D(this.objpotision, playerpotision);

        
        




        if (touch) {
            
            //リスト内検索
            ObjectList objectlist = new ObjectList();
            type = objectlist.ObjectChecker(objname);

            

            //sceneならそのsceneへ
            if (type.Equals("scene")) {
                SceneManager.LoadScene(objname);
            }


            //衝突中にSpaceで呼び出すInput.GetKeyUp(KeyCode.Space)
            if (Input.GetKeyUp(KeyCode.Space)) {

                //相手のタイプによって処理変更
                switch (type) {
                    //enemyならBattleシーンへ
                    case "enemy":
                        Debug.Log(objname + "が現れた");
                        SceneManager.LoadScene("Battlescene");
                        break;

                    //objectなら説明を
                    case "object":
                        
                        Debug.Log("どうやらこれはytesy" + objname + "のようである");
                        break;

                    //それ以外では何もしない
                    case "etc":
                        Debug.Log("どうやらこれは何でもないようである");
                        break;
                }

                
            }




        }



    }
}

