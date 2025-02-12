using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TopDownCharacterShooting : NetworkBehaviour
{
    [Header("Settings")]
    public float aimSpeed = 10.0f;
    public float holsterSpeed = 2.5f;
    public float aimDuration = 5.0f;
    private bool isAimed = false;
    private float aimWeight = 0.0f;
    private float currentAimTime = 0.0f;
    private GameObject modelObject;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        modelObject = transform.GetChild(0).gameObject;
        animator = modelObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!base.IsOwner) return;
        
        // Aim if player shoots
        if (Input.GetKey(KeyCode.Mouse0)) {
            currentAimTime = 0;
            isAimed = true;
            // TODO: Play shoot animation and sound
        }

        AnimatedAim();
    }

    public void AnimatedAim()
    {
        if (currentAimTime >= aimDuration) {
            isAimed = false;
        }

        if (isAimed) {
            currentAimTime += Time.deltaTime;
            aimWeight = Mathf.Lerp(aimWeight, 1, Time.deltaTime * aimSpeed);
        } else {
            currentAimTime = 0;
            aimWeight = Mathf.Lerp(aimWeight, 0, Time.deltaTime * holsterSpeed);
        }

        // ANIMATION LAYERS: Base: 0, Aiming: 1
        animator.SetLayerWeight(1, aimWeight);
    }
}
