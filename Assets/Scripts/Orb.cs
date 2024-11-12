using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public bool ShowDebug = true;

    public float MaxLegDistance = 10;

    public int MaxLegs = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateLine(5, 10);
        }
    }


    public void DrawnSolarLine()
    {
        var hits = CalculateLine(MaxLegDistance, MaxLegs);


        foreach (var hit in hits)
        {
            
        }
    }

    /// <summary>
    /// returns all the points hit from a reflected ray
    /// </summary>
    /// <param name="maxLegDistance"> the raycast distance</param>
    /// <param name="maxLegs">the max number of bounces</param>
    /// <returns></returns>
    public RaycastHit[] CalculateLine(float maxLegDistance, int maxLegs)
    {
        var objectsHit = new RaycastHit[maxLegs + 1];
        var pos = transform.position;
        var direction = transform.forward;

        for (int i = 0; i < maxLegs; i++)
        {
            Ray ray = new Ray(pos, direction);


            if (Physics.Raycast(ray, out var hit, maxLegDistance))
            {
                if (ShowDebug)
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.blue, 10);
                }

                objectsHit[i] = hit;
                if (ShowDebug)
                {
                    Debug.DrawLine(pos, hit.point, Color.blue, 10);
                }

                pos = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
            }
            else
            {
                if (ShowDebug)
                {
                    Debug.DrawLine(pos, pos + direction.normalized * maxLegDistance, Color.red, 10);
                }

                break;
            }
        }

        return objectsHit;
    }
}