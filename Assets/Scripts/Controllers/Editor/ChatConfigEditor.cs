using Sirenix.OdinInspector;
using UnityEditor;

namespace Controllers.Editor
{
    public class ChatConfigEditor : ConfigEditor
    {
        [MenuItem("Tools/Configs/ChatConfig")]
        private static void ShowWindow()
        {
            var w = OpenWindow<ChatConfigEditor>();
            w.LoadConfig();
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
            _chatConfigController.SetNewMessageIndex();
        }
    }
}