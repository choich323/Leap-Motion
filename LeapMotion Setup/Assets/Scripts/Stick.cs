using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public GameObject sugar_white;
    public GameObject sugar_white_mole;
    public GameObject sugar_brown_mole;
    public GameObject withBakingSoda;
    public GameObject sugar_brown_middle;
    public GameObject sugar_brown_high;
    public GameObject sugar_complete;
    public GameObject swtch;

    public GameObject startMessage;
    public GameObject swtchOnMessage;

    public GameObject stirtoBrownMessage;
    public GameObject swtchOffMessage;
    public GameObject bsPickupMessage;
    public GameObject stirMessage;

    public GameObject overFlowing;
    public GameObject clear;
    public GameObject failed_overflow;

    public int total_MixCount;

    bool isStart = false;
    bool needFire = true;

    int mixCount1 = 0;
    int mixCount2 = 0;
    int mixCount3 = 0;
    int mixCount4 = 0;
    float time = 0;
    float time1 = 0;
    float time2 = 0;
    float time3 = 0;

    void OnTriggerEnter(Collider other)
    {


        if (swtch.activeInHierarchy && sugar_white.activeInHierarchy && !sugar_white_mole.activeInHierarchy)
            MixCount(other, 6);
        else if (swtch.activeInHierarchy && sugar_white_mole.activeInHierarchy)
            MixCount(other, 12);
        else if (!swtch.activeInHierarchy && withBakingSoda.activeInHierarchy)
            MixCount(other, 17);
        else if (!swtch.activeInHierarchy && sugar_brown_middle.activeInHierarchy)
            MixCount(other, 22);
        else if (!swtch.activeInHierarchy && sugar_brown_high.activeInHierarchy)
            MixCount(other, 27);
    }



    void MixCount(Collider other, int value)
    {
        if (other.gameObject.tag == "B1" && mixCount1 < value)
            mixCount1++;
        else if (other.gameObject.tag == "B2" && mixCount2 < value)
            mixCount2++;
        else if (other.gameObject.tag == "B3" && mixCount3 < value)
            mixCount3++;
        else if (other.gameObject.tag == "B4" && mixCount4 < value)
            mixCount4++;
    }

    void Update()
    {
        UI();
        Sum();
        SugarChangetoWhiteMole();
        SugarChangetoBrownMole();
        SugarRise();
        SugarChangeBrown_Middle();
        SugarChangeBrown_High();
        SugarChangeBrown_Complete();
        Clear();
    }

    void UI()
    {
        if (!isStart && sugar_white.activeInHierarchy) {
            startMessage.SetActive(false);
            isStart = true;
            swtchOnMessage.SetActive(true);
        }

        if (swtch.activeInHierarchy && !startMessage.activeInHierarchy)
        {
            swtchOnMessage.SetActive(false);
            stirtoBrownMessage.SetActive(true);
        }

        if (stirtoBrownMessage.activeInHierarchy && !swtch.activeInHierarchy) // 예외 처리
        {
            stirtoBrownMessage.SetActive(false);
            swtchOnMessage.SetActive(true);
        }
        
        if(sugar_brown_mole.activeInHierarchy && swtch.activeInHierarchy)
        {
            stirtoBrownMessage.SetActive(false);
            swtchOffMessage.SetActive(true);
        }

        if (swtchOffMessage.activeInHierarchy && !swtch.activeInHierarchy)
        {
            swtchOffMessage.SetActive(false);
            bsPickupMessage.SetActive(true);
        }

        if(bsPickupMessage.activeInHierarchy && swtch.activeInHierarchy) // 예외 처리
        {
            bsPickupMessage.SetActive(false);
            swtchOffMessage.SetActive(true);
        }

        if (bsPickupMessage.activeInHierarchy && withBakingSoda.activeInHierarchy)
        {
            bsPickupMessage.SetActive(false);
            stirMessage.SetActive(true);
        }

        if(stirMessage.activeInHierarchy && swtch.activeInHierarchy)
        {
            stirMessage.SetActive(false);
            swtchOffMessage.SetActive(true);
        }
    }

    void NeedFire()
    {
        if (needFire && !swtch.activeInHierarchy && !swtchOnMessage.activeInHierarchy)
            swtchOnMessage.SetActive(true);
        if (!needFire && swtch.activeInHierarchy && !swtchOffMessage.activeInHierarchy)
            swtchOffMessage.SetActive(true);
    }

    void Sum()
    {
        total_MixCount = mixCount1 + mixCount2 + mixCount3 + mixCount4;
    }

    void SugarChangetoWhiteMole()
    {
        if (total_MixCount == 24 && swtch.activeInHierarchy && sugar_white.activeInHierarchy)
        {
            sugar_white_mole.SetActive(true);
        }
    }

    void SugarChangetoBrownMole()
    {
        if (total_MixCount == 48 && swtch.activeInHierarchy && sugar_white_mole.activeInHierarchy)
        {
            sugar_brown_mole.SetActive(true);
            needFire = false;
        }
    }

    void SugarRise()
    {
        if (withBakingSoda.activeInHierarchy)
        {
            time1 += Time.deltaTime;
            if (time1 > 7)
            {
                withBakingSoda.SetActive(false);
                sugar_brown_middle.SetActive(true);
                overFlowing.SetActive(true);
            }
        }
        else if (sugar_brown_middle.activeInHierarchy)
        {
            time2 += Time.deltaTime;
            if(time2 > 6)
            {
                sugar_brown_middle.SetActive(false);
                sugar_white_mole.SetActive(false);
                sugar_brown_mole.SetActive(false);
                sugar_brown_high.SetActive(true);
                overFlowing.SetActive(true);
            }
        }
        else if (sugar_brown_high.activeInHierarchy)
        {
            time3 += Time.deltaTime;
            if(time3 > 6)
            {
                if (overFlowing.activeInHierarchy)
                    overFlowing.SetActive(false);
                stirMessage.SetActive(false);
                failed_overflow.SetActive(true);
            }
        }
    }

    void SugarChangeBrown_Middle()
    {
        if (withBakingSoda.activeInHierarchy)
        {
            if (!sugar_brown_middle.activeInHierarchy && total_MixCount == 68)
            {
                withBakingSoda.SetActive(false);
                sugar_brown_middle.SetActive(true);
            }
        }
    }

    void SugarChangeBrown_High()
    {
        if (sugar_brown_middle.activeInHierarchy)
        {
            if (!sugar_brown_high.activeInHierarchy && total_MixCount == 88)
            {
                sugar_brown_middle.SetActive(false);
                sugar_white_mole.SetActive(false);
                sugar_brown_mole.SetActive(false);
                sugar_brown_high.SetActive(true);
                overFlowing.SetActive(false);
            }
        }
    }

    void SugarChangeBrown_Complete()
    {
        if (sugar_brown_high.activeInHierarchy)
        {
            if (!sugar_complete.activeInHierarchy && total_MixCount == 108)
            {
                sugar_brown_high.SetActive(false);
                sugar_complete.SetActive(true);
                overFlowing.SetActive(false);
            }
        }
    }

    void Clear()
    {
        if (sugar_complete.activeInHierarchy && !overFlowing.activeInHierarchy)
        {
            time += Time.deltaTime;
            if (time > 1) {
                stirMessage.SetActive(false);
                clear.SetActive(true);
            } 
        }
    }
}
