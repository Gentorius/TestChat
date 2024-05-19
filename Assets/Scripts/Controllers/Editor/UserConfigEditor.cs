using UnityEditor;

namespace Controllers.Editor
{
    [CustomEditor(typeof(UserConfigController))]
    
    public class UserConfigEditor : ConfigEditor
    {
        [MenuItem("Tools/Configs/UsersConfig")]
        private static void ShowWindow() 
            => OpenWindow<UserConfigEditor>();
        public override void OnInspectorGUI()
        {
            UserConfigController userConfigController = (UserConfigController)target;
            userConfigController.UserStorage = EditorGUILayout.Foldout(true, "Users");
        }
    }
}