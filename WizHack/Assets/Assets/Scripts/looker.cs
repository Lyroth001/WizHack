using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class looker : MonoBehaviour
{
    private Vector2Int pos;

    private Cavegenerator grid;

    private Tilemap tileMapWalls;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdatePos(){
        this.transform.position = tileMapWalls.GetCellCenterWorld(new Vector3Int(pos.x, pos.y, 0));
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (grid.getTileArray()[pos.x, pos.y].right != null)
            {
                pos.x++;
                UpdatePos();
            }
        }
    }

    public void setTileMap(Cavegenerator input)
    {
        Cavegenerator grid = input;
    }

    public void setLoc(Vector2Int startLoc)
    {
        pos = startLoc;
    }

    public void setTileWalls(Tilemap newMap)
    {
        Tilemap tileMapWalls = newMap;
    }
}
