using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spanwer : MonoBehaviour
{

    

    public GameObject[] standbyGroup;

    // Use this for initialization
    void Start () {
		SpawnNext();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnNext()
    {
        GetComponent<AudioSource>().Play();
        GameObject go = Instantiate(standbyGroup[FindObjectOfType<Queue>().Next()], this.transform.position,
            Quaternion.identity);
        go.transform.parent = this.transform;

    }
}
