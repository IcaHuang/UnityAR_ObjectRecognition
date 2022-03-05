using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//当脚本挂到某个GameObject时，会自动添加(add)一个ARRaycastManager脚本
[RequireComponent(typeof(ARRaycastManager))]

public class PlacementController : MonoBehaviour
{
    //[SerializeField] ？
    [SerializeField]
    private GameObject placedPrefab;

    public GameObject PlacedPrefab
    {
        get     //get与set什么意思？
        {
            return placedPrefab;
        }
        set
        {
            placedPrefab = value;   //Q: =value是什么意思？
        }
    }

    private GameObject placedObject;

    //需要利用raycast判断用户是否点击到了AR检测出的平面上
    private ARRaycastManager arRaycastManager;  

    //To track where the hits are happening in screen
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool instantiateCheck = false;  //检测是否创建对象了


    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    //Q：不加private/public声明时，是默认private吗？
    //该函数用于检测用户是否点击了手机屏幕
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)    //Input.touchCount ？？
        {
            touchPosition = Input.GetTouch(0).position; //Input.GetTouch(0) ？？
            return true;
        }
        touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))    //如果没有按屏幕，则不往下做raycast检测
            return;

        if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))    //ARRayCastManger.Raycast()会返回boolean：是否点到检测到的平面上
        {
            //未创建对象，先创建一个对象
            if (!instantiateCheck)
            {
                instantiateCheck = true;
                //The "pose", in Unity world space, of the intersection point.
                //https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.0/api/UnityEngine.XR.ARFoundation.ARRaycastHit.html
                var hitPose = hits[0].pose;
                placedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);  //创建物体

            }
            else //已创建要显示的对象，做修改对象位置的操作即可，不再多创建一个对象
            {
                //屏幕点击时，做点击到的ar平台位置检测，通过raycast找到对应的放置点
                //placedObject = 
            }

            ////原代码
            //var hitPose = hits[0].pose;
            //Instantiate(placedPrefab, hitPose.position, hitPose.rotation);  //创建物体
        }
    }
}