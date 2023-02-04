using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    private float floatSpeed;
    [SerializeField]
    private float floatDamper;
    private Camera theCam;
    private float startzPos;
    private float floatInc;

    private bool isOn;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
        startzPos = transform.position.z;
        floatInc = 0;
        // floatSpeed = 2.0f;
        // floatDamper = 10.0f;

        isOn = false;

        sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(isOn) {
            transform.position = new Vector3(transform.position.x, transform.position.y, startzPos + Mathf.Cos(floatInc) / floatDamper);

            floatInc += floatSpeed * Time.deltaTime;
        }
    }

    public void TurnOn() {
        // Turn on the billboard message
        isOn = true;
        sprite.enabled = true;
    }

    public void TurnOff() {
        // Turn off the billboard message
        isOn = false;
        sprite.enabled = false;
    }

    public void SetStartPos(float zPos) {
        startzPos = zPos;
    }
}
