using System.Collections.Generic;
using UnityEngine;
using System;

public class GridMovement : MonoBehaviour
{
    Moves moves;

    [SerializeField] GameObject morphBotRef;
    [SerializeField] Transform morphBotsParent;
    [SerializeField] GridMain gridMain;
    [SerializeField] float speed;
    float timer, percent;

    int xIndex, yIndex, zIndex;

    List<Vector3Int> oldLocations;
    List<Vector3Int> newLocations;
    List<GameObject> morphBots;
    int axisReturn;

    public void BeginMovement(Moves _moves)
    {
        moves = _moves;

        foreach (Transform child in morphBotsParent)
        {
            Destroy(child.gameObject);
        }

        gridMain.grid = new GridNode[gridMain.gridSize.x, gridMain.gridSize.y, gridMain.gridSize.z];

        for (int x = 0; x < gridMain.gridSize.x; x++)
        {
            for (int z = 0; z < gridMain.gridSize.z; z++)
            {
                gridMain.grid[x, 0, z] = new GridNode(true, new Vector3Int(x, 0, z));
            }
        }

        for (int x = 0; x < gridMain.gridSize.x; x++)
        {
            for (int y = 1; y < gridMain.gridSize.y; y++)
            {
                for (int z = 0; z < gridMain.gridSize.z; z++)
                {
                    gridMain.grid[x, y, z] = new GridNode(false, new Vector3Int(x, y, z));
                }
            }
        }

        axisReturn = 0;
        xIndex = yIndex = zIndex = 0;
        timer = speed;
        PlaceOnGrid(moves);
        XManager();
    }

    private float Timer()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, speed);

        percent = 1 - (timer / speed);

        return timer;
    }

    private void Move()
    {
        if (Timer() > 0)
        {
            //timer = Mathf.Clamp(timer - Time.deltaTime, 0, speed);
            //percent = 1 - timer / speed;

            for (int i = 0; i < oldLocations.Count; i++)
            {
                morphBots[i].transform.localPosition = Vector3.Lerp(oldLocations[i], newLocations[i], percent);
            }

            Invoke("Move", Time.deltaTime);
        }

        else
        {
            timer = speed;

            for (int i = 0; i < oldLocations.Count; i++)
            {
                morphBots[i].transform.localPosition = newLocations[i];
                gridMain.grid[newLocations[i].x, newLocations[i].y, newLocations[i].z].isTaken = true;
                gridMain.grid[newLocations[i].x, newLocations[i].y, newLocations[i].z].currentMorphBot = gridMain.grid[oldLocations[i].x, oldLocations[i].y, oldLocations[i].z].currentMorphBot;
                gridMain.grid[oldLocations[i].x, oldLocations[i].y, oldLocations[i].z].currentMorphBot = null;
                gridMain.grid[oldLocations[i].x, oldLocations[i].y, oldLocations[i].z].isTaken = false;
            }

            switch (axisReturn)
            {
                case 0:
                    XManager();
                    break;

                case 1:
                    ZManager();
                    break;

                case 2:
                    YManager();
                    break;
            }
        }
    }

    public void PlaceOnGrid(Moves moves)
    {
        for (int i = 0; i < moves.start.Count; i++)
        {
            Vector3Int position = new Vector3Int(moves.start[i].Item1, moves.start[i].Item3 + 1, moves.start[i].Item2);
            GameObject morphBot = Instantiate(morphBotRef, morphBotsParent);
            morphBot.transform.localPosition = position;
            gridMain.grid[position.x, position.y, position.z] = new GridNode(true, position);
            gridMain.grid[position.x, position.y, position.z].currentMorphBot = morphBot;
        }
    }

    private void XManager()
    {
        xIndex++;

        if (xIndex <= moves.x.Count)
        {
            oldLocations = new List<Vector3Int>();
            newLocations = new List<Vector3Int>();
            morphBots = new List<GameObject>();

            foreach (Tuple<int, int, int> t in moves.x[xIndex - 1])
            {
                Vector3Int position = new Vector3Int(t.Item1, t.Item3 + 1, t.Item2);

                oldLocations.Add(position);
                newLocations.Add(new Vector3Int(position.x + 1, position.y, position.z));
                morphBots.Add(gridMain.grid[position.x, position.y, position.z].currentMorphBot);

            }

            Move();
        }

        else
        {
            axisReturn++;
            ZManager();
        }
    }

    public void YManager()
    {
        yIndex++;

        if (yIndex <= moves.z.Count)
        {
            oldLocations = new List<Vector3Int>();
            newLocations = new List<Vector3Int>();
            morphBots = new List<GameObject>();

            foreach (Tuple<int, int, int> t in moves.z[yIndex - 1])
            {
                Vector3Int position = new Vector3Int(t.Item1, t.Item3 + 1, t.Item2);

                morphBots.Add(gridMain.grid[position.x, position.y, position.z].currentMorphBot);

                oldLocations.Add(position);
                newLocations.Add(new Vector3Int(position.x, position.y + 1, position.z));

            }

            Move();
        }

        else
        {
            print("FINISHED!");
        }
    }

    private void ZManager()
    {
        zIndex++;

        if (zIndex <= moves.y.Count)
        {
            oldLocations = new List<Vector3Int>();
            newLocations = new List<Vector3Int>();
            morphBots = new List<GameObject>();

            foreach (Tuple<int, int, int> t in moves.y[zIndex - 1])
            {
                Vector3Int position = new Vector3Int(t.Item1, t.Item3 + 1, t.Item2);

                oldLocations.Add(position);
                newLocations.Add(new Vector3Int(position.x, position.y, position.z + 1));
                morphBots.Add(gridMain.grid[position.x, position.y, position.z].currentMorphBot);

            }

            Move();
        }

        else
        {
            axisReturn++;
            YManager();
        }
    }
}
