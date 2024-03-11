using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CPManager : MonoBehaviour
{
    public CPData currentCP;

    public TextMeshProUGUI text;

    // Update is called once per frame

    private void Start()
    {
        CPData baseData = new CPData();
        baseData.cpPos = transform;
        currentCP = baseData;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) gameObject.transform.SetPositionAndRotation(currentCP.cpPos.position, currentCP.cpPos.rotation);

        text.text = Vector3.Distance(transform.position, currentCP.nextCp.cpPos.position).ToString();
    }
}
