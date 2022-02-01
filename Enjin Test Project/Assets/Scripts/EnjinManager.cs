using System.Collections;
using UnityEngine;
using Enjin.SDK.DataTypes;

namespace Enjin.SDK.Core
{
    [System.Serializable]
    public class API_Credentials
    {
        public int APP_ID;
        public string APP_SECRET;
        public string ADMIN_USER;
    }
    public class EnjinManager : MonoBehaviour
    {

        public API_Credentials testnet, mainnet, jumpnet;

        [HideInInspector]
        public API_Credentials _api;

        [SerializeField] private EnjinUIManager _enjinUIManager;

        public User _currentEnjinUser = null;
        public User _adminAccount = null;
        bool _isConnecting = false;

        private void Awake()
        {
            Enjin.IsDebugLogActive = true;
        }

        public void SetPlatform(bool test)
        {
            print("testnet: " + test);
            _api = test ? testnet : mainnet;
        }

        private void Start()
        {
            _enjinUIManager.RegisterAppLoginEvent(() =>
            {
                if (Enjin.LoginState == LoginState.VALID)
                    return;

                if (_isConnecting)
                    return;

                _isConnecting = true;
                StartCoroutine(AppLoginRoutine());
            });

            _enjinUIManager.RegisterUserLoginEvent(() =>
            {
                if (Enjin.LoginState != LoginState.VALID)
                    return;

                _currentEnjinUser = Enjin.GetUser(_enjinUIManager.UserName);
                _adminAccount = Enjin.GetUser(_api.ADMIN_USER);
                _enjinUIManager.AccessToken = Enjin.AccessToken;

                Debug.Log($"[Logined User ID] {_currentEnjinUser.id}");
                Debug.Log($"[Logined User name] {_currentEnjinUser.name}");

                _enjinUIManager.UserName = _currentEnjinUser.name;
                _enjinUIManager.DisableUserLoginUI();
                EnjinWallet.Instance.PLAYER_ADDRESS = _currentEnjinUser.identities[0].wallet.ethAddress;
                EnjinWallet.Instance.GetBalances();

            });

            _enjinUIManager.RegisterGetIdentityEvent(() =>
            {
                if (Enjin.LoginState != LoginState.VALID)
                    return;

                for (int i = 0; i < _currentEnjinUser.identities.Length; ++i)
                {
                    Debug.Log($"[{i} Identity ID] {_currentEnjinUser.identities[i].id}");
                    Debug.Log($"[{i} Identity linking Code] {_currentEnjinUser.identities[i].linkingCode}");
                    Debug.Log($"[{i} Identity Wallet :: Eth Address] {_currentEnjinUser.identities[i].wallet.ethAddress}");
                }

                EnjinWallet.Instance.PLAYER_ADDRESS = _currentEnjinUser.identities[0].wallet.ethAddress;
            });

            // TO DO : BindEvent from pusher
        }


        // this is an aleternative to using async function, 
        // you can implement this, using coroutine and this is an example of it
        private IEnumerator AppLoginRoutine()
        {
            Enjin.StartPlatform(_enjinUIManager.EnjinPlatformURL,
                _api.APP_ID, _api.APP_SECRET);

            int tick = 0;
            YieldInstruction waitASecond = new WaitForSeconds(1f);

            // try to log in within 10 seconds or 10 ticks
            while (tick < 10)
            {
                // if we successfully logged in display out, enjin appIDm, 
                // disable app login UI, and end enable user is logged in ui
                if (Enjin.LoginState == LoginState.VALID)
                {
                    Debug.Log("App auth success");
                    _enjinUIManager.EnjinAppId = Enjin.AppID;
                    _enjinUIManager.DisableAppLoginUI();
                    _enjinUIManager.EnableUserLoginUI();
                    yield break;
                }

                // increase tick and wait for another second before trying
                tick++;
                yield return waitASecond;
            }

            Debug.Log("App auth Failed");
            _isConnecting = false;

            yield return null;
        }

    }
}

