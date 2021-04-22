using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace test
{
    public class PopUpSystem : MonoBehaviour
    {
        public GameObject popUpBox;
        public Text popUpText;
        


        //Methods
        public void PopUp(string text, int fSize)
        {
            popUpBox.SetActive(true);
            popUpText.text = text;
            popUpText.fontSize = fSize;
        }

        public void closePopUp()
        {
            popUpBox.SetActive(false);
        }

        void Start()
        {
            popUpBox.SetActive(false);
        }

        
    }
}

