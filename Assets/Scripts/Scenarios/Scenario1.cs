using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario1 : MonoBehaviour
{
    private void Start() {
        transform.localScale = Vector3.zero;
    }
    public void playAnimation() 
    {
        // for movement
        //transform.LeanMoveLocal(new Vector3(0, 0, 0), 1);
        transform.LeanScale(Vector3.one, 0.4f);
    }

    public void Close() {
        transform.LeanScale(Vector3.zero, 0.3f).setEaseInBack();
    }
}
