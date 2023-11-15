using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSoketTagInteractor : XRSocketInteractor
{
    //특정 오브젝트 미리보기 소켓
    public string targetTag;
    private IXRSelectInteractable seeding;

    private void Update()
    {
        if (seeding != null && IsSelecting(seeding) && seeding.transform.GetComponent<XRGrabInteractable>().enabled)
        {
            Invoke("objectFreez", 3f);
        }
    }
    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.tag == targetTag;
    }
    private void objectFreez()
    {
        seeding.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        seeding.transform.GetComponent<XRGrabInteractable>().enabled = false;
        Destroy(transform.GetComponent<XRSoketTagInteractor>());
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        seeding = interactable;
        return base.CanSelect(interactable) && interactable.transform.tag == targetTag;
        //print(IsSelecting(interactable));
        //if(interactable.transform.tag == targetTag && IsSelecting(interactable)) return true;
        //return base.CanSelect(interactable) && interactable.transform.tag == targetTag && !IsSelecting(interactable);
    }
}
