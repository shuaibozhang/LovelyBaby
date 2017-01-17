using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchieveView : MonoBehaviour {
    public GameObject _acievenItemObject;
    public GameObject _showItemLayer;
	// Use this for initialization
	void Start () {
        initItems();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void initItems()
    {
        var temp  = AchievementMgr.getCurAchievementMgr().getSortList();
        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i].compliteday != -1)
            {
                var item = Instantiate(_acievenItemObject);
                item.transform.SetParent(_showItemLayer.transform);
                item.transform.localScale = new Vector3(1f, 1f, 1f);

                var itemsprite = item.transform.GetChild(0).gameObject.GetComponent<Image>();
                itemsprite.sprite = Resources.Load<Sprite>("ui/achieve/achieve_name_" + temp[i].id);
                itemsprite.SetNativeSize();

            }
        }
    }

    public void btnOnCancle()
    {
        Destroy(gameObject);
    }
}
