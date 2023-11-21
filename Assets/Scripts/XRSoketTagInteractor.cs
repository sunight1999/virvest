using Oculus.Interaction;
using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSoketTagInteractor : XRSocketInteractor
{
    //특정 오브젝트 미리보기 소켓
    public string targetTag;
    private IXRSelectInteractable hand;

    void Update()
    {
        if (hand != null && IsSelecting(hand) && transform.parent.parent.gameObject.layer == 11)
        {
            Invoke("objectFreezSeed", 0.5f);
        }
        else if(hand != null && transform.parent.parent.gameObject.layer == 12 && IsSelecting(hand))
        {
            Invoke("objectFreezSupportFixture", 0.5f);
        }
    }
    public override bool CanHover(IXRHoverInteractable interactable)
    {
        //if(hand.transform.gameObject.tag == "Seeding" && transform.parent.parent.gameObject.layer == 12)
        //    return base.CanHover(interactable) && interactable.transform.tag == targetTag;
        //else if (hand.transform.gameObject.tag == "SupportFixture" && transform.parent.parent.gameObject.layer == 13)
        //    return base.CanHover(interactable) && interactable.transform.tag == targetTag;
        //return false;
        return base.CanHover(interactable) && interactable.transform.tag == targetTag;
    }
    void objectFreezSeed()
    {
        transform.parent.parent.gameObject.layer = 12;
        hand.transform.SetParent(transform.parent.parent);
        hand.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Destroy(hand.transform.GetChild(0).transform.GetComponent<XRGrabInteractable>());
        Destroy(gameObject);
    }

    void objectFreezSupportFixture()
    {
        transform.parent.parent.gameObject.layer = 13;
        hand.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Destroy(hand.transform.GetComponent<XRGrabInteractable>());
        hand.transform.SetParent(transform.parent.parent);
        //Destroy(transform.GetComponent<XRSoketTagInteractor>());
        Destroy(gameObject);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        hand = interactable;
        return base.CanSelect(interactable) && interactable.transform.tag == targetTag;
    }
}
