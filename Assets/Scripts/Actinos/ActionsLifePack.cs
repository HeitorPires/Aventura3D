using UnityEngine;
using Items;

public class ActionsLifePack : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.H;
    public SOInt soInt;
    private ItemType lifePack = ItemType.LIFE_PACK;

    private void Start()
    {
        soInt = ItemManager.Instance.GetByType(lifePack).soInt;
    }

    private void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            TryToRecoverLife();
        }
    }

    private void TryToRecoverLife()
    {
        if(soInt.value>0)
        {
            ItemManager.Instance.RemovebyType(lifePack);
            Player.Instance.healthBase.ResetLife();
        }
    }



}
