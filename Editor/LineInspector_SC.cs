using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Line_SC))]
public class LineInspector_SC : Editor
{
    private void OnSceneGUI()
    {
        // creation variable of type Line_SC
        Line_SC line = target as Line_SC;

        //Information and trasformation of transform local space to position world space
        Transform handleTransform = line.transform;
        Quaternion handleRotation = handleTransform.rotation;
        Vector3 p0 = handleTransform.TransformPoint(line.p0);
        Vector3 p1 = handleTransform.TransformPoint(line.p1);

        //Print Line on screen
        Handles.color = Color.white;
        Handles.DrawLine(p0,p1);
        Handles.DoPositionHandle(p0, handleRotation);
        Handles.DoPositionHandle(p1, handleRotation);

        // determine the current mode and set our rotation accordingly
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
        handleTransform.rotation : Quaternion.identity;

        //recalculate position if one of the two points is change
        EditorGUI.BeginChangeCheck();
        p0 = Handles.DoPositionHandle(p0, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            line.p0 = handleTransform.InverseTransformPoint(p0);
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line);
        }
        EditorGUI.BeginChangeCheck();
        p1 = Handles.DoPositionHandle(p1, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            line.p1 = handleTransform.InverseTransformPoint(p1);
            Undo.RecordObject(line, "Move Point");
			EditorUtility.SetDirty(line);
        }
    }
}
