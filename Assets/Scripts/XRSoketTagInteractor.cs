using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSoketTagInteractor : XRSocketInteractor
{
    //특정 오브젝트 미리보기 소켓
    public string targetTag;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && interactable.transform.tag == targetTag;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        if (interactable.transform.tag == targetTag && transform.parent.parent.childCount < 2) // 3이 아닌 2로 수정
            interactable.transform.SetParent(transform.parent.parent, false);
        return base.CanSelect(interactable) && interactable.transform.tag == targetTag;
    }

}
