using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Components")] 
    public Rigidbody2D Rigidbody2D;
    public ParticleSystem JumpingParticle;
    public ParticleSystem DashingParticle;
    public BoxCollider2D WallCollider;
    public BoxCollider2D FloorCollider;
    
    [Header("Movements")]
    public int MoveSpeed;

    [Header("Jumps")]
    public int JumpBoost;
    public int MaxNbOfJumps;
    public int NbOfJumps;

    [Header("Dashes")] 
    public int DashBoost;
    public int MaxNbOfDashes;
    public int NbOfDashes;
    private bool DashDirection;
    
    [Header("WallJumps")]
    public int WallJumpBoost;
    public int MaxNbOfWallJumps;
    public int NbOfWallJumps;

    public static PlayerScript Instance;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    # region Updates
    void Update()
    {
        MoveUpdate();
        JumpUpdate();
        DashUpdate();
    }

    void MoveUpdate()
    {
        var horizontalMove = Input.GetAxis("Horizontal");
        DashDirection = horizontalMove > 0;
        if (horizontalMove != 0)
        {
            transform.position += new Vector3(horizontalMove * MoveSpeed * Time.deltaTime , 0, 0);
        }
    }

    void JumpUpdate()
    {
        if(JumpingParticle.isPlaying && Mathf.Abs(Rigidbody2D.linearVelocity.y) < 0.1f)
            JumpingParticle.Stop();
        
        if (!(Input.GetKeyDown(KeyCode.Space) && NbOfJumps > 0))
            return;
        
        JumpingParticle.Play();
        Rigidbody2D.AddForce(new Vector2(0, JumpBoost), ForceMode2D.Impulse);
        NbOfJumps--;
    }

    void DashUpdate()
    {
        if(DashingParticle.isPlaying && Mathf.Abs( Rigidbody2D.linearVelocity.x) < 3)
            DashingParticle.Stop();
        
        
        if(!(Input.GetKeyDown(KeyCode.E) && NbOfDashes > 0))
            return;
        DashingParticle.gameObject.transform.rotation = Quaternion.Euler(0, DashDirection ? 90 : -90, 0);
        DashingParticle.Play();
        Rigidbody2D.AddForce(new Vector2((DashDirection ? 1 : -1) * DashBoost, 0), ForceMode2D.Impulse);
        NbOfDashes--;

    }
    
    # endregion


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            NbOfJumps = MaxNbOfJumps;
            NbOfDashes = MaxNbOfDashes;
            NbOfWallJumps = MaxNbOfWallJumps;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
}
