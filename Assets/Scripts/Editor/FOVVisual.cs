using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FOV))]
public class FOVVisual : Editor
{
    private void OnSceneGUI()
    {
        FOV fov = (FOV)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, new Vector3(0,0,1), Vector2.up, 360, fov.viewRadius);
    }
}
