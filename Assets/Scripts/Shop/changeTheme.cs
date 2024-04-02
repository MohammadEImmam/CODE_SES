using UnityEngine;
using InGameCodeEditor;

class ChangeTheme : MonoBehaviour {
    public CodeEditor editor;
    public CodeEditorTheme theme;

    void setTheme() {
        editor.EditorTheme = theme;
    }

}
