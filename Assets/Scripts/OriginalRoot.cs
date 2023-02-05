using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OriginalRoot : MonoBehaviour
{
    [SerializeField]
    private GameObject model;
    private SphereCollider sphereCollider;
    private Root thisRoot;

    private void Awake() {
        thisRoot = GetComponent<Root>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    public void GrowRoot(InputAction.CallbackContext context) {
        if(context.performed) {
            if(thisRoot.GetActivated()) {
                if(thisRoot.GetRootBud().GetCarryingNewRoot()) {
                    StartCoroutine(Growing());
                    thisRoot.GetRootBud().SetCarryingNewRoot(false);
                    thisRoot.GetRootBud().SetRoot(GetComponent<Rigidbody>());
                }
            }
        }
    }

    private IEnumerator Growing() {
        Vector3 target = model.transform.localScale * 2;
        float originalRadius = sphereCollider.radius;
        while(Vector3.Magnitude(target - model.transform.localScale) > 0.5f) {
            model.transform.localScale = Vector3.Lerp(model.transform.localScale, target, 0.01f);
            sphereCollider.radius += 2 * Time.deltaTime;
            yield return null;
        }
        sphereCollider.radius = originalRadius * 2;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
