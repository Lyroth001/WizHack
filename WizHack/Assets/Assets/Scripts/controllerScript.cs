using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class controllerScript : MonoBehaviour
{

    // Start is called before the first frame update
    public int currentMap = 0;
    public Tilemap tileMapWalls;

    void Start()
    {
       
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
