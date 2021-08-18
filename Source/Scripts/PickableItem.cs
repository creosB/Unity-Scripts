using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PickableItem : MonoBehaviour
{
    public PickUpChoice Type;
    public enum PickUpChoice
    {
        PickandThrow,
        PickandDrop,
        HoldPickandDrop
    }

    [Header("PickUp Settings")]

    [SerializeField]
    public float ThrowForce = 10;

    [SerializeField]
    public float PickupAbleDistance = 2f;
    private float DistanceToPlayer;
    private Rigidbody throwableRigidBody;
    public GameObject PlayerHand;
    public GameObject PickUpText;
    public GameObject ShowCanvas = null;
    public Slider Slider = null;
    private bool isHolding = false;
    private const float ButtonHeldDuration = 0.25f;
    private float ButtonPressedTime = 0;


    private void Start()
    {
        throwableRigidBody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        PickType();
    }


    void PickandThrow()
    {
        DetectInput();
        Pick();
        // throw button and settings
        if (isHolding)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                isHolding = false;
                throwableRigidBody.AddForce(PlayerHand.transform.forward * ThrowForce, ForceMode.Impulse);
            }
        }
    }
    void PickandDrop()
    {
        DetectInput();
        Pick();
    }
    void HoldPickandDrop()
    {
        // calculate player distance
        DistanceToPlayer = Vector3.Distance(transform.position, PlayerHand.transform.position);
        if (DistanceToPlayer <= PickupAbleDistance)
        {
            // if you press button, it will show slider and deactive text
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpText.SetActive(false);
                ShowCanvas.SetActive(true);
            }  
            if (Input.GetKey(KeyCode.E))
            {
                // if you hold down E, it add value every time.
                if (Time.timeSinceLevelLoad - ButtonPressedTime > ButtonHeldDuration)
                {
                    // if you want to change hold time, you can change slider.value
                    Slider.value += 0.001f;
                }
                // if you reach the correct value, he will pickup.
                if (Slider.value == 1.0f)
                {
                    ShowCanvas.SetActive(false);
                    if(isHolding == false){
                        isHolding = !isHolding;
                    }
                    Pick();
                }
            }
        }
        // if you release the key or walk away, it will reset all value.
        if (Input.GetKeyUp(KeyCode.E) || DistanceToPlayer > PickupAbleDistance)
        {
            // Keep track of when they started holding it.
            ButtonPressedTime = Time.timeSinceLevelLoad;

            ShowCanvas.SetActive(false);
            PickUpText.SetActive(false);
            Slider.value = 0.0f;
            transform.SetParent(null);
            throwableRigidBody.useGravity = true;
            GetComponent<Rigidbody>().freezeRotation = false;
            isHolding = false;
        }
    }
    void DetectInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (DistanceToPlayer <= PickupAbleDistance)
            {
                isHolding = !isHolding;
            }
        }
    }
    void Pick()
    {
        DistanceToPlayer = Vector3.Distance(transform.position, PlayerHand.transform.position);

        if (DistanceToPlayer >= PickupAbleDistance)
        {
            isHolding = false;
        }

        if (isHolding)
        {
            transform.SetParent(PlayerHand.transform);
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            throwableRigidBody.velocity = Vector3.zero;
            throwableRigidBody.angularVelocity = Vector3.zero;
            throwableRigidBody.useGravity = false;
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        else
        {
            transform.SetParent(null);
            throwableRigidBody.useGravity = true;
            GetComponent<Rigidbody>().freezeRotation = false;
        }
    }
    void PickType()
    {
        switch (Type)
        {
            case PickUpChoice.PickandThrow:
                PickandThrow();
                ShowText();
                break;
            case PickUpChoice.PickandDrop:
                PickandDrop();
                ShowText();
                break;
            case PickUpChoice.HoldPickandDrop:
                HoldPickandDrop();
                ShowText();
                break;
        }
    }
    void ShowText()
    {
        if (DistanceToPlayer <= PickupAbleDistance)
        {
            PickUpText.SetActive(true);
        }
        else if (DistanceToPlayer > PickupAbleDistance)
        {
            PickUpText.SetActive(false);
        }
        if (isHolding)
        {
            PickUpText.SetActive(false);
        }
    }
}