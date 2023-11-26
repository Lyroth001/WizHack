using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public bool impassable;
    public Tile up;
    public Tile right;
    public Tile down;
    public Tile left;
    public Vector2Int pos;
    
    public Monster containedMonster = null;

    public Tile(bool impassable, Vector2Int pos, Tile up = null, Tile right = null, Tile down = null, Tile left = null, Monster containedMonster = null)
    {
        this.pos = pos;
        this.impassable = impassable;
        this.up = up;
        this.right = right;
        this.down = down;
        this.left = left;
        this.containedMonster = containedMonster;
    }

    public Monster getContainedMonster()
    {
        return containedMonster;
    }

    public void setMonster(Monster monster)
    {
        containedMonster = monster;
    }

    public void updateNeighbours()
    {
        if (up != null)
        {
            up.down = this;
        }
        if (right != null)
        {
            right.left = this;
        }
        if (down != null)
        {
            down.up = this;
        }
        if (left != null)
        {
            left.right = this;
        }
    }
}