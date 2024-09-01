using UnityEngine;

public class Switch : MonoBehaviour
{
    public int index;
    public GameObject[] skylanders;

    private void Start()
    {
        Change();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            index++;

            if (index > skylanders.Length - 1)
            {
                index = 0;
            }

            Change();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            index--;

            if (index < 0)
            {
                index = skylanders.Length - 1;
            }

            Change();
        }
    }

    void Change()
    {
        foreach (var skylander in skylanders)
        {
            skylander.SetActive(false);
        }

        skylanders[index].SetActive(true);
    }
}
