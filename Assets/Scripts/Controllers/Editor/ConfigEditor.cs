using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace Controllers.Editor
{
    public abstract class ConfigEditor : OdinEditorWindow
    {
        protected static TY OpenWindow<TY>() where TY : ConfigEditor
        {
            var editor = GetWindow<TY>();
            editor.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
            editor.Show();

            return editor;
        }
    }
}