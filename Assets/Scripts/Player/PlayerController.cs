using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerActions))]
public class PlayerController : MonoBehaviour
{
    private PlayerActions actions;

    void Start()
    {
        actions = GetComponent<PlayerActions>();
    }

    void Update()
    {
        actions.DoOnUpdate();
    }

    void FixedUpdate()
    {
        actions.DoOnFixedUpdate();
    }
}
