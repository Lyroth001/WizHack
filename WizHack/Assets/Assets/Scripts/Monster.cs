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
        var data = controller.GetComponent<controllerScript>().getMonster();
        this.icon = (char)data[0];
        this.monName = (string)data[1];
        this.hp = (int)data[2];
        this.dmg = (int)data[3];
        this.def = (int)data[4];
        this.lootTable = (int)data[5];
        this.location = controller.GetComponent<controllerScript>().getLocation();
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
