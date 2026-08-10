using UnityEditor;

[CustomEditor(typeof(ExampleStateMachine))]
public class ExampleStateControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ExampleStateMachine controller = (ExampleStateMachine)target;

        // Check if the current state exists
        if (controller.currentBaseState != null)
        {
            EditorGUILayout.LabelField("Current Base State", controller.currentBaseState.GetType().Name);
        }
        else
        {
            EditorGUILayout.LabelField("Current Base State", "None");
        }
        
        if (controller.currentOverlayState != null) // We may want to show overlay + base, and base would be replaced with action
        {
            EditorGUILayout.LabelField("Current Overlay State", controller.currentOverlayState.GetType().Name);
        }
        else
        {
            EditorGUILayout.LabelField("Current Overlay State", "None");
        }
        
        if (controller.currentActionState != null)
        {
            EditorGUILayout.LabelField("Current Action State", controller.currentActionState.GetType().Name);
        }
        else
        {
            EditorGUILayout.LabelField("Current Action State", "None");
        }

        // The rest of your inspector drawing code
        DrawDefaultInspector();
    }
}