using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public enum HandType
{
    Left,
    Right
}

public class XRHandController : MonoBehaviour
{
    public HandType handType;

    public Animator animator;

    [SerializeField] private InputAction activeAnimation;
    [Space] [SerializeField] private InputActionAsset animControl;

    private float threeFingersValue;

    public void Awake()
    {
        var gaucheActionMap = animControl.FindActionMap("XRI LeftHand");

        activeAnimation = gaucheActionMap.FindAction("Select");

        activeAnimation.performed += AnimateHand;
        activeAnimation.canceled += AnimateHand;
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void AnimateHand (InputAction.CallbackContext context)
    {        
        animator.SetFloat("ThreeFingers", 1);
    }
}