using UnityEditor;

namespace Controllers.Editor
{
    public class ConfigEditor : UnityEditor.Editor
    {
        protected static void OpenWindow<TY>() where TY : ConfigEditor
        {
            var editor = GetWindow<TY>();
        }
    }
}