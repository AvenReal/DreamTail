using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movements")]
    public int MoveSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    # region Updates
    void Update()
    {
        MoveUpdate();
    }

    void MoveUpdate()
    {
        var horizontalMove = Input.GetAxis("Horizontal");
        if (horizontalMove != 0)
        {
            transform.position += new Vector3(horizontalMove * MoveSpeed * Time.deltaTime , 0, 0);
        }
    }
    # endregion
}
