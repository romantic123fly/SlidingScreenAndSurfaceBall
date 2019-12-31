#region 模块信息
// **********************************************************************
// Copyright (C) 2019 Blazors
// Please contact me if you have any questions
// File Name:             GameManager
// Author:                幻世界
// QQ:                    853394528 
// **********************************************************************
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public EnhanceScrollView EnhanceScroll;
    public Button button;


    private static GameManager instance;
    public static GameManager GetInstance() {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        button.transform.parent.gameObject.SetActive(false);
        button.onClick.AddListener(()=> { button.transform.parent.gameObject.SetActive(false); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public  void SetTargetItemIndex(int a)
    {
        button.transform.parent.gameObject.SetActive(true);

        EnhanceScroll.SetHorizontalTargetItemIndex(EnhanceScroll.listEnhanceItems[a%6]);
    }

}
