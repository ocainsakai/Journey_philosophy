using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject itemSpawn;
    [SerializeField] private bool oneTimeUse = true;
    [SerializeField] private List<BaseCondition> conditionAssets;

    private List<IInteractionCondition> conditions = new List<IInteractionCondition>();

    public UnityEvent OnInteract;

    private void Awake()
    {
        // convert ScriptableObject thành IInteractionCondition
        foreach (var obj in conditionAssets)
        {
            if (obj is IInteractionCondition cond)
                conditions.Add(cond);
        }
    }

    private bool CheckConditions()
    {
        foreach (var cond in conditions)
        {
            if (!cond.IsMet(PlayerController.Instance))
                return false;
        }
        return true;
    }

    public string GetInteractionPrompt()
    {
        foreach (var cond in conditions)
        {
            if (!cond.IsMet(PlayerController.Instance))
                return cond.GetFailMessage();
        }
        return "Press F to interact";
    }

    public void Interact()
    {
        if (!CheckConditions()) return;

        if (itemSpawn != null)
            SpawnItem();

        OnInteract?.Invoke();

        if (oneTimeUse)
            gameObject.SetActive(false);
    }

    public GameObject SpawnItem()
    {
        return Instantiate(itemSpawn, transform.position, Quaternion.identity);
    }
}
