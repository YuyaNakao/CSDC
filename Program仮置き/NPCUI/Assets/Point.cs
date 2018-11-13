using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public enum EType
    {
        Item,
        Magic,
        Trap,
    }

    [SerializeField]
    private EType m_type = default(EType);
    [SerializeField, Range(0.0f, 10.0f)]
    private float m_radius = 0.0f;

    public EType Type { get { return m_type; } }
    public float Radius { get { return m_radius; } }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
