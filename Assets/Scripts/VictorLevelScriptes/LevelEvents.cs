using UnityEngine;

public class LevelEvents : MonoBehaviour
{
    int numberOfCrystals = 3;

    [SerializeField] GameObject LightN01, LightN02, LightN03;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MyEvents.CrystalOne.AddListener(ActivateLightN01);
		MyEvents.CrystalOne.AddListener(ActivateLightN02);
		MyEvents.CrystalOne.AddListener(ActivateLightN03);
		MyEvents.CrystalOne.AddListener(OpenLevelExit);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateLightN01()
    {

    }

	void ActivateLightN02()
	{

	}

	void ActivateLightN03()
	{

	}

    void OpenLevelExit()
    {
        if(numberOfCrystals <= 0)
        {

        }
    }
}
