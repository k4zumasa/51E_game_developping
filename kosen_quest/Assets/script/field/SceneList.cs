using System.Collections;
using System.Collections.Generic;


public class ObjectList {
    public List<string> scenes = new List<string>();
    public List<string> enemys = new List<string>();
    public List<string> buildings = new List<string>();

    //追加
    public ObjectList() {
        //Sceneの追加
        this.scenes.Add("In_Tent");
        this.scenes.Add("School"); 

        //ここからenemys
        this.enemys.Add("Dragon");

        //ここからobjects
        this.buildings.Add("Tent");
        this.buildings.Add("Tree");
    }

    //リスト検索
    public string ObjectChecker(string objectname) {
        string type="etc";
        bool exist_scenes=scenes.Contains(objectname);;
        bool exist_enemys = enemys.Contains(objectname);
        bool exist_buildings = buildings.Contains(objectname);


        if (exist_enemys) {
            type = "enemy";
        }

        if (exist_buildings) {
            type = "object";
        }

        if (exist_scenes) {
            type = "scene";
        }


        //タイプを返却
        return type;
    }

}