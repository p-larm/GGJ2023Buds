using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float cameraMovementOffset = 5f;
    private Vector2 movementVector;
    private List<Bud> buds;
    private PlayerMovement playerRef;

    private void Awake() {
        buds = new List<Bud>();
        movementVector = Vector2.zero;
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        AddBud(playerRef.GetComponent<Bud>());
    }

    private void LateUpdate(){
        Vector3 averagePos = GetAverageBudPositions();
        movementVector = playerRef.GetInputRef() * cameraMovementOffset;
        transform.position = Vector3.Lerp(transform.position, new Vector3(averagePos.x + movementVector.x, transform.position.y, averagePos.z + movementVector.y), 0.05f);
    }

    private Vector3 GetAverageBudPositions() {
        Vector3 positionSum = Vector3.zero;
        if(buds.Count == 0) {
            Debug.Log("buds is empty");
        } else {
            foreach(Bud bud in buds) {
                positionSum += bud.gameObject.transform.position;
            }
            return positionSum / buds.Count;
        }
        return Vector3.zero;
    }

    public void AddBud(Bud bud) {
        if(buds == null) {
            buds = new List<Bud>();
        }
        buds.Add(bud);
    }
}
