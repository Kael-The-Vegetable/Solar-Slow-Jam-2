using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class OrbReceiver : MonoBehaviour
    {
        public bool ConsumeOrb = false;

        public UnityEvent<Orb> OnOrbHit;
    }
}