using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
public class PixelizeEditor : MonoBehaviour
{
    private static readonly Camera cam = Camera.main;

    private static readonly string dataCamPath = "AbsorbingData";
    private static readonly GameObject dataCam = Resources.Load(dataCamPath) as GameObject;

    private static readonly string canvasRawImage = "CameraCanvas";
    private static readonly GameObject canvas = Resources.Load(canvasRawImage) as GameObject;

    [MenuItem("Pixelize/Setup")]
    static void Setup()
    {
        for (int i = 0; i < cam.transform.childCount; i++)
        {
            Transform child = cam.transform.GetChild(i);
            if (child.name == "AbsorbingData(Clone)" || child.name == "CameraCanvas(Clone)")
            { // delete child
                DestroyImmediate(child.gameObject);
                i--;
            }
        }

        Instantiate(dataCam, cam.transform);
        Instantiate(canvas, cam.transform);

    }
}

#endif