using UnityEngine;

[CreateAssetMenu(fileName = "NewShip", menuName = "Ship Selection/Ship")]
public class Ship : ScriptableObject
{
    public string shipName;          
    public GameObject shipModel;     
    public int maxHealth;
    public float handling;           // Velocidad de movimiento vertical
    public float cadence;
    public int speed;              
}
