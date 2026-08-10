using UnityEditor;

[CustomEditor(typeof(ExampleStateMachine))]
public class ExampleStateControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ExampleStateMachine controller = (ExampleStateMachine)target;

        // Check if the current state exists
        if (controller.currentState != null)
        {
            EditorGUILayout.LabelField("Current Base State", controller.currentState.GetType().Name);
        }
        else
        {
            EditorGUILayout.LabelField("Current Base State", "None");
        }

        // The rest of your inspector drawing code
        DrawDefaultInspector();
    }
}