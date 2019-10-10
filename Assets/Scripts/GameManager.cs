using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singletonGameManagerInstance = null;
    public PathHelper pathHelperInstance;
    public GridManager gridManagerInstance;
    public static int currency = 100;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (singletonGameManagerInstance != null && singletonGameManagerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            singletonGameManagerInstance = this;
        }
        DontDestroyOnLoad(gameObject);

        pathHelperInstance = FindObjectOfType<PathHelper>();
        gridManagerInstance = FindObjectOfType<GridManager>();
    }



    //placeholder
    public void saveGame()
    {

    }

    //placeholder
    public void loadGame()
    {

    }
}
