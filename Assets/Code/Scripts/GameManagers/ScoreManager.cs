using System;
using Unity.Netcode;
using UnityEngine;

namespace GameManagers
{
	public class ScoreManager : NetworkBehaviour
	{
		public NetworkVariable<int> ScoreTeam1;
		public NetworkVariable<int> ScoreTeam2;
		
        public static ScoreManager Instance { get; private set; }
        
        private void Awake()
        {
	        if (Instance)
	        {
		        Destroy(gameObject);
		        return;
	        }

	        Instance = this;
	        DontDestroyOnLoad(gameObject);
            
            ScoreTeam1 = new NetworkVariable<int>();
            ScoreTeam2 = new NetworkVariable<int>();
        }
	}
}