using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bud : MonoBehaviour
{
    [Header("Buds")]
    [SerializeField]
    private bool isRootBud = false;
    [SerializeField]
    private Bud rootBud;
    [SerializeField]
    private GameObject budPrefab;

    [Header("Camera")]
    private Camera mainCamera;

    [Header("Root")]
    [SerializeField]
    private float maxDistanceFromRoot;
    [SerializeField]
    private float distanceIncreaseAmt;
    [SerializeField]
    private float newBudDistance;
    [SerializeField]
    private GameObject root;
    [SerializeField]
    private Color endColor;
    private SpringJoint rootTether;
    private LineRenderer lineRenderer;
    private bool carryingNewRoot;

    private void Awake() {
        rootTether = GetComponent<SpringJoint>();
        rootTether.maxDistance = maxDistanceFromRoot;
        rootTether.connectedBody = root.GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.green;
        mainCamera = Camera.main;
        mainCamera.gameObject.GetComponent<CameraController>().AddBud(this);

        carryingNewRoot = false;

        if(isRootBud) {
            rootBud = this;
        }
    }

    private void Update(){
        float distanceFromRoot = Vector3.Magnitude(transform.position - root.transform.position);
        lineRenderer.endColor = Color.Lerp(Color.green, endColor, distanceFromRoot/maxDistanceFromRoot);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.SetPosition(0, root.transform.position);
    }

    public void CreateBud() {
        GameObject newBud = Instantiate(budPrefab, transform.position + transform.forward * 2, transform.rotation);
        newBud.GetComponent<Bud>().SetRoot(GetComponent<Rigidbody>());
        newBud.GetComponent<Bud>().SetMaxDistance(newBudDistance);
        newBud.GetComponent<Bud>().SetRootBud(rootBud);
        rootBud.IncreaseMaxDistance(distanceIncreaseAmt);
    }

    public void SetRoot(Rigidbody body) {
        root = body.gameObject;
        rootTether.connectedBody = body;
    }

    public void SetRootBud(Bud root) {
        rootBud = root;
    }

    public void SetMaxDistance(float maxDistance) {
        maxDistanceFromRoot = maxDistance;
        rootTether.maxDistance = maxDistanceFromRoot;
    }

    public void IncreaseMaxDistance(float increase) {
        rootTether.maxDistance += increase;
    }

    public bool GetisRootBud(){
        return isRootBud;
    }

    public Bud GetRootBud() {
        return rootBud;
    }

    public GameObject GetRoot() {
        return root;
    }

    public void SetCarryingNewRoot(bool carrying) {
        carryingNewRoot = carrying;
    }

    public bool GetCarryingNewRoot() {
        return carryingNewRoot;
    }
}
