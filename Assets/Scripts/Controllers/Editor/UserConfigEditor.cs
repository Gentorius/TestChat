﻿using Sirenix.OdinInspector;
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
        private UserConfigController _userConfigController;

        
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