using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private Player player;
    public Image HPB;
    public Image MPB;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            HPB.fillAmount = player.HP / player.MaxHP;
            MPB.fillAmount = player.MP / player.MaxMP;
        }
        else { 
            MPB.fillAmount = 0;
            HPB.fillAmount = 0;
        }
    }
}
