using System;
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
        public Sprite[] spriteArray;
        private Cavegenerator cavegenerator;
        private Tilemap tilemap;
        private Player thePlayer;

        

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
        updateSprite(gameObject.GetComponent<SpriteRenderer>());
        UpdatePosition(location);
    }

    private void Update()
    {
        move();
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
        
        Vector2Int conv = thePlayer.getLocation();
        Vector3 toCheck = new Vector3(conv.x, conv.y, 0);
        if (GetComponent<BoxCollider2D>().bounds.Contains(toCheck))
        {
            Debug.Log("truerheu");
            Vector3 locationCalc = new Vector3(location.x, location.y, 0);
            Vector3 movDir = locationCalc - toCheck;
            Vector3Int mov = new Vector3Int();
            if (movDir.x > 0)
            {
                location.x -= 1;
            }
            else
            {
                location.x += 1;
            }

            if (movDir.y > 0)
            {
                location.y -= 1;
            }
            else
            {
                location.x += 1;
            }
            UpdatePosition(location);
        }
    }

    public void setPlayer(Player newPlayer)
    {
        thePlayer = newPlayer;
    }

    void updateSprite(SpriteRenderer spriteRenderer)
    {
        switch (icon)
        {
            case 'A':
                spriteRenderer.sprite = spriteArray[0];
                break;
            case 'a':
                spriteRenderer.sprite = spriteArray[1];
                break;
            case 'b':
                spriteRenderer.sprite = spriteArray[2];
                break;
            case 'c':
                spriteRenderer.sprite = spriteArray[3];
                break;
            case 'd':
                spriteRenderer.sprite = spriteArray[4];
                break;
            case 'e':
                spriteRenderer.sprite = spriteArray[5];
                break;
            case 'f':
                spriteRenderer.sprite = spriteArray[6];
                break;
            case 'g':
                spriteRenderer.sprite = spriteArray[7];
                break;
            case 'h':
                spriteRenderer.sprite = spriteArray[8];
                break;
            case 'i':
                spriteRenderer.sprite = spriteArray[9];
                break;
            case 'j':
                spriteRenderer.sprite = spriteArray[10];
                break;
            case 'k':
                spriteRenderer.sprite = spriteArray[11];
                break;
            case 'l':
                spriteRenderer.sprite = spriteArray[12];
                break;
            case 'm':
                spriteRenderer.sprite = spriteArray[13];
                break;
            case 'n':
                spriteRenderer.sprite = spriteArray[14];
                break;
            case 'o':
                spriteRenderer.sprite = spriteArray[15];
                break;
            case 'p':
                spriteRenderer.sprite = spriteArray[16];
                break;
            case 'q':
                spriteRenderer.sprite = spriteArray[17];
                break;
            case 'r':
                spriteRenderer.sprite = spriteArray[18];
                break;
            case 's':
                spriteRenderer.sprite = spriteArray[19];
                break;
            case 't':
                spriteRenderer.sprite = spriteArray[20];
                break;
            case 'u':
                spriteRenderer.sprite = spriteArray[21];
                break;
            case 'v':
                spriteRenderer.sprite = spriteArray[22];
                break;
            case 'w':
                spriteRenderer.sprite = spriteArray[23];
                break;
            case 'x':
                spriteRenderer.sprite = spriteArray[24];
                break;
            case 'y':
                spriteRenderer.sprite = spriteArray[25];
                break;
            case 'z':
                spriteRenderer.sprite = spriteArray[26];
                break;
                
               
        }
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
