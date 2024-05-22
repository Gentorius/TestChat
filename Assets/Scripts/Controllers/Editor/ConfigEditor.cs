using UnityEditor;

namespace Controllers.Editor
{
    public class ConfigEditor : EditorWindow
    {
        protected static TY OpenWindow<TY>() where TY : ConfigEditor
        {
            var editor = GetWindow<TY>();
            editor.Show();

            return editor;
        }
    }
}