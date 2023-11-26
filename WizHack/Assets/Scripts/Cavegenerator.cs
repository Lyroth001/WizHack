using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;


public class Cavegenerator : MonoBehaviour
{
    private Random rand = new Random();
    public bool[] map;
    public int width = 0;
    public int height = 0;
    public Tilemap walls;
    public TileBase solid;
    public Vector3Int[] coordGrid;
    public int offX;
    public int offY;
    public bool[] mapWide;
    public Vector3Int[] coordGridWide;
    public static Tile[,] TileArray;

    public Tile[,] getTileArray()
    {
        return TileArray;
    }
    private void Start()
    {
        TileArray = new Tile[width * 2, height];
        coordGrid = new Vector3Int[(width * height)];
        int index = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                coordGrid[index] = (new Vector3Int(i, j, 0));
                index++;
            }
        }
        map = Generate(width, height, 4, 45);
        
        drawGrid();
    }

    public void drawGrid()
    {
        var wallDict = coordGrid.Zip(map, (a, b) => new { pos = a, val = b });
        
        foreach (var posval in wallDict)
        {
            TileArray[(posval.pos.x * 2),(posval.pos.y)] = new Tile(posval.val,new Vector2Int(posval.pos.x*2,posval.pos.y));
            TileArray[(posval.pos.x * 2 + 1), (posval.pos.y)] = new Tile(posval.val, new Vector2Int(posval.pos.x * 2 + 1, posval.pos.y));
            if (posval.val)
            {
                walls.SetTile(new Vector3Int(posval.pos.x*2 - offX, posval.pos.y - offY, 0), solid);
                walls.SetTile(new Vector3Int((posval.pos.x*2)+1 - offX, posval.pos.y - offY, 0), solid);
            }
        }

        foreach (var tile in TileArray)
        {
            if (tile.pos.x > 0)
            {
                tile.left = TileArray[tile.pos.x - 1, tile.pos.y];
                if (tile.pos.x < (2 * width - 1))
                {
                    tile.right = TileArray[tile.pos.x+1,tile.pos.y];
                }
            }
            

            if (tile.pos.y > 0)
            {
                tile.up = TileArray[tile.pos.x, tile.pos.y - 1];
                if (tile.pos.y < (height - 1))
                {
                    tile.down = TileArray[tile.pos.x, tile.pos.y + 1];
                }

            }
        }
    }
    
    private bool[] Generate(int width, int height, int iterations = 4, int percentAreWalls = 40)
    {
        var map = new bool[width * height];

        RandomFill(map, width, height, percentAreWalls);
        
        for(var i = 0; i < iterations; i++)
            map = Step(map, width, height);

        return map;
    }
    
    private void RandomFill(bool[] map, int width, int height, int percentAreWalls = 40)
    {
        var randomColumn = rand.Next(4, width - 4);
        
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if(x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    map[x + y * width] = true;
                else if(x != randomColumn && rand.Next(100) < percentAreWalls)
                    map[x + y * width] = true;
            }
        }
    }

    private bool[] Step(bool[] map, int width, int height)
    {
        var newMap = new bool[width * height];
        
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if(x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    newMap[x + y * width] = true;
                else
                    newMap[x + y * width] = PlaceWallLogic(map, width, height, x, y);
            }
        }

        return newMap;
    }

    private bool PlaceWallLogic(bool[] map, int width, int height, int x, int y) =>
        CountAdjacentWalls(map, width, height, x, y) >= 5 ||
        CountNearbyWalls(map, width, height, x, y) <= 2;

    private int CountAdjacentWalls(bool[] map, int width, int height, int x, int y)
    {
        var walls = 0;
        
        for(var mapX = x - 1; mapX <= x + 1; mapX++)
        {
            for(var mapY = y - 1; mapY <= y + 1; mapY++)
            {
                if(map[mapX + mapY * width])
                    walls++;
            }
        }

        return walls;
    }
    
    private int CountNearbyWalls(bool[] map, int width, int height, int x, int y)
    {
        var walls = 0;
        
        for(var mapX = x - 2; mapX <= x + 2; mapX++)
        {
            for(var mapY = y - 2; mapY <= y + 2; mapY++)
            {
                if(Math.Abs(mapX - x) == 2 && Math.Abs(mapY - y) == 2)
                    continue;
                
                if(mapX < 0 || mapY < 0 || mapX >= width || mapY >= height)
                    continue;
                        
                if(map[mapX + mapY * width])
                    walls++;
            }
        }

        return walls;
    }
}
