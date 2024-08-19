using System.Threading.Tasks;
using Interface;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using Utility.DependencyInjection;

namespace Controllers.Editor
{
    public abstract class ConfigEditor : OdinEditorWindow
    {
        private DIServiceRegistry _serviceRegistry;
        protected DIContainer DiContainer;
        
        protected async static Task<TY> GetWindow<TY>() where TY : ConfigEditor
        {
            var editor = EditorWindow.GetWindow<TY>();
            editor.titleContent = new GUIContent(typeof(TY).Name);
            editor.minSize = new Vector2(300, 300);
            editor.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
            editor.SetAntiAliasing(1);

            return editor;
        }
        
        protected void CreateDiContainerWithRegisteredDependencies()
        {
            if (_serviceRegistry == null)
            {
                _serviceRegistry = new DIServiceRegistry();
                _serviceRegistry.RegisterService<IDataHandler>(new DataHandler());
            }

            if (DiContainer != null) return;
            DiContainer = new DIContainer(_serviceRegistry);
            DiContainer.RegisterServices();
        }
    }
}