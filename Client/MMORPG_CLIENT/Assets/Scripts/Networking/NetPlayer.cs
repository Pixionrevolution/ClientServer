using UnityEngine;
using System.Threading;
using UnityEngine.UI;

class NetPlayer : MonoBehaviour
{
    public static NetPlayer instance;

    public Transform _player;
    public int Index;
    public Vector3 destinationPos; // Where we want the player to walk to
    public float destinationDistance; // Distance between player and destination position
    

    public Text PlayerIndex;

    private Vector3 moveDir;
    private CharacterController controller;
    private float moveSpeed;
    private Vector3 ctargetPos;
    private Vector3 targetPos;
    private Quaternion targetRot;


    private void Awake()
    {
        instance = this;
    }
    
    private void Update()
    {
        if (this.Index == Globals.instance.MyIndex)
        {
            MovePlayer();
        }      
    }

    private void Start()
    {
        if (Index == Globals.instance.MyIndex)
        {
            destinationPos = new Vector3(Network.instanceP[Index].posX, Network.instanceP[Index].posY, Network.instanceP[Index].posZ);
            controller = this.GetComponent<CharacterController>();
            
            // Camera controller
            CameraController.instance.target = gameObject;
            CameraController.instance.SetupCam();
            // ""
            PlayerIndex.text = "Player " + Index.ToString() + " : " + Network.instanceP[Index].Username;

        }
    }


    void MovePlayer()
    {

        if (Index != Globals.instance.MyIndex)
        {
            return;
        }

        destinationDistance = Vector3.Distance(destinationPos, transform.position);
        
        if(destinationDistance <= 1.3f)
        {
            moveSpeed = 0;
        }    
        else if (destinationDistance> 1.3f)
        {
            moveSpeed = 3.5f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitDist;

            if (Physics.Raycast(ray, out hitDist, 100, 1 << 11))
            {
                if (hitDist.collider.tag != "Player")
                {
                    Vector3 targetPoint = Vector3.zero;
                    targetPoint.x = hitDist.point.x;
                    targetPoint.y = transform.position.y;
                    targetPoint.z = hitDist.point.z;
                    destinationPos = hitDist.point;
                    targetRot = Quaternion.LookRotation(targetPoint - transform.position);
                }
            }

            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit h;

            if (Physics.Raycast(r, out h, 100, 1 << 11))
            {
                if (h.collider.tag == "Ground")
                {
                    // SPawn Mouse Effect
                    // Instantiate (mouseGraphicPrefab)
                }
            }
        }
            // Move player
            if(controller.isGrounded)
            {
                moveDir = Vector3.zero;
                moveDir = transform.TransformDirection(Vector3.forward * moveSpeed);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 25);
            }

        moveDir.y -= 20 * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
        

       

        float x = _player.position.x;
        float y = _player.position.y;
        float z = _player.position.z;

        float rotX = _player.rotation.x;
        float rotY = _player.rotation.y;
        float rotZ = _player.rotation.z;
        float rotW = _player.rotation.w;

        ClientSendData.instance.SendMovement(x, y, z, rotX, rotY, rotZ, rotW);
        

    }

    public void DestroyPrefab()
    {

       
            Destroy(gameObject);
        
        
    }

}

