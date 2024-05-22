using UnityEditor;

namespace Controllers.Editor
{
    [CustomEditor(typeof(UserConfigController))]
    
    public class UserConfigEditor : ConfigEditor
    {
        [MenuItem("Tools/Configs/UsersConfig")]
        private static void ShowWindow() 
            => OpenWindow<UserConfigEditor>();
        
        
    }
}