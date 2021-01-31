using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Bytes;

public class PlayerController : Bytes.Controllers.FPSController
{
    private GenericAnimationStateMachine animController;
    public bool alive = true;

    public Rigidbody pickedItem;
    public Transform pickedItemTarget;

    public float forceTowardPickedObj = 12f;

    private DialogPlayer dialogPlayer;

    public void Start()
    {
        canBeControlled = false;
        pickedItem = null;
        
        animController = GetComponentInChildren<GenericAnimationStateMachine>();
        dialogPlayer = GetComponent<DialogPlayer>();

        EventManager.AddEventListener("playerDropsObject", (Bytes.Data d)=> {
            DropObject();
        });

        Cursor.visible = false; Cursor.lockState = CursorLockMode.Locked;
        Animate.Delay(5f, ()=> {
            PlayDialog(0);
            Animate.Delay(3f, ()=> { canBeControlled = true; });
        });
    }

    protected override void Update()
    {
        _PickItem_Update();

        if (!canBeControlled) { return; }

        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractionManager.InteractWithCurrentObject();
        }

        float vitesse = _Movement_Update();
        _Camera_Update();
        _Controls_Update();

        if (vitesse > 0)
        {
            PlayerAnim used = PlayerAnim.Walking;
            if (vitesse > walkingSpeed) { used = PlayerAnim.Running; }
            animController?.SetLoopedState(used, "", true);
        }
        else
        {
            animController?.SetLoopedState(PlayerAnim.Idle, "", true);
        }
    }

    public void PlayDialog(int index, System.Action call = null)
    {
        dialogPlayer.PlayDialog(index, call);
    }

    public void Stop()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    /*public void AddGluten(float amount)
    {
        gluten = Mathf.Clamp(gluten + amount, 0, 100);

        if (amount > 0) { EventManager.Dispatch("playSound", new PlaySoundData("Damage")); }

        if (gluten >= 100) { Die(); }

        glutenBar.SetHealth(gluten);
    }*/

    protected void Die()
    {
        if (!alive) { return; }

        EventManager.Dispatch("fadeToBlack", null);

        Animate.Delay(3f, () => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        alive = false;
    }

    /*private void HandleGlutenUpdate(Data data)
    {
        IntDataBytes casted = (IntDataBytes)data;
        AddGluten((float)casted.IntValue);

    }*/

    // For only disabling tooltip when needed
    private bool previouslyHadRaycast = false;

    protected virtual void _PickItem_Update()
    {
        RaycastHit? storedHit = null;
        

        // Pick
        if (pickedItem == null)
        {

            // Only when player has nothing in its hands
            #region Interact with View
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 3f))
            {
                storedHit = hit;
                if (hit.transform.tag == "InteractableRaycast")
                {
                    Interactable obj = hit.transform.GetComponent<Interactable>();
                    if (obj != null)
                    {
                        previouslyHadRaycast = true;
                        EventManager.Dispatch("setInteractableText", new Bytes.StringDataBytes(obj.GetInteractionDescription()));

                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            obj.Interact(this);
                        }

                    }
                }
                else if (hit.transform.tag == "Pickable")
                {
                    previouslyHadRaycast = true;
                    EventManager.Dispatch("setInteractableText", new Bytes.StringDataBytes("&HAND_GRAB"));
                }
            }

            if (previouslyHadRaycast && storedHit == null)
            {
                previouslyHadRaycast = false;
                EventManager.Dispatch("setInteractableText", null);
            }
            #endregion

            if (Input.GetKeyDown(KeyCode.Mouse0) && storedHit != null)
            {
                if (hit.transform.tag == "Pickable")
                {
                    PickObject(hit.transform.GetComponent<Rigidbody>());
                    return;
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1)) // Drop
        {
            DropObject();
        }

        if (pickedItem != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                YeetusTheFeetus();
                return;
            }
            // Picked item update
            Vector3 dir = pickedItemTarget.position - pickedItem.transform.position;
            dir.Normalize();
            float dis = Vector3.Distance(pickedItemTarget.position, pickedItem.position);
            pickedItem.velocity = dir * (forceTowardPickedObj * dis);
        }

    }

    public void PickObject(Rigidbody newPickedItem)
    {
        pickedItem = newPickedItem;
        pickedItem.freezeRotation = true;
    }

    public void DropObject()
    {
        if (pickedItem == null || !canBeControlled) { return; }

        pickedItem.freezeRotation = false;
        pickedItem = null;
    }

    protected void YeetusTheFeetus()
    {
        Vector3 dir = pickedItemTarget.position - Camera.main.transform.position;
        pickedItem.velocity = dir * 15f / (pickedItem.mass / 2f);
        pickedItem.freezeRotation = false;
        pickedItem = null;
    }

    public class PlayerAnim : BaseAnimState
    {
        public static PlayerAnim Walking = new PlayerAnim("Walk", 1f);
        public static PlayerAnim Running = new PlayerAnim("Walk", 1.5f);
        public static PlayerAnim Idle = new PlayerAnim("Idle", 1f);
        public static PlayerAnim Die = new PlayerAnim("Idle", 1f);

        public PlayerAnim(string pClipName, float pSpeed, int pNbVariations = -1) : base(pClipName, pSpeed, pNbVariations)
        { }
    }

}