using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : MonoBehaviour
{
    [SerializeField] private string mothFood;
    [SerializeField] private int change = 10; // крок голоду
    [SerializeField] private int sleepTime = 5; // час до появи голоду
    public int calories = 10;

    public int stage = 0;

    [SerializeField] private int caloriesNeeded = 0;
    private int howHungryItIs = 0;
    private bool isFoodNeeded = false;

    [Header("Stages & UI")]
    [SerializeField] private GameObject hungryLeaf;
    [SerializeField] private GameObject satisfaction;
    [SerializeField] private GameObject[] stages; // Egg, Warm, cocon, Moth

    private int currentStage = 0;

    [SerializeField] private List<Transform> availablePlaces;
    private bool isEvolutionCompleted = false;

    void Start()
    {
        // Знаходимо всі об'єкти з тегом "LivingPoint"
        GameObject[] foundPoints = GameObject.FindGameObjectsWithTag("LivingPoint");

        // Додаємо їх Transform у список availablePlaces
        foreach (GameObject point in foundPoints)
        {
            availablePlaces.Add(point.transform);
        }
        // Вимкнути всі стадії, крім першої
        for (int i = 0; i < stages.Length; i++)
            stages[i].SetActive(i == 0);

        // Знайти відповідні об'єкти
        if (mothFood == "Leaf")
            hungryLeaf = transform.Find("hungry leaf")?.gameObject;
        else if (mothFood == "Wax")
            hungryLeaf = transform.Find("hungry wax sign")?.gameObject;

        satisfaction = transform.Find("satisfaction")?.gameObject;

        if (hungryLeaf != null) hungryLeaf.SetActive(false);
        if (satisfaction != null) satisfaction.SetActive(false);

        // Запустити появу голоду через затримку
        StartCoroutine(WaitUntilHungry());
    }

    private void Update()
    {
        if (isEvolutionCompleted)
        {
            isEvolutionCompleted = false; // щоб не повторювати
            FindPlace();
        }
    }

    private void FindPlace()
    {
        if (availablePlaces == null || availablePlaces.Count == 0)
        {
            Debug.LogWarning("No available places set!");
            return;
        }

        int randomIndex = Random.Range(0, availablePlaces.Count);
        Transform chosenPlace = availablePlaces[randomIndex];

        transform.position = chosenPlace.position;

        float randomYRotation = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0, randomYRotation, 0);
    }

    private IEnumerator WaitUntilHungry()
    {
        yield return new WaitForSecondsRealtime(sleepTime);
        isFoodNeeded = true;
        caloriesNeeded += change; // збільшення потреби в їжі
        if (hungryLeaf != null)
            hungryLeaf.SetActive(true);
    }

    private IEnumerator ActivateTemporarily(GameObject obj, float duration)
    {
        obj.SetActive(true);
        yield return new WaitForSecondsRealtime(duration);
        obj.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (isFoodNeeded && !isEvolutionCompleted)
        {
            if (howHungryItIs < caloriesNeeded)
            {
                InventoryManager inventory = FindObjectOfType<InventoryManager>();
                for (int i = 0; i < inventory.itemSlot.Length; i++)
                {
                    if (inventory.itemSlot[i].itemName == mothFood && inventory.itemSlot[i].quantity > 0)
                    {
                        howHungryItIs += calories;
                        inventory.itemSlot[i].quantity--;

                        if (satisfaction != null)
                            StartCoroutine(ActivateTemporarily(satisfaction, 2f));

                        if (howHungryItIs >= caloriesNeeded)
                        {
                            isFoodNeeded = false;
                            howHungryItIs = 0;

                            if (hungryLeaf != null)
                                hungryLeaf.SetActive(false);

                            // Перехід на наступну стадію
                            if (currentStage < stages.Length - 1)
                            {
                                stages[currentStage].SetActive(false);
                                currentStage++;
                                stages[currentStage].SetActive(true);

                                // Запускаємо таймер голоду знову
                                StartCoroutine(WaitUntilHungry());
                            } else
                            {
                                isEvolutionCompleted = true;
                                stage = 4;
                            }
                        }

                        break;
                    }
                }
            }
        }
    }
}
