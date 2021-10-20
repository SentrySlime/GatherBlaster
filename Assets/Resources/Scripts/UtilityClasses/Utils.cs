using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{

    //We could put all functions we would want to access in the whole project here. Like math calculations. Just testing for fun :D

    public static Vector3 GetRandomDirNoY()
    {
        return new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }
}
