using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.VisualScripting;

//Original code by Code Monkey: https://www.youtube.com/watch?v=dHzeHh-3bp4

public class ShowBaseDirection : MonoBehaviour
{
    [SerializeField] GameObject Base;
    [SerializeField] GameObject PointerImage;
    [SerializeField] GameObject PointerBack;

    [SerializeField] private Camera uiCamera;
    private Vector3 targetPosition;
    private RectTransform pointerRectTransfrom1;
    private RectTransform pointerRectTransfrom2;

    private void Awake()
    {
        targetPosition = Base.transform.localPosition;
        pointerRectTransfrom1 = transform.Find("White").GetComponent<RectTransform>();
        pointerRectTransfrom2 = transform.Find("BaseImage").GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0;
        Vector3 dir = (toPosition - fromPosition).normalized;

        float borderSize = 100f;
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <=borderSize || targetPositionScreenPoint.x >= Screen.width-borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height-borderSize;

        if (isOffScreen)
        {
            PointerImage.SetActive(true);
            PointerBack.SetActive(true);
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if(cappedTargetScreenPosition.x <= borderSize)
            {
                cappedTargetScreenPosition.x = borderSize;
            }
            if (cappedTargetScreenPosition.x > Screen.width-borderSize)
            {
                cappedTargetScreenPosition.x = Screen.width - borderSize;
            }
            if (cappedTargetScreenPosition.y <= borderSize)
            {
                cappedTargetScreenPosition.y = borderSize;
            }
            if (cappedTargetScreenPosition.y > Screen.height - borderSize)
            {
                cappedTargetScreenPosition.y = Screen.height-borderSize;
            }

            //Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRectTransfrom1.position = cappedTargetScreenPosition;
            pointerRectTransfrom2.position = cappedTargetScreenPosition;
            pointerRectTransfrom1.localPosition = new Vector3(pointerRectTransfrom1.localPosition.x, pointerRectTransfrom1.localPosition.y, 0f);
            pointerRectTransfrom2.localPosition = new Vector3(pointerRectTransfrom2.localPosition.x, pointerRectTransfrom2.localPosition.y, 0f);

        }
        else
        {
            PointerImage.SetActive(false);
            PointerBack.SetActive(false);
        }

    }
}
