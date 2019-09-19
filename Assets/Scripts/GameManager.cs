using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singletonGameManagerInstance = null;
    public PathHelper pathHelperInstance;
    public GridManager gridManagerInstance;

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

        pathHelperInstance = gameObject.GetComponent<PathHelper>();
        gridManagerInstance = gameObject.GetComponent<GridManager>();
    }
}
