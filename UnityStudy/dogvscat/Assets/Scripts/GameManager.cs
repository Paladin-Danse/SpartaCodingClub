using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject dog;
    [SerializeField] GameObject food;
    [SerializeField] GameObject normalCat;
    [SerializeField] GameObject fatCat;
    [SerializeField] GameObject pirateCat;
    [SerializeField] GameObject drop_moneyItem;
    [SerializeField] GameObject retryBtn;
    [SerializeField] Text levelText;
    [SerializeField] GameObject levelFront;
    [SerializeField] Text moneyText;
    public static GameManager instance;
    int level = 0;
    int iMoney = 0;
    float foodFullness = 1.0f;
    public float fullness { get { return foodFullness; } }
    float foodUpgradeFullness = 0f;
    public float upgradeFullness { get { return foodUpgradeFullness; } }
    public int money { get { return iMoney; }}
    bool bGameOver = false;
    public bool OnGameOver { get { return bGameOver; } }
    List<GameObject> foodList = new List<GameObject>();
    List<Cat> catList = new List<Cat>();
    List<moneyItem> dropItemList = new List<moneyItem>();
    [SerializeField] int catFullCnt = 0;
    float fFoodShotCooltime = 0.2f;
    float fFoodCooltimeUpgrade = 0f;
    public float FoodShotCooltime { get {  return fFoodShotCooltime - (fFoodCooltimeUpgrade); }}
    float fCatSpawnCooltime = 1.0f;
    
    private IEnumerator foodCorout = null;
    private IEnumerator catCorout = null;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitGame();
    }
    void InitGame()
    {
        Time.timeScale = 1.0f;
        iMoney = 0;
        moneyText.text = iMoney.ToString();

        if (foodList.Count > 0)    foodList.Clear();
        if (catList.Count > 0)     catList.Clear();
        if (dropItemList.Count > 0) dropItemList.Clear();
    }

    private void FixedUpdate()
    {
        if (foodCorout == null && bGameOver == false)
        {
            foodCorout = makeFood();
            StartCoroutine(foodCorout);
        }
        if(catCorout == null && bGameOver == false)
        {
            catCorout = makeCat();
            StartCoroutine(catCorout);
        }
    }

    IEnumerator makeFood()
    {
        if (shopManager.instance.sUpgradeState.onDoubleShot == false)
        {
            Vector3 setFoodPos = dog.transform.position + new Vector3(0, 2.0f, 0);
            objectPooling(foodList, setFoodPos);
        }
        else
        {
            Vector3 setFoodPos = dog.transform.position + new Vector3(-1.0f, 2.0f, 0);
            objectPooling(foodList, setFoodPos);

            setFoodPos = dog.transform.position + new Vector3(1.0f, 2.0f, 0);
            objectPooling(foodList, setFoodPos);
        }
        yield return new WaitForSeconds(FoodShotCooltime);

        foodCorout = null;
    }
    //level에 맞춰 고양이를 얼마나 만들고 어떤 고양이를 만들어야 할지 판단하는 단계
    IEnumerator makeCat()
    {
        CatPooling(normalCat);

        if (level >= 1)
        {
            float p = Random.Range(0, 10);
            if (level <= 3)
            {
                if (p < 2) CatPooling(normalCat);
            }
            if (level >= 4 && level <= 5)
            {
                if (p < 5) CatPooling(normalCat);
            }
            if (level >= 6)
            {
                if (p < 6) CatPooling(normalCat);
                if (p < 3) CatPooling(fatCat);
            }
            if (level >= 10)
            {
                if (p < 5) CatPooling(pirateCat);
            }
            if(level >= 15)
            {
                if (p < 8) CatPooling(normalCat);
                if (p < 5) CatPooling(pirateCat);
                if (p < 3) CatPooling(fatCat);
            }
        }
        yield return new WaitForSeconds(fCatSpawnCooltime);

        catCorout = null;
    }

    public void GameOver()
    {
        retryBtn.SetActive(true);
        Time.timeScale = 0;
        bGameOver = true;
    }
    public void addCat()
    {
        catFullCnt++;
        level = catFullCnt / 5;
        
        levelText.text = level <= 99 ? level.ToString() : "99+";
        levelFront.transform.localScale = new Vector3((catFullCnt - level * 5) / 5.0f, 1.0f, 1.0f);
    }
    //Instantiate나 SetActive를 사용하여 cat을 직접 만드는 단계
    public void CatPooling(GameObject m_Cat)
    {
        var catObj = catList.Find(item =>
        {
            if (item.gameObject.activeSelf == false &&
                item.getType.Equals(m_Cat.GetComponent<Cat>().getType)) return true;
            return false;
        });

        if (catObj == null)
        {
            catObj = Instantiate(m_Cat).GetComponent<Cat>();
            catList.Add(catObj);
        }
        catObj.gameObject.SetActive(true);
    }

    public void makeMoneyItem(Vector3 catPos)
    {
        var itemObj = dropItemList.Find(item =>
        {
            if (item.gameObject.activeSelf == false) return true;
            return false;
        });

        if (itemObj == null)
        {
            itemObj = Instantiate(drop_moneyItem).GetComponent<moneyItem>();
            dropItemList.Add(itemObj);
        }
        itemObj.transform.position = catPos;
        itemObj.gameObject.SetActive(true);
        
    }
    public void getMoney(int mMoney)
    {
        iMoney += mMoney;
        moneyText.text = iMoney.ToString();
    }
    public void lostMoney(int mMoney)
    {
        iMoney -= mMoney;
        moneyText.text = iMoney.ToString();
    }

    private void objectPooling(List<GameObject> mList, Vector3 setObjectPos)
    {
        var obj = foodList.Find(item =>
        {
            if (item.gameObject.activeSelf == false) return true;
            return false;
        });
        if (obj == null)
        {
            obj = Instantiate(food, setObjectPos, Quaternion.identity);
            foodList.Add(obj);
        }
        obj.transform.position = setObjectPos;
        obj.SetActive(true);
    }
    public void foodUpgrade()
    {
        foodUpgradeFullness = (float)shopManager.instance.sUpgradeState.fullnessPowerLvl * 0.5f;
        foreach(GameObject food in foodList)
        {
            food.GetComponent<Food>().SetfoodStatus();
        }

        fFoodCooltimeUpgrade = (float)shopManager.instance.sUpgradeState.foodSpeedLvl * 0.01f;
    }
}