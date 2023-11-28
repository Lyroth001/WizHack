using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public int hp = 10;
    public int score = 0;
    public int dmg = 1;
    public int def = 1;
    public Tilemap tileMapWalls;
    public looker myLooker;
    private Vector2Int pos = new Vector2Int(0, 0);
    public controllerScript Controller;
    public TileBase spellTrail;
    public TMP_Text healthTxt;
    //public TMP_Text scoreTxt;
    //public TMP_Text dmgTxt;
    //public TMP_Text defTxt;

    public Cavegenerator grid;

    private char lastDir = 'u';
    // Start is called before the first frame update
    public Vector2Int getLocation()
    {
        return pos;
    }

    public void addScore(int val)
    {
        score += val;
    }
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

    public void castSpell(char dir, int damage, int range)
    {
        Tile target = grid.getTileArray()[pos.x, pos.y];
        bool freeFlight = true;
        int timeOfFlight = 0;
        List<Tile> trailList = new List<Tile>();
        switch (dir)
        {
            case 'u' :
                while (freeFlight == true && timeOfFlight < range)
                {
                    trailList.Add(target);
                    tileMapWalls.SetTile(new Vector3Int(target.pos.x,target.pos.y,0), spellTrail);
                    if (target.up != null && ! target.up.impassable)
                    {
                        if (target.up.getContainedMonster() != null)
                        {
                            target.up.containedMonster.damage(damage);
                            freeFlight = false;
                        }
                        else
                        {
                            target = target.up;
                            timeOfFlight++;
                        }
                    }
                    else
                    {
                        freeFlight = false;
                    }
                }
                break;
            case 'r' :
                while (freeFlight == true && timeOfFlight < range)
                {
                    trailList.Add(target);
                    tileMapWalls.SetTile(new Vector3Int(target.pos.x,target.pos.y,0), spellTrail);
                    if (target.right != null && ! target.right.impassable)
                    {
                        if (target.right.getContainedMonster() != null)
                        {
                            target.right.containedMonster.damage(damage);
                            freeFlight = false;
                        }
                        else
                        {
                            target = target.right;
                            timeOfFlight++;
                        }
                    }
                    else
                    {
                        freeFlight = false;
                    }
                }
                break;
            case 'd' :
                while (freeFlight == true && timeOfFlight < range)
                {
                    trailList.Add(target);
                    tileMapWalls.SetTile(new Vector3Int(target.pos.x,target.pos.y,0), spellTrail);
                    if (target.down != null && ! target.down.impassable)
                    {
                        if (target.down.getContainedMonster() != null)
                        {
                            target.down.containedMonster.damage(damage);
                            freeFlight = false;
                        }
                        else
                        {
                            target = target.down;
                            timeOfFlight++;
                        }
                    }
                    else
                    {
                        freeFlight = false;
                    }
                }
                break;
            case 'l' :
                while (freeFlight == true && timeOfFlight < range)
                {
                    tileMapWalls.SetTile(new Vector3Int(target.pos.x,target.pos.y,0), spellTrail);
                    trailList.Add(target);
                    if (target.left != null && ! target.left.impassable)
                    {
                        if (target.left.getContainedMonster() != null)
                        {
                            target.left.containedMonster.damage(damage);
                            freeFlight = false;
                        }
                        else
                        {
                            target = target.left;
                            timeOfFlight++;
                        }
                    }
                    else
                    {
                        freeFlight = false;
                    }
                }
                break;
        }

        StartCoroutine(cleanTrail(trailList));

        // foreach (Tile tile in trailList)
        // {
        //     tileMapWalls.SetTile(new Vector3Int(tile.pos.x,tile.pos.y,0), null);
        // }
    }

    IEnumerator cleanTrail(List<Tile> list)
    {
        foreach (Tile tile in list)
        {
            yield return new WaitForSeconds(0.02F);
            tileMapWalls.SetTile(new Vector3Int(tile.pos.x,tile.pos.y,0), null);
        }
    }
    
    public bool dig(char dir)
    {
        switch (dir)
        {
            case 'u' :
                if (grid.getTileArray()[pos.x, pos.y].up != null && grid.getTileArray()[pos.x, pos.y].up.impassable)
                {
                    Tile oldTile = grid.getTileArray()[pos.x, pos.y].up;
                    tileMapWalls.SetTile(new Vector3Int(pos.x,pos.y+1,0),null);
                    grid.setTile(new Tile(false,new Vector2Int(pos.x,pos.y+1),oldTile.up,oldTile.right,oldTile.down,oldTile.left,oldTile.containedMonster));
                    grid.updateTile(grid.getTileArray()[pos.x,pos.y].up.pos);
                    return true;
                }   
                break;
            case 'r' :
                if (grid.getTileArray()[pos.x, pos.y].right != null && grid.getTileArray()[pos.x, pos.y].right.impassable)
                {
                    Tile oldTile = grid.getTileArray()[pos.x, pos.y].right;
                    tileMapWalls.SetTile(new Vector3Int(pos.x+1,pos.y,0),null);
                    grid.setTile(new Tile(false,new Vector2Int(pos.x+1,pos.y),oldTile.up,oldTile.right,oldTile.down,oldTile.left,oldTile.containedMonster));
                    grid.updateTile(grid.getTileArray()[pos.x,pos.y].right.pos);
                    return true;
                }
                break;
            case 'd' :
                if (grid.getTileArray()[pos.x, pos.y].down != null && grid.getTileArray()[pos.x, pos.y].down.impassable)
                {
                    Tile oldTile = grid.getTileArray()[pos.x, pos.y].down;
                    tileMapWalls.SetTile(new Vector3Int(pos.x,pos.y-1,0),null);
                    grid.setTile(new Tile(false,new Vector2Int(pos.x,pos.y-1),oldTile.up,oldTile.right,oldTile.down,oldTile.left,oldTile.containedMonster));
                    grid.updateTile(grid.getTileArray()[pos.x,pos.y].down.pos);
                    return true;
                }
                break;
            case 'l' :
                if (grid.getTileArray()[pos.x, pos.y].left != null && grid.getTileArray()[pos.x, pos.y].left.impassable)
                {
                    Tile oldTile = grid.getTileArray()[pos.x, pos.y].left;
                    tileMapWalls.SetTile(new Vector3Int(pos.x-1,pos.y,0),null);
                    grid.setTile(new Tile(false,new Vector2Int(pos.x-1,pos.y),oldTile.up,oldTile.right,oldTile.down,oldTile.left,oldTile.containedMonster));
                    grid.updateTile(grid.getTileArray()[pos.x,pos.y].left.pos);
                    return true;
                }
                break;
            default:
                return false;
            
        }

        return false;
    }
    
    //wall detection
    public void UpdatePos(){
        this.transform.position = tileMapWalls.GetCellCenterWorld(new Vector3Int(pos.x, pos.y, 0));
        if (hp < 20)
        {
            hp++;
        }
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
                    if (grid.getTileArray()[pos.x, pos.y].up.containedMonster)
                    {
                        grid.getTileArray()[pos.x, pos.y].up.containedMonster.damage(dmg);
                    }
                    else
                    {
                        pos.y++;
                        UpdatePos();
                        
                    }
                }
            }

            lastDir = 'u';
            Controller.triggerMonsters();

        }
        
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (grid.getTileArray()[pos.x, pos.y].down != null)
            {
                if (grid.getTileArray()[pos.x, pos.y].down.impassable == false)
                {
                    if (grid.getTileArray()[pos.x, pos.y].down.containedMonster)
                    {
                        grid.getTileArray()[pos.x, pos.y].down.containedMonster.damage(dmg);
                    }
                    else
                    {
                        pos.y -= 1;
                        UpdatePos();
                    }
                }
            }

            lastDir = 'd';
            Controller.triggerMonsters();

        }
        
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if (grid.getTileArray()[pos.x, pos.y].left != null)
            {
                if (grid.getTileArray()[pos.x, pos.y].left.impassable == false)
                {
                    if (grid.getTileArray()[pos.x, pos.y].left.containedMonster)
                    {
                        grid.getTileArray()[pos.x, pos.y].left.containedMonster.damage(dmg);
                    }
                    else
                    {
                        pos.x -= 1;
                        UpdatePos();
                    }
                }
            }

            lastDir = 'l';
            Controller.triggerMonsters();


        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (grid.getTileArray()[pos.x, pos.y].right != null)
            {
                if (grid.getTileArray()[pos.x, pos.y].right.impassable == false)
                {
                    if (grid.getTileArray()[pos.x, pos.y].right.containedMonster)
                    {
                        grid.getTileArray()[pos.x, pos.y].right.containedMonster.damage(dmg);
                    }
                    else
                    {
                        pos.x++;
                        UpdatePos();
                    }
                }
            }

            lastDir = 'r';
            Controller.triggerMonsters();


        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            dig(lastDir);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            castSpell(lastDir, 2, 6);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            spawnLooker();
        }
        
        // scoreTxt.text = "Score: " + score.ToString();
        healthTxt.text = "HP: " + hp.ToString();
        // dmgTxt.text = "DMG: " + dmg.ToString();
        // defTxt.text = "DEF: " + def.ToString();
    }

    void spawnLooker()
    { 
        looker thisLooker = Instantiate(myLooker, this.transform);
        thisLooker.setTileMap(grid);
        Vector2Int toPass = new Vector2Int(pos.x, pos.y);
        thisLooker.setLoc(toPass);
        thisLooker.setTileWalls(tileMapWalls);


    }
    void checkHealth()
    {
        if (hp <= 0)
        {
            //die
            Destroy(gameObject);
            Application.Quit();
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
