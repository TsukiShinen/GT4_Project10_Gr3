using System;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.Netcode;
using UnityEngine;

public class ShootServer : NetworkBehaviour
{
    [SerializeField] private List<ProjectilePrefabDatabase> m_ProjectilePrefabDatabase;

    public void Shoot(int pBulletsPerShotFinal, Vector3 pShotDirection, int pProjectileid, Vector3 pWeaponMuzzlePosition, ServerRpcParams pServerRRpcParams = default)
    {
        if (IsOwner)
            ShootServerRpc(pBulletsPerShotFinal, pShotDirection, pProjectileid, pWeaponMuzzlePosition);
    }

    [ServerRpc]
    public void ShootServerRpc(int pBulletsPerShotFinal, Vector3 pShotDirection, int pProjectileid, Vector3 pWeaponMuzzlePosition, ServerRpcParams pServerRRpcParams = default)
    {

        // spawn all bullets with random direction
        for (int i = 0; i < pBulletsPerShotFinal; i++)
        {
            Vector3 shotDirection = pShotDirection;
            ProjectileBase newProjectile = Instantiate(m_ProjectilePrefabDatabase.Find(data => data.ID == pProjectileid).ProjectileBase, pWeaponMuzzlePosition,
                Quaternion.LookRotation(shotDirection));
            newProjectile.GetComponent<NetworkObject>().Spawn();

            //newProjectile.Shoot(MultiplayerManager.Instance.GetPlayerGameObject(pServerRRpcParams.Receive.SenderClientId).GetComponentInChildren<WeaponController>());
        }
    }

    [Serializable]
    private struct ProjectilePrefabDatabase
    {
        public int ID;
        public ProjectileBase ProjectileBase;
    }
}