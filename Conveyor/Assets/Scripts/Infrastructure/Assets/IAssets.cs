using GamePlay.LocationFolder;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Assets
{
    public interface IAssets : IService
    {
        TCreatable Instantiate<TCreatable>(string path, Transform parent) where TCreatable : Object;
        TCreatable Instantiate<TCreatable>(string path, Vector3 at) where TCreatable : Object;
        TCreatable Instantiate<TCreatable>(string path) where TCreatable : Object;
    }
}