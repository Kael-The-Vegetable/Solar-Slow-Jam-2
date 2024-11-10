using UnityEditor;
using UnityEngine;

public class MenuEditor : MonoBehaviour
{
    static GameObject menuPrefab;

    [MenuItem("GameObject/UI/Menu")]
    static void CreateMenu()
    {
        var obj = Instantiate(menuPrefab);
        obj.name = menuPrefab.name;
        Undo.RegisterCreatedObjectUndo(obj, obj.name);
    }

    [MenuItem("GameObject/UI/Menu", true)]
    static bool CreateMenuValidate()
    {
        bool valid = false;
        menuPrefab = Resources.Load("Menu") as GameObject;

        if (menuPrefab != null)
        {
            valid = true;
        }

        return valid;
    }
}
