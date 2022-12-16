using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// The GameUI behavior
/// </summary>

public class GameUI : MonoBehaviour
{
    /// <summary>
    /// Player Object
    /// </summary>
    private Player player;
    /// <summary>
    /// Health Bar
    /// </summary>
    public Image HPB;
    /// <summary>
    /// Magic Bar
    /// </summary>
    public Image MPB;
    /// <summary>
    /// Element UI
    /// </summary>
    public TMP_Text ElementsUI;
    /// <summary>
    /// Enemy Value UI
    /// </summary>
    public Image ValueB;
    // Start is called before the first frame update
    void Start()
    {
        /// read the player object to ge HP and MP
        player = GameObject.FindGameObjectWithTag("PlayerBehavior").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            HPB.fillAmount = player.HP / player.MaxHP;
            MPB.fillAmount = player.MP / player.MaxMP;
            if (player.stopPoints != 0) {
                ValueB.fillAmount = player.currentPoints / player.stopPoints;
            }
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
