using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class NPCManager
{

	private static void Update () {
        GameObject[] m_izimekko;       //いじめっ子タグからのデータを入れるゲームオブジェクト
        GameObject[] m_izimerarekko;   //いじめられっ子タグからのデータを入れるゲームオブジェクト
        GameObject[] m_hutuunoko;      //いじめっ子リーダータグからのデータを入れるゲームオブジェクト
        GameObject[] m_izimekko_l;     //普通の子タグからのデータを入れるゲームオブジェクト		
        //いじめ子のデータを取得
        m_izimekko = GameObject.FindGameObjectsWithTag("izimekko");
        //いじめられっ子のデータを取得
        m_izimerarekko = GameObject.FindGameObjectsWithTag("izimerarekko");
        //いじめ子リーダーのデータを取得
        m_izimekko_l = GameObject.FindGameObjectsWithTag("izimekko_L");
        //普通の子のデータを取得
        m_hutuunoko = GameObject.FindGameObjectsWithTag("hutuunoko");
        for (int i = 0; i<m_hutuunoko.Length; i++)
        {
            if (m_izimekko.Length <= 2)//いじめっ子が少なくなると普通の子がいじめっ子に変化する
            {
                hutuunoko hutuunoko =m_hutuunoko[i].GetComponent<hutuunoko>();
                hutuunoko.move_mode = hutuunoko.move.izimekko;
                break;
            }
            if (m_izimerarekko.Length <= 2)//いじめられっ子が少なくなると普通の子がいじめっ子に変化する
            {
                hutuunoko hutuunoko = m_hutuunoko[i].GetComponent<hutuunoko>();
                hutuunoko.move_mode = hutuunoko.move.izimerarekko;
                break;
            }
        }
        Debug.Log(m_hutuunoko.Length);
	}
}
