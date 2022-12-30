using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour, IPointerClickHandler
{
    void Update()
    {
        transform.Rotate(0, Time.deltaTime*10, 0);
		
		if (Application.platform == RuntimePlatform.Android)
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    void ChangeColor(string colorCode)
    {
        if (ColorUtility.TryParseHtmlString(colorCode, out Color color))
        {
            GetComponent<Renderer>().material.color = color;
        } else {
            GetComponent<Renderer>().material.color = Color.black;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Color color = GetComponent<Renderer>().material.color;
        string stringColor = ColorUtility.ToHtmlStringRGBA(color);
        Debug.Log($"[Cube.cs] OnPointerClick() called name: ${name} color: ${stringColor}");
        #if UNITY_ANDROID
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("io.github.yuk7.uaaltest.android.MainActivity");
            AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("instance");
            activity.Call("onUnityObjectClick", stringColor);
        } catch(Exception e)
        {
            Debug.LogError($"[Cube.cs] OnPointerClick() exception: ${e.Message}\n${e.StackTrace}");
        }
        #endif
    }
}

