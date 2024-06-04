using System.Collections.Generic;
using Models;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Controllers.Editor
{
    [CustomEditor(typeof(UserConfigController))]
    
    public class UserConfigEditor : ConfigEditor
    {
        
        [MenuItem("Tools/Configs/UsersConfig")]
        private static void ShowWindow() 
            => OpenWindow<UserConfigEditor>();
        
        [SerializeField]
        private UserConfigController UserConfigController;

        [Button]
        [ButtonGroup("Changes")]
        public void SaveChanges()
        {
            UserConfigController.SaveToJson();
        }
        
        [Button]
        [ButtonGroup("Changes")]
        public void LoadChanges()
        {
            UserConfigController.LoadFromJson();
        }
    }
}