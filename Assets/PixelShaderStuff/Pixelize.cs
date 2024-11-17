using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Pixelize : MonoBehaviour
{ // Apply this to the camera to pixelize when playing
    [SerializeField] private Camera _originalCamera;

    private void Awake()
    {
        Instantiate(new GameObject("Absorbing Data", typeof(Camera)), transform);
    }

}
