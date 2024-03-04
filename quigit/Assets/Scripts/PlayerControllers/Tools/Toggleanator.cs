using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Toggleanator : MonoBehaviour
{
    public int currentDim = 0;

    List<ToggleBlock> togglers = new List<ToggleBlock>();

    private void Start()
    {
        var blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject block in blocks)
        {
            togglers.Add(block.GetComponent<ToggleBlock>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Toggling all");
            currentDim = (currentDim == 0) ? 1 : 0;
            toggleAll();
        }
    }

    private void toggleAll()
    {
        foreach(var toggler in togglers)
        {
            toggler.setToggle(currentDim);
        }
    }
}
