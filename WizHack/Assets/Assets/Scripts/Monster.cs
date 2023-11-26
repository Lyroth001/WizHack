using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.Intrinsics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Monster : MonoBehaviour
{
        //GET PLAYER HERE
        public char icon;
        public string monName;
        public int hp;
        public int dmg;
        public int def;
        public int lootTable;
        public Vector3Int location;
        public GameObject controller;
        private Cavegenerator cavegenerator;
        private Tilemap tilemap;

        public void Init(Cavegenerator cavegenerator, Vector3Int location, Tilemap tilemap)
        {
            this.cavegenerator = cavegenerator;
            this.location = location;
            this.tilemap = tilemap;
            
            UpdatePosition(location);
        }
    void Start()
    {
        var data = GetComponentInParent<controllerScript>().getMonster();
        this.icon = ((string)data[0])[0];
        this.monName = (string)data[1];
        this.hp = System.Convert.ToInt32(data[2]);
        this.dmg = System.Convert.ToInt32(data[3]);
        this.def = System.Convert.ToInt32(data[4]);
        this.lootTable = System.Convert.ToInt32(data[5]);
        
        UpdatePosition(location);
    }
    

    // Update is called once per frame
    public void UpdatePosition(Vector3Int pos)
    {
        cavegenerator.getTileArray()[location.x,location.y].setMonster(null);
        cavegenerator.getTileArray()[pos.x,pos.y].setMonster(this);
        this.transform.position = tilemap.GetCellCenterWorld(pos);
    }

    void move()
    {
        //GET DIST FROM PLAYER
        //MOVE TOWARDS IF WITHIN CERTAIN DIST
    }

    void checkHealth()
    {
        if (hp <= 0)
        {
            //die
            cavegenerator.getTileArray()[location.x,location.y].setMonster(null);
            Destroy(gameObject);
        }
    }

    public void damage(int dmg)
    {
        dmg -= def;
        if (dmg <= 0)
        {
            dmg = 1;
        }
        hp -= dmg;
        checkHealth();
    }
}
