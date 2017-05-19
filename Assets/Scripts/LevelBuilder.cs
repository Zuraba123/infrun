using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public float Dtt = 10f;
    public Vector3 Psp = Vector3.zero;
	public Transform player;
    public GameObject[] platform;
    void Start ()
    {
        int counter = 0;
        while (counter <10)
        {
            CreatePlatform();
            counter = counter + 1;
        }
    }
    int pp;
    int lastPP = 0;
    void Update ()
    {
        int pp = (int)player.position.z / 50;
        if (pp > lastPP)
        {
            CreatePlatform();
        }
        lastPP = pp;
    }
    void CreatePlatform ()
    {
        Psp.z = Psp.z + 62.5f;
        Debug.Log("PLTFRM");
        GameObject clone;
        int plch = Random.Range(0, 14);
        clone = Instantiate(platform[ plch ]);
        clone.transform.position = Psp;
        Destroy(clone, Dtt);
    }
}
