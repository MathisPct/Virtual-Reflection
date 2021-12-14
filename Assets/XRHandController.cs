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

    [SerializeField] private Animator animator;

    [SerializeField] private InputAction activeAnimation;
    [Space] [SerializeField] private InputActionAsset animControl;


    public void Awake()
    {
        animator = this.GetComponent<Animator>();

        var gaucheActionMap = animControl.FindActionMap("XRI LeftHand");

        activeAnimation = gaucheActionMap.FindAction("Index Hand");

        activeAnimation.performed += AnimateHand;
        activeAnimation.canceled += AnimateHand;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void AnimateHand (InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        Debug.Log("VALUE TO MAP: " + value);
        value = Mathf.Clamp(value, 0, 1);
        animator.SetFloat("Blend", value);
        //animator.Play("Blend Tree", 0);
    }
}