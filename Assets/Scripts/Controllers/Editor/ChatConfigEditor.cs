using Sirenix.OdinInspector;
using UnityEditor;

namespace Controllers.Editor
{
    public class ChatConfigEditor : ConfigEditor
    {
        [MenuItem("Tools/ClientConfigs/ChatConfig")]
        private static void ShowWindow()
        {
            var window = OpenWindow<ChatConfigEditor>();
            window.LoadConfig();
        }
        
        [ShowInInspector]
        private ChatDataHandler _chatDataHandler = new ChatDataHandler();

        
        [Button]
        [ButtonGroup("Changes")]
        public void LoadConfig()
        {
            _chatDataHandler.LoadHistory();
        }
        
        [Button]
        [ButtonGroup("Changes")]
        public new void SaveChanges()
        {
            _chatDataHandler.SaveToJson();
        }
    }
}