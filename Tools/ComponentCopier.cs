using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class ComponentCopier : EditorWindow
{
    public MonoBehaviour origin;
    public MonoBehaviour target;

    [MenuItem("ReMix-Factory/Tools/Copy Component Values")]
    public static void OpenWindow()
    {
        GetWindow<ComponentCopier>();
    }

    void OnGUI()
    {
        origin = EditorGUILayout.ObjectField( "Origin",origin, typeof(MonoBehaviour), true ) as MonoBehaviour;
        target = EditorGUILayout.ObjectField( "Target",target, typeof(MonoBehaviour), true ) as MonoBehaviour;

        if (GUILayout.Button("Copy"))
        {
            CopyComponentValues(origin, target);
        }
    }

    public static void CopyComponentValues( MonoBehaviour origin, MonoBehaviour target)
    {
        System.Type origType = origin.GetType();
        System.Type targetType = target.GetType();

        foreach( FieldInfo origInfo in origType.GetFields( BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ) )
        {
            FieldInfo targetInfo = targetType.GetField(origInfo.Name);
            if (targetInfo != null)
            {
                Debug.Log("Copy Field: "+targetInfo.Name);
                targetInfo.SetValue( target, origInfo.GetValue( origin ) );
            }
        }
    }
}
