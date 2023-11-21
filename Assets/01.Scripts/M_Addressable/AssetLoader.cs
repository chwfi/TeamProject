using UnityEngine;
using System.Threading.Tasks;

public class AssetLoader : MonoBehaviour
{
    [SerializeField]
    private AssetLoaderSO assetLoaderSO;
    public delegate void Notify();

    public static Notify OnLoadComplete;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private async void Start()
    {
        await LoadAsset();

        PoolManager.Instance = new PoolManager(transform);

        OnLoadComplete?.Invoke();

        await MakePooling();
    }

    private async Task LoadAsset()
    {
        foreach (var r in assetLoaderSO.PoolingList)
        {
            var asset = await r.assetRef.LoadAssetAsync<GameObject>().Task;
            assetLoaderSO.LoadingComplete(r.assetRef, asset.name);
        }
    }

    private async Task MakePooling()
    {
        PoolManager.Instance = new PoolManager(transform);
        foreach (var r in assetLoaderSO.PoolingList)
        {
            var obj = (r.assetRef.Asset as GameObject).GetComponent<PoolableMono>();
            if(obj == null)
            {
                Debug.LogWarning($"{r.assetRef.Asset.name} doesnt has PoolableMono Component, skip it");
                continue;
            }
            await Task.Delay(1);
            PoolManager.Instance.CreatePool(r.assetRef.AssetGUID, obj, r.count);
        }
    }
}
