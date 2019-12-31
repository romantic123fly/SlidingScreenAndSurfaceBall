#region 模块信息
// **********************************************************************
// Copyright (C) 2019 Blazors
// Please contact me if you have any questions
// File Name:             CreatSurfaceBall
// Author:                幻世界
// QQ:                    853394528 
// **********************************************************************
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class CreatSurfaceBall : MonoBehaviour
{
    private Transform container;
    private List<Transform> transforms;
    void Start()
    {
        transforms = new List<Transform>();
        container = GameObject.Find("Container").transform;
        CreatPointOnSphere(100, 15);
        CreatPointOnSphere(80, 10);
    }
   /// <summary>
   /// 
   /// </summary>
   /// <param name="bornPointNum">实例个数</param>
   /// <param name="radius">球体半径</param>
    public void CreatPointOnSphere(int bornPointNum, float radius)
    {
        //生成
        for (int i = 0; i < bornPointNum; i++)
        {
            float y = (float)i * 2.0f / bornPointNum + (1 / bornPointNum) - 1.0f;
            float r = Mathf.Sqrt(1.0f - y * y);
            float  phi = i * 2.4f;
            Vector3 pos = new Vector3(Mathf.Cos(phi) * r * radius, y * radius, Mathf.Sin(phi) * r * radius); 
            BornPoint(pos, i);
        }
    }
   
    void BornPoint(Vector3 pos, int tempIndex)
    {
        GameObject obj = Instantiate( Resources.Load<GameObject>("Cube"));
        obj.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/"+ tempIndex%5);
        obj.transform.SetParent(container);
        obj.transform.localPosition = pos;
        obj.name = tempIndex+"";
        transforms.Add(obj.transform);
    }
    RaycastHit m_hitInfo;
    private void Update()
    {
        if (container != null)
        {
            if (Input.GetMouseButtonUp(0)) //点击鼠标右键
            {
                object ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isHit = Physics.Raycast((Ray)ray, out hit);
                if (isHit)
                {
                    Debug.Log(hit.transform.name);
                    GameManager.GetInstance().SetTargetItemIndex(int.Parse( hit.transform.name));
                }
            }

            if (Input.GetMouseButton(0)&& !EventSystem.current.IsPointerOverGameObject())
            {
                float hor = Input.GetAxis("Mouse X");
                float ver = Input.GetAxis("Mouse Y");
                container.Rotate(new Vector3(ver, -hor, 0) * Time.deltaTime * 100, Space.World);
            }
        }
        if (transforms!=null&& transforms.Count>0)
        {
            foreach (var item in transforms)
            {
                item.LookAt(Camera.main.transform);
            }
        }
    }
}

