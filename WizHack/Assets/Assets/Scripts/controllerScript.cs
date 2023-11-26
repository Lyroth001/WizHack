using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class controllerScript : MonoBehaviour
{

    // Start is called before the first frame update
    public int currentMap = 0;
    public Tilemap tileMapWalls;
    public Dictionary<char,List<object>> monsters = new Dictionary<char, List<object>>();
    void Start()
    {
        //read all csvs
        //read monster csv
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
            for(int i = 0; i < 6; i++)
                {
                    Debug.Log(monsters['a'][i]);
                }
                
        }
        }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector2 updateLocation(Vector3 location, Vector3 direction, Vector3 icon)
    {
        return new Vector3(0,0);

    }
}
