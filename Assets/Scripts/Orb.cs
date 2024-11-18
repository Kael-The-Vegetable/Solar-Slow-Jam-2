using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Events;


public struct LineHit
{
    public RaycastHit RaycastHit;
    public bool IsHit;
    public Vector3 Point;
}

public class Orb : MonoBehaviour
{
    /// <summary>
    /// called when the orb lost the light of sight to the sky
    /// </summary>
    public UnityEvent<Orb> OnLostSkyLOS;

    /// <summary>
    /// called when the orb gains line of sight to the sky
    /// </summary>
    public UnityEvent<Orb> OnGainSkyLOS;

    public bool ShowDebug = true;

    public float MaxLegDistance = 10;

    public int MaxLegs = 5;


    public bool OrbVisibleToSky;

    /// <summary>
    /// the transform that we are using to rotate the orb
    /// </summary>
    public Transform Rotator;

    // used to enable/disable the beam from the orb
    private bool _canCast;


    [SerializeField] private LineRenderer _line;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Rotator.rotation;
          


        _canCast = false;
        var skyCast = Physics.Raycast(transform.position, Vector3.up, 100);
        if (skyCast == false)
        {
            if (OrbVisibleToSky == false)
            {
                Debug.Log("Orb Gained LOS to the sky");
                OrbVisibleToSky = true;
                // we just gained LOS to the sky
                OnGainSkyLOS.Invoke(this);
            }

            OrbVisibleToSky = true;
            _canCast = true;
        }
        else
        {
            if (OrbVisibleToSky == true)
            {
                Debug.Log("Orb lost LOS to the sky");
                // we just lost LOS to the sky
                OrbVisibleToSky = false;
                OnLostSkyLOS.Invoke(this);
            }
        }

        if (_canCast)
        {
            var hits = CalculateLine(MaxLegDistance, MaxLegs);
            var points = new Vector3[hits.Length];

            BounceLine(hits);
        }
    }


    /// <summary>
    /// we take in the full line that has already been calculated and determine if we should end the line early
    /// </summary>
    public void BounceLine(LineHit[] line)
    {
        List<LineHit> newLine = new();

        for (int i = 0; i < line.Length; i++)
        {
            var section = line[i];
            newLine.Add(section);
            if (section.IsHit)
            {
                // the start of this line was a collision with an object

                var hitObject = section.RaycastHit.collider.gameObject;


                var surface = hitObject.GetComponent<Surface>();

                // only reflects on objects with a surface
                if (surface is null)
                {
                    break;
                }

                surface.OnOrbHit.Invoke(surface, this);


                if (surface.AllowOrbReflection == false)
                {
                    break;
                }
            }
        }


        var points = new Vector3[newLine.Count];

        for (int i = 0; i < newLine.Count; i++)
        {
            points[i] = newLine[i].Point;
        }

        DrawLine(points);
    }


    public void DrawLine(Vector3[] points)
    {
        _line.positionCount = points.Length;
        _line.SetPositions(points);
    }

    public RaycastHit? CastOrb(float maxLegDistance)
    {
        var pos = transform.position;
        var direction = transform.forward;
        Ray ray = new Ray(pos, direction);


        if (Physics.Raycast(ray, out var hit, maxLegDistance))
        {
            return hit;
        }

        return null;
    }

    /// <summary>
    /// returns all the points hit from a reflected ray
    /// </summary>
    /// <param name="maxLegDistance"> the raycast distance</param>
    /// <param name="maxLegs">the max number of bounces</param>
    /// <returns></returns>
    public LineHit[] CalculateLine(float maxLegDistance, int maxLegs)
    {
        var objectsHit = new List<LineHit>();
        var pos = transform.position;
        var direction = transform.forward;

        //first point
        var firstPoint = new LineHit()
            { IsHit = false, Point = pos };
        objectsHit.Add(firstPoint);

        for (int i = 1; i < maxLegs; i++)
        {
            Ray ray = new Ray(pos, direction);


            if (Physics.Raycast(ray, out var hit, maxLegDistance))
            {
                if (ShowDebug)
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.cyan, 10);
                }

                var lineHit = new LineHit()
                {
                    IsHit = true,
                    RaycastHit = hit,
                    Point = hit.point
                };
                objectsHit.Add(lineHit);


                pos = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
            }
            else
            {
                //add end point

                var finalPoint = new LineHit()
                {
                    IsHit = false,
                    Point = pos
                };

                objectsHit.Add(finalPoint);

                if (ShowDebug)
                {
                    Debug.DrawLine(pos, pos + direction.normalized * maxLegDistance, Random.ColorHSV(), 10);
                }

                break;
            }
        }

        return objectsHit.ToArray();
    }
}