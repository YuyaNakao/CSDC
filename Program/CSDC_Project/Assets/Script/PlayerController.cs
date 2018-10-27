using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerController
{

    private int m_num = 0;

    public void Init(int num)
    {
        m_num = num;
    }

    public bool GetInput(int button_num)
    {
        switch (m_num)
        {
            case 0:
                switch (button_num)
                {
                    case 0:
                        return Input.GetKeyDown(KeyCode.A);
                    case 1:
                        return Input.GetKeyDown(KeyCode.B);
                }
                break;
            case 1:
                switch (button_num)
                {
                    case 0:
                        return Input.GetKeyDown(KeyCode.C);
                    case 1:
                        return Input.GetKeyDown(KeyCode.D);
                }
                break;
        }
        return false;
    }
}
