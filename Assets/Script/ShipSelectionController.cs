using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using UnityEngine.SceneManagement;

public class ShipSelectionController : MonoBehaviour
{
    public Ship[] availableShips;    
    public int selectedShipIndex = 0; 
    public GameObject shipDisplay;   

    [Header("UI References")]
    [SerializeField] TMP_Text shipNameText; 
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text cadencia;
    [SerializeField] TMP_Text sped;

    [SerializeField] float rotationSpeed = 30f;
    public GameObject selectorcontrol,selectornave;

    private void Start()
    {

        if (shipDisplay != null)
        {
            Destroy(shipDisplay);
        }
        DisplayShip(selectedShipIndex);
        UpdateShipInfoUI();
    }
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    public void NextShip()
    {
        selectedShipIndex = (selectedShipIndex + 1) % availableShips.Length;
        DisplayShip(selectedShipIndex);
    }

    public void PreviousShip()
    {
        selectedShipIndex = (selectedShipIndex - 1 + availableShips.Length) % availableShips.Length;
        DisplayShip(selectedShipIndex);
    }

    private void DisplayShip(int index)
    {
        if (shipDisplay != null)
        {
            Destroy(shipDisplay);
        }

        Quaternion initialRotation = Quaternion.Euler(15f, 0f, 0f); 

        shipDisplay = Instantiate(
            availableShips[index].shipModel,
            transform.position,
            initialRotation,          
            transform                
        );
        UpdateShipInfoUI();
    }

    public void ConfirmSelection()
    {
        GameManager.Instance.selectedShip = availableShips[selectedShipIndex];
        selectorcontrol.SetActive(true);
        selectornave.SetActive(false);
        
    }

   
    private void UpdateShipInfoUI()
    {
        Ship currentShip = availableShips[selectedShipIndex];

        if (shipNameText != null)
            shipNameText.text = currentShip.shipName;

        if (healthText != null)
            healthText.text = "Salud: " + currentShip.maxHealth.ToString();

        if (speedText != null)
            speedText.text = "Manejo: " + currentShip.handling.ToString("F1");
        if (cadencia != null)
            cadencia.text = "Cadencia: " + currentShip.cadence.ToString("F1");
        if (sped != null)
            sped.text = "Velocidad: " + currentShip.speed.ToString("F1");

    }


}
