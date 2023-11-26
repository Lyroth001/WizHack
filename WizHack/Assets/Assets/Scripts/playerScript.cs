using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerScript : MonoBehaviour
{
    public int hp = 10;
    public int score = 0;
    public int dmg = 1;
    public int def = 1;
    public Tilemap tileMapWalls;
    private Vector2Int pos = new Vector2Int(0, 0);

    public Cavegenerator grid;
    // Start is called before the first frame update
    void Start()
    {
        bool trapped = true;
        while (trapped)
        {
            if (!grid.getTileArray()[pos.x,pos.y].impassable)
            {
                trapped = false;
            }
            else if (pos.x < grid.width * 2 - 1)
            {
                pos.x++;
            }
            else
            {
                pos.y++;
                pos.x = 0;
            }
        }
        UpdatePos();
    }

    //wall detection
    public void UpdatePos(){
        this.transform.position = tileMapWalls.GetCellCenterWorld(new Vector3Int(pos.x, pos.y, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (grid.getTileArray()[pos.x, pos.y].up != null)
            {
                if (grid.getTileArray()[pos.x, pos.y].up.impassable == false)
                {
                    pos.y ++;
                    UpdatePos();
                }
            }

        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {

            if (grid.getTileArray()[pos.x, pos.y].down != null)
            {

                if (grid.getTileArray()[pos.x, pos.y].down.impassable == false)
                {

                    pos.y -=1;
                    UpdatePos();
                }
            }

        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if (grid.getTileArray()[pos.x, pos.y].left != null)
            {
                if (grid.getTileArray()[pos.x, pos.y].left.impassable == false)
                {
                    pos.x -= 1;
                    UpdatePos();
                }
            }

        
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (grid.getTileArray()[pos.x, pos.y].right != null)
            {
                if (grid.getTileArray()[pos.x, pos.y].right.impassable == false)
                {
                    pos.x++;
                    UpdatePos();
                }
            }

        
        }
    }
}
