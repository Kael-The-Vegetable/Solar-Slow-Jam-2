using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Surface : MonoBehaviour
    {
        /// <summary>
        /// this is called every frame the orb hit this surface
        /// </summary>
        public UnityEvent<Orb> OnOrbHit;

        public bool AllowOrbReflection = false;
    }
}