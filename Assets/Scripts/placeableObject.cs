using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Instantiate.GameObject
// Parameters: Object original, Vector3 position, Quaternion rotation, Transform parent, 
public class placeableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeableObjects;
    private GameObject currentObject;
    private float mouseRotation;
    private int prefabIndex = -1;


    // Update is called once per frame
    void Update()
    {
            newPlaceableObject();

            // if the current object is null that means the place button
            // was clicked twice and the game object is destroyed
            if(currentObject != null) {
                MoveObjectWithMouse();
                MouseWheeleRotate();
                Clicked();
            }
            else {
                Camera cam = GameObject.Find("MainCamera").GetComponent<Camera>();
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool deleteableObject;

                if(Physics.Raycast(ray, out hit)) {
                    if(Input.GetMouseButtonDown(1)) {
                        deleteableObject = canPlayerDelete(hit);
                        if(deleteableObject)
                            Destroy(hit.transform.gameObject);
                    }
                }
            }
        
    }

    private void newPlaceableObject() {
        for(int i = 0; i < placeableObjects.Length; i++) {

            if(Input.GetKeyDown(KeyCode.Alpha1 + i)) {

                if(prefabIndex == i && currentObject != null) {
                    Destroy(currentObject);
                    prefabIndex = -1;
                }

                else {
                    if(currentObject != null)
                        Destroy(currentObject);

                    if(shopManager.isPurchased(i)) {
                        currentObject = Instantiate(placeableObjects[i]);
                        //currentObject.GetComponent<BoxCollider>().enabled = false;
                        prefabIndex = i;
                    }
                }
                break;
            }
        }
    }

    private void MoveObjectWithMouse() {
        Camera cam = GameObject.Find("MainCamera").GetComponent<Camera>();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if(Physics.Raycast(ray, out hit)){
            Vector3 hitPoint = hit.point;
            Vector3 hitNormal = hit.normal;

            // Adjust object position to align with the hit point and normal
            currentObject.transform.position = hitPoint; //+ hitNormal * (currentObject.transform.localScale.y / 2);
            currentObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitNormal);
        }
    }

    private void MouseWheeleRotate() {
        mouseRotation += Input.mouseScrollDelta.y * 10f;
        currentObject.transform.Rotate(0.0f, mouseRotation, 0.0f, Space.Self);
    }

    private void Clicked(){
        if(Input.GetMouseButtonDown(0))
            currentObject = null;
    } 

    private bool canPlayerDelete(RaycastHit hit) {
        GameObject obj = hit.transform.gameObject;

        if(obj.name.Contains("Floor"))
            return false;

        return true;
    }
}

