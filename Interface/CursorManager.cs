using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform rectTransform;
    private float bound = 100;
    private bool moveCursor = false;
    private void Update() {
        if (Input.GetMouseButton(0)) {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Input.GetMouseButtonDown(0)) {
                if(Physics.Raycast(ray, out hit)) {
                    if (hit.transform.gameObject == gameObject) {
                        scrollRect.enabled = false;
                        moveCursor = true;
                    }
                }
            }

            if(moveCursor) {
                if(Physics.Raycast(ray, out hit)) {
                    transform.position = new Vector3 (hit.point.x, hit.point.y, transform.position.z);
                }
            }
            
        }

        if(Input.GetMouseButtonUp(0))
        {
           scrollRect.enabled = true; 
           moveCursor = false;
        }

        if (rectTransform.localPosition.x < -bound) {
            rectTransform.localPosition = new Vector3 (-bound, rectTransform.localPosition.y, rectTransform.localPosition.z);
        }
        if (rectTransform.localPosition.x > bound) {
            rectTransform.localPosition = new Vector3 (bound, rectTransform.localPosition.y, rectTransform.localPosition.z);
        }
        if (rectTransform.localPosition.y < -bound) {
            rectTransform.localPosition = new Vector3 (rectTransform.localPosition.x, -bound, rectTransform.localPosition.z);
        }
        if (rectTransform.localPosition.y > bound) {
            rectTransform.localPosition = new Vector3 (rectTransform.localPosition.x, bound, rectTransform.localPosition.z);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition));
    }
}
