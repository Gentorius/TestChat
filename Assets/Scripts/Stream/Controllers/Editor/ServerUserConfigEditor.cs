using Controllers;
using Controllers.Editor;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Stream.Controllers.Editor
{
    [CustomEditor(typeof(UserConfigController))]
    
    public class ServerUserConfigEditor : ConfigEditor
    {
        
        [MenuItem("Tools/ClientConfigs/UsersConfig")]
        private static void ShowWindow()
        {
            var w = OpenWindow<UserConfigEditor>();
            w.LoadConfig();
        }

        [ShowInInspector]
        private ServerUserConfigController _userConfigController = new ServerUserConfigController();

        
        [Button]
        [ButtonGroup("Changes")]
        public void LoadConfig()
        {
            _userConfigController.LoadFromJson();
            _userConfigController.SetActiveUser();
        }
        
        [Button]
        [ButtonGroup("Changes")]
        public new void SaveChanges()
        {
            _userConfigController.SaveToJson();
            _userConfigController.SetActiveUser();
        }
    }
}