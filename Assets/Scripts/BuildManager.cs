using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    [SerializeField] private GameObject _turret1Prefab;
    [SerializeField] private GameObject _turret2Prefab;
    private TurretBlueprints _turretToBuild;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }

    public bool CanBuild { get { return _turretToBuild != null; } }
    public bool HasMoney { get { return GameManager.Instance.Coins >= _turretToBuild.cost; } }

    public void BuildTurretOn(Node node)
    {
        if (GameManager.Instance.Coins < _turretToBuild.cost)
        {
            return;
        }
        GameManager.Instance.Coins -= _turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(_turretToBuild.prefab, node.transform.position + node.positionOffset, node.transform.rotation);
        node.turret = turret;
    }

    public void SelectTurretToBuild(TurretBlueprints turret)
    {
        _turretToBuild = turret;
    }
}