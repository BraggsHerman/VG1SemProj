using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test
{
    public class PopUpLogic : MonoBehaviour
    {
        public string instructions;
        public string popUp;
        public int fontSize;

        bool player;

        

        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void playerDied()
        {
            PopUpSystem pop = GameObject.FindGameObjectWithTag("GameController").GetComponent<PopUpSystem>();
            pop.PopUp(popUp, fontSize);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (gameObject.name == "Player")
            {
                return; //Do nothing.
            }
            if (other.gameObject.GetComponent<PlayerController>())
            {
                if (gameObject.tag == "BossDoor")
                {
                    
                    PopUpSystem pop = GameObject.FindGameObjectWithTag("GameController").GetComponent<PopUpSystem>();
                    if (GameController.instance.askedForIt)
                    {
                        if (GameController.instance.hasBow)
                        {
                            pop.PopUp(instructions, fontSize);
                        }
                        else
                        {
                            pop.PopUp(popUp, fontSize);
                        }
                    }
                    else
                    {
                        pop.PopUp(popUp, fontSize);
                    }
                    
                    GameController.instance.askedForIt = true;
                }
                else if (gameObject.name == "Boss")
                {
                    return;
                }
                else
                {
                    PopUpSystem pop = GameObject.FindGameObjectWithTag("GameController").GetComponent<PopUpSystem>();
                    if (GameController.instance.keyCount == 0)
                    {
                        pop.PopUp(popUp, fontSize);
                    }
                    if (GameController.instance.keyCount > 0)
                    {
                        pop.PopUp(instructions, fontSize);
                    }
                    if (GameController.instance.hasBow)
                    {
                        pop.PopUp(popUp, fontSize);
                    }
                }
                
                
            }
            
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                PopUpSystem pop = GameObject.FindGameObjectWithTag("GameController").GetComponent<PopUpSystem>();
                pop.closePopUp();
                if (gameObject.tag == "FirstClue")
                {
                    Destroy(gameObject);
                }
                if (gameObject.tag == "BossDoor")
                {
                    Destroy(gameObject);
                }
                if (gameObject.name == "InstructionPopUp")
                {
                    Destroy(gameObject);
                }
            }
        }

       
        


        // Update is called once per frame
        void Update()
        {

            if (gameObject.name == "Boss" && GameController.instance.beatBoss)
            {
                PopUpSystem pop = GameObject.FindGameObjectWithTag("GameController").GetComponent<PopUpSystem>();
                print(popUp);
                pop.PopUp(popUp, fontSize);
            }

            if (gameObject.name == "Boss" && GameController.instance.youLose)
            {
                PopUpSystem pop = GameObject.FindGameObjectWithTag("GameController").GetComponent<PopUpSystem>();
                print(instructions);
                pop.PopUp(instructions, fontSize);
            }

        }
    }
}

