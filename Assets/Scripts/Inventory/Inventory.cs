using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameCodeEditor;

public class Inventory : MonoBehaviour
{
    public Canvas canvas;
    public CodeEditorTheme[] themes;
	public CodeEditor editor;
    // Update is called once per frame
    void Update()
    {
        // Toggle inventory UI
        if(Input.GetKeyDown(KeyCode.I)) {
            canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy);
        }

    }

    public void setTheme(int index) {
        editor.editorTheme = themes[index];
    }
}
