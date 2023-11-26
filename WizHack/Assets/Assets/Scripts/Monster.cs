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
    public Monster(char icon, string monName, int hp, int dmg, int def, int lootTable, Vector3 location)
    {
        this.icon = icon;
        this.monName = monName;
        this.hp = hp;
        this.dmg = dmg;
        this.def = def;
        this.lootTable = lootTable;
        this.location = location;
    }
    

    void Start()
    {
        
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
