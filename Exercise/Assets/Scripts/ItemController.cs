using System;
using UnityEngine;
using UnityEngine.Assertions;

public class ItemController : TriggerController
{
    private static readonly int INVALID_ID = 0;

    [SerializeField] private GameObject m_Item;

    public int UniqueID { get; private set; } = INVALID_ID;

    private void Awake()
    {
        Assert.IsNotNull(m_Item, "Please assign a valid GameObject to the item member.");

        UniqueID = m_Item.GetInstanceID();
    }

    protected override void Interact()
    {
        PickItem();

        CanInteract = false;
    }

    private void PickItem()
    {
        Debug.Log("Quelque chose touche le trigger : " );
        // 1. Enregistrer l'item dans l'Inventaire (Singleton)
        InventorySystem.Instance.StoreItem(UniqueID);

        // 2. Désactiver l'interaction (pour ne pas pouvoir le ramasser 2 fois)
        // Note : Ta méthode Interact() met déjà CanInteract = false, 
        DisableInteraction();

        // 3. Faire disparaître l'objet visuel (le GameObject de la clé)
        if (m_Item != null)
        {
            m_Item.SetActive(false);
        }
    }
}
