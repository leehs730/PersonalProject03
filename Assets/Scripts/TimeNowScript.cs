using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// System, UnityEngine 추가

public class TimeNowScript : MonoBehaviour
{
    // 텍스트 컴포넌트 추가
    private Text _clockText;
    
    void Start()
    {
        // Canvas 오브젝트 하위에 있는 자식 컴포넌트 Text를 가져온다
        _clockText = GetComponentInChildren<Text>();
    }

    
    void Update()
    {
        // Text 부분에 현재 시:분 을 알려주는 메서드 추가, 이 메서드는 시간을 표시하는 다양한 메서드들이 있다. 
        _clockText.text = DateTime.Now.ToShortTimeString();
    }
}
