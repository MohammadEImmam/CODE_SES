using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Instantiate.GameObject
// Parameters: Object original, Vector3 position, Quaternion rotation, Transform parent, 
public class placeableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeableObjects;
    private GameObject currentObject;
    private float mouseRotation;
    private int prefabIndex = -1;
    GameObject shopUI;
    Collider collider;

    void Update()
    {
        newPlaceableObject();

        // if the current object is null that means the place button
        // was clicked twice and the game object is destroyed
        if(currentObject != null) {
            Destroy(currentObject.GetComponent<BoxCollider>());
            Destroy(currentObject.GetComponent<MeshCollider>());
            MoveObjectWithMouse();
            MouseWheeleRotate();
            Clicked();
        }

        // deleting objects
        else {
            if(GameObject.Find("MainCamera") != null) {
            Camera cam = GameObject.Find("MainCamera").GetComponent<Camera>();
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool deleteableObject;

            // send raycast to mouse position in scene
            if(Physics.Raycast(ray, out hit)) {
                if(Input.GetMouseButtonDown(1)) {
                    
                    deleteableObject = canPlayerDelete(hit);
                    if(deleteableObject)                            
                        Destroy(hit.transform.gameObject);
                    }
                }
                }
            }
        
    }

    /* Checks to see if key pressed matches with a prefab. Then ckecks to see if the
        prefab has been purchased. If both are true then the prefab is set to current Object*/
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
                    print(i+4);
                    if(inventoryManager.getItem(i+4)) {
                        currentObject = Instantiate(placeableObjects[i]);
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

            // Adjust object position to align with the hit point and orientation
            currentObject.transform.position = hitPoint;
            currentObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitNormal);
        }
    }

    private void MouseWheeleRotate() {
        mouseRotation += Input.mouseScrollDelta.y * 10f;
        currentObject.transform.Rotate(0.0f, mouseRotation, 0.0f, Space.Self);
    }

    private void Clicked(){
        if(Input.GetMouseButtonDown(0)) {
            currentObject.AddComponent<BoxCollider>();
            currentObject.layer = 0;
            currentObject = null;
        }
    } 

    /* Function limits what objects can be deleted by players */
    private bool canPlayerDelete(RaycastHit hit) {
        GameObject obj = hit.transform.gameObject;

        if(obj.name.Contains("Floor"))
            return false;

        return true;
    }
}

