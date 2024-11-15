using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class Surface : MonoBehaviour
{
    /// <summary>
    /// called every frame the orb hits this surface
    /// </summary>
    [FormerlySerializedAs("OnOrbStay")] public UnityEvent<Surface, Orb> OnOrbHit;


    public bool AllowOrbReflection = false;


    // only support for one orb currently

    // a potential shitty way to check if the colliding orb is still colliding with us "staying"
}