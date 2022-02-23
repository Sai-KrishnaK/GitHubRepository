using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static int SCREEN_WIDTH = 64;
    private static int SCREEN_Height = 48;

    public float speed = 0.1f; // frame rate speed

    public float timer = 0;

    Cell[,] grid = new Cell[SCREEN_WIDTH, SCREEN_Height];

    // Start is called before the first frame update
    void Start()
    {
        PlaceCells();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= speed)
        {
            timer = 0f;
            CountNeighbors();
            PopulationControl();
        }
        else
        {
            timer += Time.deltaTime;
        }
        
    }
    void PlaceCells()
    {
        for (int y = 0; y < SCREEN_Height; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                Cell cell = Instantiate(Resources.Load("Prefabs/Cell", typeof(Cell)), new Vector2(x, y), Quaternion.identity) as Cell;
                grid[x, y] = cell;
                grid[x, y].SetAlive(RandomAliveCell());
            }
        }
    }

    void CountNeighbors()
    {
        for (int y = 0; y < SCREEN_Height; y++)
        {
            for (int x = 0; x < SCREEN_WIDTH; x++)
            {
                int numNeighbors = 0;

                //North
                if (y + 1 < SCREEN_Height)
                {

                    if (grid[x, y + 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //East
                if (x + 1 < SCREEN_WIDTH)
                {

                    if (grid[x + 1, y].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //South
                if (y - 1 >= 0)
                {
                    if (grid[x, y - 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //West
                if (x - 1 >= 0)
                {
                    if (grid[x - 1, y].isAlive)
                    {
                        numNeighbors++;
                    }

                }
                //Northeaset
                if(x+1 < SCREEN_WIDTH && y+1 < SCREEN_Height)
                {
                    if(grid[x+1, y+1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //Northwest
                if(x-1 >=0 && y+1 < SCREEN_Height)
                {
                    if(grid[x-1,y+1].isAlive)
                    {
                        numNeighbors++;
                    }
                }
                //SouthEast
                if (x + 1 < SCREEN_WIDTH && y - 1 >= 0)
                {
                    if (grid[x + 1, y - 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                //SouthWest
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    if (grid[x - 1, y - 1].isAlive)
                    {
                        numNeighbors++;
                    }
                }

                grid[x, y].numNeighbors = numNeighbors;

            }
            }
        }

    void PopulationControl()
    {
        //Rules
        for(int y=0; y<SCREEN_Height; y++)
        {
            for(int x=0; x < SCREEN_WIDTH; x++)
            {
                if(grid[x,y].isAlive)
                {
                    //Cell alive
                    if(grid[x,y].numNeighbors != 2 && grid[x,y].numNeighbors != 3)
                    {
                        grid[x, y].SetAlive(false);

                    }
                }
                else
                {
                    //Cell Dead
                    if(grid[x,y].numNeighbors == 3)
                    {
                        grid[x, y].SetAlive(true);
                    }
                }

            }
        }
    }

        bool RandomAliveCell()
        {
            int rand = UnityEngine.Random.Range(0, 100);

            if (rand > 75)
            {
                return true;
            }
            return false;
        }
    }

