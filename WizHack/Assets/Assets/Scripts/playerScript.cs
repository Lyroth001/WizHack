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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //wall detection
    public void moveChar(Vector3 pos){
        if (! tileMapWalls.GetComponent<TilemapCollider2D>().OverlapPoint(pos)){
            this.transform.position = pos;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            Vector3 pos = this.transform.position;
            pos.y += 0.3F;
            moveChar(pos);
            
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            Vector3 pos = this.transform.position;
            pos.y -= 0.3F;
            moveChar(pos);

        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            Vector3 pos = this.transform.position;
            pos.x-= 0.12F;
            moveChar(pos);

        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            Vector3 pos = this.transform.position;
            pos.x += 0.12F;
            moveChar(pos);
        }
    }
}
