using TMPro.Examples;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[CustomEditor(typeof(CameraController))]
public class CameraControllerCI : Editor
{
    private CameraController _cam; 

    bool showCameraVars;

    private void OnEnable()
    {
        _cam = (CameraController)target;
    }

    public override void OnInspectorGUI()
    {
        showCameraVars = EditorGUILayout.Foldout(showCameraVars, "Camara Variables");

        if (showCameraVars)
        {
            // sensibilidad como slider
            //_cam.sensibilidad = EditorGUILayout.Slider("Sensibilidad", _cam.sensibilidad, 0f, 17f);

            // smoothness como float
        //    _cam.smoothness = EditorGUILayout.FloatField("Smoothness", _cam.smoothness);

        //    // angulos verticales como floats
        //    _cam.maxVerticalAngle = EditorGUILayout.FloatField("Max Vertical Angle", _cam.maxVerticalAngle);
        //    _cam.minVerticalAngle = EditorGUILayout.FloatField("Min Vertical Angle", _cam.minVerticalAngle);
        //}

        //// posicion del mouse (no se modifican)
        //GUI.enabled = false;
        //EditorGUILayout.Vector3Field("Mouse Position", _cam.mouseScaledPos);
        //GUI.enabled = true;

        //// bool para estado del cursor
        //_cam.customLockMode = EditorGUILayout.Toggle("Custom Cursor State at Start", _cam.customLockMode);

        //// si esta activado, mostrar enum para CursorLockMode
        //if (_cam.customLockMode)
        //{
        //    _cam.lockMode = (CursorLockMode)EditorGUILayout.EnumPopup("Cursor Lock Mode", _cam.lockMode);
        }

        // guardar cambios 
        serializedObject.ApplyModifiedProperties();

    }
}

#endif
