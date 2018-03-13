using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGird : MonoBehaviour
{

    private static MyGird _instance;

    public static MyGird Instance
    {
        get
        {
            return _instance;
        }
    }

    public static int w = 10;
    public static int h = 20;

    public Transform[,] gird = new Transform[w, h];

    private void Awake()
    {
        _instance = this;
    }


    public Vector2 RoundVector2(Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public bool IsInside(Vector2 pos)
    {
        return (pos.x >= 0 && pos.x < w && pos.y >= 0);
    }

    public bool IsRowFull(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (gird[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void DeleteRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            Destroy(gird[x, y].gameObject);
            gird[x, y] = null;
        }
    }

    public void DecreaseRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (gird[x, y] != null)
            {
                gird[x, y - 1] = gird[x, y];
                gird[x, y] = null;

                gird[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void DecreaseRowAbove(int y)
    {
        for (int i = y; i < h; i++)
        {
            DecreaseRow(i);
        }
    }

    public void DeleteFullRows()
    {

        int currentLine = 0;

        for (int y = 0; y < h; y++)
        {
            if (IsRowFull(y))
            {
                currentLine++;
                DeleteRow(y);
                DecreaseRowAbove(y + 1);
                y--;
            }
        }

        FindObjectOfType<GameManager>().SetScore(currentLine);
    }
}
