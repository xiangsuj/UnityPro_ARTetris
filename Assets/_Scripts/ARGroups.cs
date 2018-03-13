using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARGroups : MonoBehaviour
{

    public float freezingTime = 0.5f;

    private float pressingButtonTime = 0f;

    private float lastFallTime = 0;

    // Use this for initialization
    void Start()
    {
        if (!IsValidGridPos())
        {
           FindObjectOfType<GameManager>().SetGameOver();
           Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (VBInputEvent.Instance.whichButton == 1)
        {
            pressingButtonTime += Time.deltaTime;

            if (pressingButtonTime > freezingTime)
            {
                //移动

                transform.position -= new Vector3(1, 0, 0);

                if (IsValidGridPos())
                {
                    UpdateGrid();
                }
                else
                {
                    transform.position += new Vector3(1, 0, 0);
                }

                pressingButtonTime = 0;
            }
        }

        if (VBInputEvent.Instance.whichButton == 2)
        {
            pressingButtonTime += Time.deltaTime;

            if (pressingButtonTime > freezingTime)
            {
                //移动

                transform.position += new Vector3(1, 0, 0);

                if (IsValidGridPos())
                {
                    UpdateGrid();
                }
                else
                {
                    transform.position -= new Vector3(1, 0, 0);
                }


                pressingButtonTime = 0;
            }
        }
        if (VBInputEvent.Instance.whichButton == 3)
        {
            pressingButtonTime += Time.deltaTime;

            if (pressingButtonTime > freezingTime)
            {
                //旋转

                transform.Rotate(0, 0, -90);

                if (IsValidGridPos())
                {
                    UpdateGrid();
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }

                pressingButtonTime = 0;
            }
        }

        Fall();
    }

    void Fall()
    {
        if ((Time.time - lastFallTime) > FindObjectOfType<Queue>().TimeFrame)
        {
            transform.position -= new Vector3(0, 1, 0);


            if (IsValidGridPos())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);

                MyGird.Instance.DeleteFullRows();

                FindObjectOfType<Spanwer>().SpawnNext();

                enabled = false;
            }

            lastFallTime = Time.time;
        }
    }

    private bool IsValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            Vector2 v = MyGird.Instance.RoundVector2(new Vector2(child.position.x,child.position.y));

            if (!MyGird.Instance.IsInside(v))
            {
                Debug.Log("222222");
                Debug.Log(this.gameObject.name);
                return false;
            }
            if (MyGird.Instance.gird[(int)v.x, (int)v.y] != null && MyGird.Instance.gird[(int)v.x, (int)v.y].parent != this.transform)
            {

                Debug.Log(this.gameObject.name);
                return false;
            }
        }
        return true;
    }

    private void UpdateGrid()
    {
        for (int x = 0; x < MyGird.w; x++)
        {
            for (int y = 0; y < MyGird.h; y++)
            {
                if (MyGird.Instance.gird[x, y] != null)
                {
                    if (MyGird.Instance.gird[x, y].parent == transform)
                    {
                        MyGird.Instance.gird[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform child in transform)
        {
            Vector2 v = MyGird.Instance.RoundVector2(child.position);

            MyGird.Instance.gird[(int)v.x, (int)v.y] = child;
        }
    }
}
