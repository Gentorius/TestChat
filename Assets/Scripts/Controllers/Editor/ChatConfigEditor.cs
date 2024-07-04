using Sirenix.OdinInspector;
using UnityEditor;

namespace Controllers.Editor
{
    public class ChatConfigEditor : ConfigEditor
    {
        [MenuItem("Tools/Configs/ChatConfig")]
        private static void ShowWindow()
        {
            var window = OpenWindow<ChatConfigEditor>();
            window.LoadConfig();
        }
        
        [ShowInInspector]
        private ChatConfigController _chatConfigController = new ChatConfigController();

        
        [Button]
        [ButtonGroup("Changes")]
        public void LoadConfig()
        {
            _chatConfigController.LoadFromJson();
        }
        
        [Button]
        [ButtonGroup("Changes")]
        public new void SaveChanges()
        {
            _chatConfigController.SaveToJson();
        }
    }
}