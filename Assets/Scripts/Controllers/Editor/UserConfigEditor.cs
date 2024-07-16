using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Controllers.Editor
{
    [CustomEditor(typeof(UserConfigController))]
    
    public class UserConfigEditor : ConfigEditor
    {
        
        [MenuItem("Tools/ClientConfigs/UsersConfig")]
        private static void ShowWindow()
        {
            var w = OpenWindow<UserConfigEditor>();
            w.LoadConfig();
        }

        [ShowInInspector]
        private UserConfigController _userConfigController = new UserConfigController();

        
        [Button]
        [ButtonGroup("Changes")]
        public void LoadConfig()
        {
            _userConfigController.LoadFromJson();
        }
        
        [Button]
        [ButtonGroup("Changes")]
        public new void SaveChanges()
        {
            _userConfigController.SaveToJson();
        }
    }
}