
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SceneSmoothShift : UdonSharpBehaviour
{

    [SerializeField] private Transform shiftStart, shiftEnd;
    [SerializeField] private float ShiftSpeed;

    private float distance = 0;
    
    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        if(player == Networking.LocalPlayer)
            distance = Vector3.Distance(shiftStart.position, shiftEnd.position);
    }

    public void FixedUpdate()
    {
        var present_Location = ShiftSpeed != 0 ? (Time.time * ShiftSpeed) / distance : 0;
        transform.position = Vector3.Lerp(shiftStart.position, shiftEnd.position, present_Location);
    }
}
