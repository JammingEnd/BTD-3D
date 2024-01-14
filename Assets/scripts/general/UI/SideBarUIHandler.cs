using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.scripts.towers.visuals;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RangeVisualizer))]
public class SideBarUIHandler : MonoBehaviour
{
    public GameObject ButtonPrefab;

    int InspectingLayer = 0; //0 is nothing, 1 is towers, 2 is Upgrades, 3 is Powers

    private GameObject _panel;
    private Camera _mainCam;

    public LayerMask HitBoxFilter;

    private List<GameObject> _spawnedUiElements = new List<GameObject>();

    private UpgradePath[] upgradePaths;

    private RangeVisualizer _rangeVisualizer;

    private bool _isButtonPress;

    private void Start()
    {
       _mainCam = this.GetComponentInChildren<Camera>();
        _panel = GameObject.FindGameObjectWithTag("SideBar");
       _rangeVisualizer = this.GetComponent<RangeVisualizer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
           
          
            Select();
          
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Button_onClick(0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Button_onClick(1);
        }

    }

    void Select()
    {

        bool isOverButton = UIDetector.DetectIfOverUpgradeButtons(9);
        Debug.Log(isOverButton);
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, HitBoxFilter, (int)QueryTriggerInteraction.Ignore))
        {
            if(hit.collider.gameObject.layer == 0)
            {
                ClearElements();
                InspectingLayer = 2;
                upgradePaths = hit.collider.GetComponentsInParent<UpgradePath>();
              
                FillSideBarWithUpgrades(upgradePaths);
               
                if (upgradePaths[0].gameObject.TryGetComponent(out AttackingDef attackingDef) )
                {
                    RenderRange(attackingDef);
                }
            }
            
           

        }
        else if (!isOverButton)
        {
            ClearElements() ;
            SetLineRendered(false);
            
            
        }
       
        


    }
    #region Upgrades
    public void FillSideBarWithUpgrades(UpgradePath[] upgradePaths)
    {
       
        InspectingLayer = 2;
        for (int i = 0; i < upgradePaths.Length; i++)
        {
            if (upgradePaths[i]._currentLevel < upgradePaths[i].Upgrades.Count)
            {
                var buttonObj = Instantiate(ButtonPrefab, _panel.transform); //create button
                _spawnedUiElements.Add(buttonObj);  //save elements in list
                AssignUpgradeBtn(buttonObj, i); //add listener to spawned index (so 0 = first upgrade path)

                var reference = upgradePaths[i].Upgrades[upgradePaths[i]._currentLevel]; //retrieve upgradeObject from list via the currentLevel index
                SetText(reference.Title, reference.Description, reference.Price.ToString(), buttonObj); //set the text
            }
            else
            {
                var buttonObj = Instantiate(ButtonPrefab, _panel.transform); //create button
                _spawnedUiElements.Add(buttonObj);  //save elements in list
                AssignUpgradeBtn(buttonObj, i, true); //add listener to spawned index (so 0 = first upgrade path)
                SetText("Sorry man", "End of the upgrade Path", "You need 4 dabloons", buttonObj); //set the text
            }

        }
        
    }

    void SetText(string name, string desc, string price, GameObject button)
    {
       var textField = button.GetComponentInChildren<TMP_Text>();
        textField.text = $"{name} \n {desc} \n {price}";
    }

    void AssignUpgradeBtn(GameObject Btn, int index, bool ReachedMax = false)
    {
        if(Btn.TryGetComponent(out Button button))
        {
            if(!ReachedMax)
            {
                button.onClick.AddListener(() => Button_onClick(index));
            }
           
        }
    }
    private UpgradePath _tempPathForListener;
    void Button_onClick(int index)
    {
        _isButtonPress = true;
        _tempPathForListener = upgradePaths[index];
        _tempPathForListener.OnUpgrade_OnClick();
        if (upgradePaths[0].gameObject.TryGetComponent(out AttackingDef attackingDef))
        {
            RenderRange(attackingDef);
        }
        ClearElements();
        _spawnedUiElements.Distinct();
        FillSideBarWithUpgrades(upgradePaths);
    }

    #endregion

    #region on_selecting

    private void RenderRange(AttackingDef def)
    {
        
        SetLineRendered(true);
        _rangeVisualizer.RenderCircle(def.CurrentRange, def.transform.position);
    }
    private void SetLineRendered(bool state)
    {
        if (TryGetComponent(out LineRenderer lineRenderer))
        {
            lineRenderer.enabled = state;
        }
    }
    
    #endregion
    void ClearElements()
    {
        if (_spawnedUiElements.Count > 0)
        {
            for (int i = 0; i < _spawnedUiElements.Count; i++)
            {
                
                Destroy(_spawnedUiElements[i]);
                
            }
            _spawnedUiElements.Clear();
            //_spawnedUiElements.RemoveAll(x => !x);
        }
    }

   
}
