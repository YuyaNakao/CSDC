using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

//　基底クラス
public abstract class Status :MonoBehaviour{
    public Vector3 pos;
    public float power;
    public float speed;
    public float chargespeed;
    public static int point;
    public int maxShot;
    public GamePad.Index playerNo;

}
