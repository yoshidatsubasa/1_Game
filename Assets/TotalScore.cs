using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{


    public Text textComponent;
   

    int coinCount;
   
    void Start()
    {
        coinCount = Coincounter.getscore();
        
        textComponent.text = "älìæÇµÇΩÉRÉCÉìÇÃñáêî :   " + coinCount+"ñá";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
