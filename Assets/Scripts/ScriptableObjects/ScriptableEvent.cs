using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Event", menuName = "SO/Events/Event", order = 0)]
    public class ScriptableEvent : ScriptableObject
    {
        // we dont want to register this in the editor because we might accidentally add subscribers from a level not loaded?
        [HideInInspector] public UnityEvent OnEvent;
        // we can even add unity events that pass data for things that are common...
        // like sending the click location as a vector3
        // in this case its only triggering the event nothing else
    }

    [CreateAssetMenu(fileName = "Vector3Event", menuName = "SO/Events/VectorEvent", order = 0)]
    public class ScriptableVector3Event : ScriptableObject
    {
        [HideInInspector] public UnityEvent<Vector3> OnEvent;
    }
}