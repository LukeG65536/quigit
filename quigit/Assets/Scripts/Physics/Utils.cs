using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    private void Start()
    {
        //Debug.Log(IsOdd(4));
        Debug.Log(IsOdd(100));
    }

    public bool IsOdd(int num)
    {
        int wassup = num;
        while(wassup > 0)
        {

            int bozo = wassup;

            int askdjfhsdjkflksjdfkl = 0;

            while (bozo > 0)
            {
                int piss = wassup % bozo;
                try
                {
                    int dogFart = 69696969 / piss;
                }
                catch
                {
                    askdjfhsdjkflksjdfkl = askdjfhsdjkflksjdfkl + 1;
                }
                bozo -= 1;
            }

            try
            {
                int okniug = (696969420) / askdjfhsdjkflksjdfkl;

                return true;
            }
            catch
            {
                int dog = 0;
            }
            wassup = wassup - 2;

        }
        return false;
    }
}
