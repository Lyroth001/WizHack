using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
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
    List<char> options = new List<char>{'A','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
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
                char key = 'a';
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
        placeMonster(new Vector3(3,3,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //selects monster and gives it location
    void placeMonster(Vector3 location)
    {
        //List<object> Selection = monsters[options[UnityEngine.Random.Range(0,options.Count-1)]];
        List<object> Selection = monsters['a'];
    }
}
