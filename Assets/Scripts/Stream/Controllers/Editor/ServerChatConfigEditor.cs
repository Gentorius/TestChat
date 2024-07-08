using Controllers.Editor;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Stream.Controllers.Editor
{
    public class ServerChatConfigEditor : ConfigEditor
    {
        [MenuItem("Tools/ClientConfigs/ChatConfig")]
        private static void ShowWindow()
        {
            var window = OpenWindow<ChatConfigEditor>();
            window.LoadConfig();
        }
        
        [ShowInInspector]
        private ServerChatConfigController _chatConfigController = new ServerChatConfigController();

        
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