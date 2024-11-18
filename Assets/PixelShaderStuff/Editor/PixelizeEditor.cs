using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
public class PixelizeEditor : MonoBehaviour
{
    private static readonly Camera cam = Camera.main;

    private static readonly string renderTexturePath = "CameraInfo";
    private static readonly RenderTexture texture = Resources.Load(renderTexturePath) as RenderTexture;

    private static readonly string canvasRawImage = "CameraCanvas";
    private static readonly GameObject canvas = Resources.Load(canvasRawImage) as GameObject;

    [MenuItem("Pixelize/Setup")]
    static void Setup()
    {
        for (int i = 0; i < cam.transform.childCount; i++)
        {
            Transform child = cam.transform.GetChild(i);
            if (child.name == "Absorbing Data" || child.name == "CameraCanvas(Clone)")
            { // delete child
                DestroyImmediate(child.gameObject);
                i--;
            }
        }

        GameObject dataObj = new GameObject("Absorbing Data", typeof(Camera));
        dataObj.transform.parent = cam.transform;

        dataObj.transform.localPosition = Vector3.zero;
        dataObj.transform.localEulerAngles = Vector3.zero;
        dataObj.transform.localScale = Vector3.one;

        Camera dataCam = dataObj.GetComponent<Camera>();

        dataCam.fieldOfView = Camera.main.fieldOfView;
        dataCam.targetTexture = texture;
        
        GameObject canvasObj = Instantiate(canvas, cam.transform);

    }
}

#endif