using UnityEngine;

public class Tooltip_Popout : MonoBehaviour
{
    [Header("Parameters")] 
    [SerializeField] private float DetectionRange;
    
    [Header("Component")]
    [SerializeField] private GameObject UI_Prompt;
    
    private Transform _playerPos;
    private float distance;
    private bool playerInRange;

    private void Start()
    {
        _playerPos = GameManager.Instance.FetchPlayerObj().transform;
    }
    
    private void Update()
    {
        distance = Vector2.Distance(transform.position, _playerPos.position);
        UI_Prompt.SetActive(DetectionRange > distance);
    }
}
