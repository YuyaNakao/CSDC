using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeObject : MonoBehaviour
{
    [SerializeField, Range(0.0f, 360.0f)]
    private float m_widthAngle = 0.0f;
    [SerializeField, Range(0.0f, 360.0f)]
    private float m_heightAngle = 0.0f;
    [SerializeField, Range(0.0f, 15.0f)]
    private float m_length = 0.0f;

    public float WidthAngle { get { return m_widthAngle; } }
    public float HeightAngle { get { return m_heightAngle; } }
    public float Length { get { return m_length; } }

} // class RangeObject
