using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class controllerScript : MonoBehaviour
{

    // Start is called before the first frame update
    public int currentMap = 0;
    public Tilemap tileMapWalls;
    public Dictionary<char,List<object>> monsters = new Dictionary<char, List<object>>();
    public Monster Monster;
    public Cavegenerator Generator;
    List<char> options = new List<char>{'A','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
    private List<Monster> monList = new List<Monster>();
    void Start()
    {
        //read all csvs
        //read monster csv, infrastructure can be copied for items
        using(var reader = new StreamReader(@"Assets\Assets\Materials\monsters.csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var items = line.Split(',');
                char key = ' ';
                for(int i = 0; i < 6; i++)
                {
                    if(i==0)
                    {
                        key = items[i].ToCharArray()[0];
                        monsters[key] = new List<object>();
                        monsters[key].Add(items[i]);
                    }
                    else
                    {
                        monsters[key].Add(items[i]);
                    }

                }                
            }              
        }
        for(int i = 0; i<61; i++)
        {
            placeMonster(getLocation());
        }
    }
        

    // Update is called once per frame
    void Update()
    {
        
    }

    //selects monster and gives it location
    void placeMonster(Vector3Int location)
    {
        //List<object> Selection = monsters[options[UnityEngine.Random.Range(0,options.Count-1)]]
        Monster thisMonster = Instantiate(Monster, this.transform);
        thisMonster.Init(Generator, location,tileMapWalls);
        thisMonster.UpdatePosition(location);
        monList.Add(thisMonster);

    }

    public Cavegenerator getCaveGenerator()
    {
        return Generator;
    }
    public List<object> getMonster()
    {
        int selected = UnityEngine.Random.Range(0, options.Count);
        List<object> Selection = monsters[options[selected]];
        return Selection;
    }
    
    public Vector3Int getLocation()
    {
        //This should give a random location pls but fornow will just return a set one, debugging lols XD
        bool found = false;
        Vector3Int loc = new Vector3Int();
        while(!found) 
        { 
            loc = new Vector3Int(UnityEngine.Random.Range(2, 158), UnityEngine.Random.Range(2, 78), 0);
            if (GetComponentInChildren<Cavegenerator>().getTileArray()[loc.x,loc.y].getContainedMonster() == null && !GetComponentInChildren<Cavegenerator>().getTileArray()[loc.x,loc.y].impassable)
            {
                found = true;
                gameObject.transform.Find("Grid");
            }
        }
        return(loc);
    }
}
