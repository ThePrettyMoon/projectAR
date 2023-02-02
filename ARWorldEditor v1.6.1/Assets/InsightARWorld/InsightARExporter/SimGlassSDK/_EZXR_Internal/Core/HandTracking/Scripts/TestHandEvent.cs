using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestHandEvent : MonoBehaviour
{
    public GameObject obj;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))//enter
        //{
        //    ExecuteEvents.Execute(obj, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))//down
        //{
        //    ExecuteEvents.Execute(obj, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))//up
        //{
        //    ExecuteEvents.Execute(obj, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))//click
        //{
        //    ExecuteEvents.Execute(obj, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha5))//deselectHandler
        //{
        //    ExecuteEvents.Execute(obj, new PointerEventData(EventSystem.current), ExecuteEvents.deselectHandler);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha6))//exit
        //{
        //    ExecuteEvents.Execute(obj, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha7))//selectHandler
        //{
        //    ExecuteEvents.Execute(obj, new PointerEventData(EventSystem.current), ExecuteEvents.selectHandler);
        //}
    }

    public void Test(string content)
    {
        Debug.Log(content);
    }
}
