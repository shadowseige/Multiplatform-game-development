using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterBar : MonoBehaviour
{
    public Slider boosterBar;
    private int maxBoost = 100;
    private int currentBooster;
    private WaitForSeconds Regentick = new WaitForSeconds(0.3f);
    private Coroutine regen;

    public static BoosterBar instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentBooster = maxBoost;
        boosterBar.maxValue = maxBoost;
        boosterBar.value = maxBoost;
    }

    public void useBoost(int amount)
    {
        if(currentBooster - amount >= 0)
        {
            currentBooster -= amount;
            boosterBar.value = currentBooster;

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenBoost());
        }
        else
        {
            Debug.Log("No boost!");
        }
    }

    private IEnumerator RegenBoost()
    {
        yield return new WaitForSeconds(2);

        while(currentBooster < maxBoost)
        {
            currentBooster += maxBoost / 50;
            boosterBar.value = currentBooster;
            yield return Regentick;
        }
        regen = null;
    }
}
