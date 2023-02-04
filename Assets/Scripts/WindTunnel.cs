using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTunnel : MonoBehaviour
{
    [Header("Wind Force/Direction")]
    [SerializeField]
    private float windForce;
    [SerializeField] [Range(0,1)]
    private float windChangeAmt;
    [SerializeField]
    private List<Vector2> windDirections;
    private Vector2 targetDirection;
    [SerializeField]
    private Vector2 currentDirection;

    [Header("Particle Effects")]
    [SerializeField]
    private ParticleSystem loopEffect;
    [SerializeField]
    private ParticleSystem straightEffect;
    [SerializeField]
    private float emissionRateMult;
    private BoxCollider boxCollider;

    private bool active;

    [SerializeField]
    private List<PlayerMovement> budList;

    // Start is called before the first frame update
    void Start()
    {
        budList = new List<PlayerMovement>();
        active = true;
        boxCollider = GetComponent<BoxCollider>();
        StartCoroutine(WindDirection());
    }

    private void Update() {
        Vector3 force = new Vector3(currentDirection.x, 0, currentDirection.y) * windForce;
        Debug.Log(force.ToString());
        HandleEffects(force);

        foreach(PlayerMovement bud in budList) {
            bud.AddForce(force);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            Debug.Log("Player Enter");
            budList.Add(other.gameObject.GetComponent<PlayerMovement>());
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            Debug.Log("Player Exit");
            for(int i = 0; i < budList.Count; i++) {
                if(budList[i] == other.gameObject.GetComponent<PlayerMovement>()) {
                    budList.RemoveAt(i);
                    i = budList.Count;
                }
            }
        }
    }

    private IEnumerator WindDirection() {
        while(active) {
            foreach(Vector2 direction in windDirections) {
                targetDirection = direction;
                var loopShape = loopEffect.shape;
                var straightShape = straightEffect.shape;
                if(direction.x != 0) {
                    loopShape.scale = new Vector3(boxCollider.size.x, boxCollider.size.z, boxCollider.size.z);
                    straightShape.scale = new Vector3(boxCollider.size.x, boxCollider.size.z, boxCollider.size.z);
                } else {
                    loopShape.scale = new Vector3(boxCollider.size.z, boxCollider.size.x, boxCollider.size.x);
                    straightShape.scale = new Vector3(boxCollider.size.z, boxCollider.size.x, boxCollider.size.x);
                }
                while(Vector2.Angle(currentDirection, targetDirection) > 5f) {
                    currentDirection = Vector2.Lerp(currentDirection, targetDirection, windChangeAmt);
                    yield return null;
                }
                currentDirection = targetDirection;
                yield return new WaitForSeconds(5);
            }
        }
    }

    private void HandleEffects(Vector3 force) {
        // Handle Direction
        Vector3 direction = Vector3.Normalize(force);

        loopEffect.gameObject.transform.right = Vector3.Lerp(loopEffect.gameObject.transform.right, direction, 0.1f);
        loopEffect.gameObject.transform.Rotate(new Vector3(90, 0, 0), Space.Self);

/*             if(force.x < 0) {
                loopEffect.gameObject.transform.localScale *= -1;
                straightEffect.gameObject.transform.localScale *= 1;
            } else {
                loopEffect.gameObject.transform.localScale = -1f;
                straightEffect.gameObject.transform.localScale = -1f;
            } */

        straightEffect.gameObject.transform.eulerAngles = loopEffect.gameObject.transform.eulerAngles;


        // Handle Speed
        var loopEmission = loopEffect.emission;
        var straightEmission = straightEffect.emission;
        float emissionRate = Mathf.Abs(force.magnitude) * emissionRateMult;
        loopEmission.rateOverTime = emissionRate;
        straightEmission.rateOverTime = emissionRate;
    }
}
