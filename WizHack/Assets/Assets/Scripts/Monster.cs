using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Monster : MonoBehaviour
{
        //GET PLAYER HERE
        public char icon;
        public string monName;
        public int hp;
        public int dmg;
        public int def;
        public int lootTable;
        public Vector3 location;
        public GameObject controller;

    void Start()
    {
        var data = GetComponentInParent<controllerScript>().getMonster();
        this.icon = ((string)data[0])[0];
        this.monName = (string)data[1];
        this.hp = System.Convert.ToInt32(data[2]);
        this.dmg = System.Convert.ToInt32(data[3]);
        this.def = System.Convert.ToInt32(data[4]);
        this.lootTable = System.Convert.ToInt32(data[5]);
        this.location = GetComponentInParent<controllerScript>().getLocation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void move()
    {
        //GET DIST FROM PLAYER
        //MOVE TOWARDS IF WITHIN CERTAIN DIST
    }
}
