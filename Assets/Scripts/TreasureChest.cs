using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{

    public Item contents;
    public bool isOpen;
    public Inventory playerInventory;
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
           if(!isOpen)
           {
               //Open chest
               OpenChest();
           }else
           {
               //Chest is already Open
               ChestAlreadyOpen();
           }
        }
    }

    public void OpenChest()
    {
        //DialogBox on
        dialogBox.SetActive(true);
        //dialog text = contents text
        dialogText.text = contents.itemDescription;
        //add contents to the player inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        //raise the signal to player for animation
        raiseItem.Raise();
        //raise the context clue
        context.Raise();
        isOpen = true;
        anim.SetBool("opened", true);
    }

    public void ChestAlreadyOpen()
    {
        // dialog off
        dialogBox.SetActive(false);
        //raise the signal to the player to stop animation
        raiseItem.Raise();
       
        
    }

     private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = false;
        }
    }






}
