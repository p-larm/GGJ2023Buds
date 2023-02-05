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
        if (mainCamera != null)
        {
            mainCamera.gameObject.GetComponent<CameraController>().AddBud(this);
        }

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

    public GameObject CreateBud() {
        Vector3 randomDirection = new Vector3(Random.value, 0, Random.value).normalized;
        GameObject newBud = Instantiate(budPrefab, transform.position + randomDirection * 4, transform.rotation);
        newBud.GetComponent<Bud>().SetRoot(GetComponent<Rigidbody>());
        newBud.GetComponent<Bud>().SetMaxDistance(newBudDistance);
        newBud.GetComponent<Bud>().SetRootBud(rootBud);
        rootBud.IncreaseMaxDistance(distanceIncreaseAmt);
        return newBud;
    }

    public GameObject CreateBud(Vector3 spawnPoint) {
        GameObject newBud = Instantiate(budPrefab, spawnPoint, transform.rotation);
        newBud.GetComponent<Bud>().SetisRootBud(false);
        newBud.GetComponent<Bud>().SetRoot(GetComponent<Rigidbody>());
        newBud.GetComponent<Bud>().SetMaxDistance(newBudDistance);
        newBud.GetComponent<Bud>().SetRootBud(rootBud);
        rootBud.IncreaseMaxDistance(distanceIncreaseAmt);
        newBud.transform.parent = transform;
        return newBud;
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

    public void SetisRootBud(bool isRootBud) {
        this.isRootBud = isRootBud;
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

    public void Respawn(Vector3 respawnPoint) {
        Bud rootBud = GetRootBud();
        rootBud.gameObject.transform.position = respawnPoint;
        List<Bud> allBuds = mainCamera.GetComponent<CameraController>().GetList();

        foreach(Bud b in allBuds) {
            Vector3 randomDirection = new Vector3(Random.value, 0, Random.value).normalized;
            b.transform.position = rootBud.transform.position + randomDirection * 4;
        }
    }
}
