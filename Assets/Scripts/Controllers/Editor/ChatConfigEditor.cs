using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Controllers.Editor
{
    public class ChatConfigEditor : ConfigEditor
    {
        [MenuItem("Tools/Configs/ChatConfig")]
        private static async void ShowWindow()
        {
            var window = await GetWindow<ChatConfigEditor>();
            await window.LoadConfig();
            window.Show();
        }
        
        [ShowInInspector]
        private ChatDataHandler _chatDataHandler;

        
        [Button]
        [ButtonGroup("Changes")]
        public async Task LoadConfig()
        {
            _chatDataHandler ??= new ChatDataHandler();
            CreateDiContainerWithRegisteredDependencies();
            DiContainer.InjectDependencies(_chatDataHandler);
            _chatDataHandler.LoadHistory();
        }
        
        [Button]
        [ButtonGroup("Changes")]
        public new void SaveChanges()
        {
            CreateDiContainerWithRegisteredDependencies();
            DiContainer.InjectDependencies(_chatDataHandler);
            _chatDataHandler.SaveChatHistory();
        }
        
        [Button]
        [ButtonGroup("Radical Changes")]
        public void ClearConfig()
        {
            _chatDataHandler.ClearChatHistory();
            SaveChanges();
        }
    }
}