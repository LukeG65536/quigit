using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBlock : MonoBehaviour
{
    private Collider m_collider;
    private MeshRenderer m_renderer;
    public bool isOn = true;
    public int dim = 0;

    private void Start()
    {
        m_collider = GetComponent<Collider>();
        m_renderer = GetComponent<MeshRenderer>();
    }

    public void setToggle(int dim)
    {
        set(this.dim == dim);
    }

    public void set(bool on)
    {
        m_collider.enabled = on;
        m_renderer.enabled = on;
        isOn = on;
        //Debug.Log("toggled");
    }
}
