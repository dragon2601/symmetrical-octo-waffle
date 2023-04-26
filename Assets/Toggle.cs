using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToToggle;
    [SerializeField] KeyCode[] inputKeys;
    [SerializeField] PauseMenu pauseMenuScript; // Reference to the PauseMenu script

    void Update()
    {
        for (int i = 0; i < objectsToToggle.Length; i++)
        {
            if (Input.GetKeyDown(inputKeys[i]))
            {
                bool activeState = objectsToToggle[i].activeInHierarchy;
                objectsToToggle[i].SetActive(!activeState);

                // Call the Pause method from the PauseMenu script and pass the new active state
                if (objectsToToggle[i].name == "PauseMenu") // Make sure to match the name of your PauseMenu GameObject
                {
                    pauseMenuScript.Pause(!activeState);
                }
            }
        }
    }
}
