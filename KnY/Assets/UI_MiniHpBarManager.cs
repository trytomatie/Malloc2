
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MiniHpBarManager : MonoBehaviour
{
    public Statusmanager statusmanager;
    public Image hpBar;
    public float timer = 1;
    public float heigthOffset;
    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    void LateUpdate()
    {
        if (timer < 0 || statusmanager == null)
        {
            Destroy(gameObject);
        }
        transform.position = new Vector3(statusmanager.transform.position.x, statusmanager.transform.position.y + heigthOffset, 0);
        timer -= Time.deltaTime;

    }


    public void UpdateUI()
    {
        UpdateUpBar();
    }
    public void UpdateUI(float time)
    {
        timer = time;
        UpdateUpBar();
    }
    private void UpdateUpBar()
    {
        float hpPercent = (float)statusmanager.Hp / (float)statusmanager.maxHp;
        hpBar.GetComponent<Image>().fillAmount = hpPercent;
    }

}
