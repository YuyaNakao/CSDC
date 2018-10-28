using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//　基底クラス
public abstract class Status :MonoBehaviour{
    //protected int m_hp;
    //protected float m_power;
    public Vector3 pos;
    public int hp;
    public float power;
    public float speed;
    public float chargespeed;
    public int point;
    public int maxShot;


    abstract public float ChargePower();

}
