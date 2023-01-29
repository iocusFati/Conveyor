using System;
using GamePlay.PlayerFolder;
using Infrastructure.Assets;
using Infrastructure.Bootstrapper;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IAssets _assetProvider;
        private readonly PlayerStaticData _playerStaticData;
        private readonly Vector3 _originRotation;

        public event  Action<Player> OnPlayerCreated;
        public event  Action<IPlayerAnimation> OnPlayerAnimationCreated;

        public PlayerFactory(IAssets assets, PlayerStaticData playerStaticData)
        {
            _assetProvider = assets;
            _playerStaticData = playerStaticData;

            _originRotation = playerStaticData.OriginRotation;
        }

        public void CreatePlayer(Vector3 at)
        {
            var player = _assetProvider.Instantiate<Player>(AssetPaths.PlayerPath, at);
            var playerAnimation = player.GetComponent<PlayerAnimation>();

            player.Construct(playerAnimation);
            playerAnimation.Construct(_playerStaticData);
            player.transform.localRotation = Quaternion.Euler(_originRotation);

            OnPlayerCreated.Invoke(player);
            OnPlayerAnimationCreated.Invoke(playerAnimation);
        }
    }
}