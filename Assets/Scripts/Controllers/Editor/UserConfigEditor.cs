using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Controllers.Editor
{
    [CustomEditor(typeof(UserDataHandler))]
    
    // ReSharper disable once RequiredBaseTypesIsNotInherited
    public class UserConfigEditor : ConfigEditor
    {
        
        [MenuItem("Tools/Configs/UsersConfig")]
        private static async void ShowWindow()
        {
            var w = await GetWindow<UserConfigEditor>();
            w.LoadConfig();
            w.Show();
        }

        [ShowInInspector]
        private UserDataHandler _userDataHandler = new UserDataHandler();

        
        [Button]
        [ButtonGroup("Changes")]
        public void LoadConfig()
        {
            CreateDiContainerWithRegisteredDependencies();
            DiContainer.InjectDependencies(_userDataHandler);
            _userDataHandler.LoadUserData();
        }
        
        [Button]
        [ButtonGroup("Changes")]
        public new void SaveChanges()
        {
            CreateDiContainerWithRegisteredDependencies();
            DiContainer.InjectDependencies(_userDataHandler);
            _userDataHandler.SaveUserData();
        }
    }
}