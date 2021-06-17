
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;

public class AdjustableChair : UdonSharpBehaviour
{

    [SerializeField] private Collider handleCollider;
    [SerializeField] private MeshRenderer handle;
    [SerializeField] private SceneSmoothShift[] smoothShift;
    //[SerializeField] private Text text;
    [SerializeField] private Transform[] objects;

    public void FixedUpdate()
    {
        //text.text = $"{objects[0].name} : Owner ? {Networking.LocalPlayer.IsOwner(objects[0].gameObject)}";
    }

    public override void Interact()
    {
        Networking.LocalPlayer.UseAttachedStation();
    }
    

    public override void OnStationEntered(VRCPlayerApi player)
    {
        if (Networking.LocalPlayer != player)
            return;
        
        Debug.Log("[DEBUG] start " );
        SetOwnerGameObjects(objects, player);
        
        if(handleCollider != null)
            handleCollider.enabled = true;

        if (handle != null)
            handle.enabled = true;
        
        for (int i = 0; i < smoothShift.Length; i++)
            smoothShift[i].enabled = true;
    }

    private void SetOwnerGameObjects(Transform[] trans, VRCPlayerApi player)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            Networking.SetOwner(player, objects[i].gameObject);
            Debug.Log("[DEBUG] SetOwner " );
        }
    }

    public override void OnStationExited(VRCPlayerApi player)
    {
        if (Networking.LocalPlayer != player)
            return;
        
        if(handleCollider != null)
            handleCollider.enabled = false;

        if (handle != null)
            handle.enabled = false;
        
        for (int i = 0; i < smoothShift.Length; i++)
            smoothShift[i].enabled = false;

        var pickup = (VRCPickup) handleCollider.gameObject.GetComponent(typeof(VRCPickup));
        if(pickup != null)
            pickup.Drop();
    }


}
