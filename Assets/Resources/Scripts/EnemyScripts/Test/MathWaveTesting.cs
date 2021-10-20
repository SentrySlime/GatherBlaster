using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class MathWaveTesting : MonoBehaviour
{
    public enum Functions
    {
        Wave,
        MultiWave,
        MorphingWave,
        Ripple,
        Circle,
        Sphere
    }

    public Functions function;
    public bool includeZ = false;
    public bool vector = true;

    [SerializeField] Transform pointPrefab;
    Transform[] points;

    

    [Header("General")]
    [SerializeField] int numOfCubes = 10;
    [SerializeField] float size = 4f;

    [Header("Ripple")]
    [SerializeField, Range(1f, 15f)] float sizeOfRippleEffect = 10f;
    [SerializeField, Range(1f, 6f)] float firstWaveSize = 1f;
    [SerializeField, Range(1f, 10f)] float speed = 1f;

    [Header("Sphere")]
    [SerializeField, Range(1f, 20f)] public float sphereSize = 5f;

    private void Awake()
    {
        if(includeZ)
            CreatePointsWithZ();
        else
            CreatePointsNoZ();
    }

    private void Update()
    {
        UpdatePattern();
    }
    private void CreatePointsNoZ()
    {
        points = new Transform[numOfCubes];
        float step = size / numOfCubes;
        Vector3 scale = Vector3.one * step;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i] = Instantiate(pointPrefab, Vector3.zero, Quaternion.identity);
            point.localScale = scale;
        }
    }
    private void CreatePointsWithZ()
    {
        points = new Transform[numOfCubes * numOfCubes];
        float step = size / numOfCubes;
        Vector3 scale = Vector3.one * step;
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i] = Instantiate(pointPrefab);
            point.localScale = scale;
        }
    }
    private void UpdatePattern()
    {
        float step = size / numOfCubes;
        float v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == numOfCubes)
            {
                x = 0;
                z++;
                v = (z + 0.5f) * step - 1f;
            }
            float u = (x + 0.5f) * step - 1f;

            Transform point = points[i];
            Vector3 position = point.localPosition;
            //position.y = MultiWave(position.x, position.z, Time.time);
            if(vector)
                position = ApplyFunctionVector(u, v, Time.time, sizeOfRippleEffect, firstWaveSize, speed);
            else
                position.y = ApplyFunction(u, v, Time.time, sizeOfRippleEffect, firstWaveSize, speed);
            point.localPosition = position;
        }
    }
    private Vector3 ApplyFunctionVector(float x, float z, float t, float sizeEffect, float firstWaveSize, float speed)
    {
        Vector3 pos = Vector3.zero;

        switch (function)
        {
            case Functions.Wave:
                pos = Wave(x, z, t);
                break;

            case Functions.MultiWave:
                Debug.Log("not implemented");
                break;

            case Functions.MorphingWave:
                Debug.Log("not implemented");
                break;

            case Functions.Ripple:
                Debug.Log("not implemented");
                break;

            case Functions.Circle:
                pos = Circle(x, z, t);
                break;

            case Functions.Sphere:
                pos = Sphere(x, z, t, sphereSize);
                break;
        }

        return pos;
    }
    private float ApplyFunction(float x, float z, float t, float sizeEffect, float firstWaveSize, float speed)
    {
        float y = 0f;
        //Vector3 pos;
        switch(function)
        {
            case Functions.Wave:
                //pos = Wave(x, z, t);
                break;

            case Functions.MultiWave:
                y = MultiWave(x, z, t);
                break;

            case Functions.MorphingWave:
                y = MorphingWave(x, z, t);
                break;

            case Functions.Ripple:
                y = Ripple(x, z, t, sizeEffect, firstWaveSize, speed);
                break;
        }

        return y;
    }
    public static Vector3 Wave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;
        return p;

        //float y = Sin(PI * (x + z + t));
        //return y;
    }
    public static float MultiWave(float x, float z, float t)
    {
        float y = Sin(PI * (x + t));
        if(z != 0)
            y += 0.5f * Sin(2f * PI * (z + t));
        else
            y += 0.5f * Sin(2f * PI * (x + t));
        return y * (2f / 3f);
    }
    public static float MorphingWave(float x, float z, float t)
    {
        float y = Sin(PI * (x + 0.5f * t));
        y += 0.5f * Sin(2f * PI * (x + t));
        return y * (2f / 3f);
    }
    public static float Ripple(float x, float z, float t, float sizeEffect, float firstWaveSize, float speed)
    {
        float d;
        if (z != 0)
           d  = Sqrt(x * x + z * z);
        else
           d = Abs(x);

        float y = Sin(PI * (4f * d - (t / firstWaveSize * speed) )) * firstWaveSize;
        return y / (1f + sizeEffect * d);
    }
    public static Vector3 Circle (float u, float v, float t)
    {
        Vector3 p;
        p.x = Sin(PI * u);
        p.y = 0f;
        p.z = Cos(PI * u);
        return p;
    }
    public static Vector3 Sphere(float u, float v, float t, float sphereSize)
    {
        float r = .5f + .5f * sphereSize /*Sin(PI * sphereSize)*/;
        float s = r * Cos(.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * 0.5f * v);
        p.z = s * Cos(PI * u);
        return p;
    }
}
