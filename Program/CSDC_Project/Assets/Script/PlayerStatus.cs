using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//　派生クラス
public class PlayerStatus :Status {

    public override float ChargePower()
    {
        return power;
    }

    //public override int GetHP()
    //{
    //    return m_hp;
    //}

    //public override float GetPower()
    //{
    //    return m_power;
    //}
}
