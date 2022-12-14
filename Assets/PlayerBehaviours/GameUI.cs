using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private Player player;
    public Image HPB;
    public Image MPB;
    public TMP_Text ElementsUI;
    public Image ValueB;
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
            ValueB.fillAmount = player.currentPoints / player.stopPoints;
            ChangeUI();
        }
        else { 
            MPB.fillAmount = 0;
            HPB.fillAmount = 0;
            ElementsUI.text = "DIE";
        }
    }
    private void ChangeUI() {
        switch (player.Elements) {
            case 0:
                ElementsUI.text = "Fire";
                break;
            case 1:
                ElementsUI.text = "Earth";
                break;
                    case 2:
                ElementsUI.text = "Air";
                break;
            case 3:
                ElementsUI.text = "Water";
                break;
        }
    }
}
