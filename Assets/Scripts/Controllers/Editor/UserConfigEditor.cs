using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Controllers.Editor
{
    [CustomEditor(typeof(UserDataHandler))]
    
    public class UserConfigEditor : ConfigEditor
    {
        
        [MenuItem("Tools/ClientConfigs/UsersConfig")]
        private static void ShowWindow()
        {
            var w = OpenWindow<UserConfigEditor>();
            w.LoadConfig();
        }

        [ShowInInspector]
        private UserDataHandler _userDataHandler = new UserDataHandler();

        
        [Button]
        [ButtonGroup("Changes")]
        public void LoadConfig()
        {
            _userDataHandler.LoadUserData();
        }
        
        [Button]
        [ButtonGroup("Changes")]
        public new void SaveChanges()
        {
            _userDataHandler.SaveUserData();
        }
    }
}