// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.Networking;
// using Enjin.SDK.Core;
// using GraphQlClient.Core;
// using TMPro;


// public class AccountManager : MonoBehaviour
// {
//     public GraphApi enjin;

//     public TMP_InputField emailInput;
//     public TMP_InputField passwordInput;
//     private readonly string PLATFORM_URL = "https://kovan.cloud.enjin.io/";
//     private readonly string DEVELOPER_USERNAME = "MemphisGameDevelopers@gmail.com";
//     private readonly string DEVELOPER_PASSWORD = "LDK3mj9GM9yPtWw";
//     private readonly int APP_ID = 3846;
//     private readonly string APP_SECRET = "pTCt4CekupNKJPBa62q8Ywu4QryQmkfBjclUe7Rw";
//     //public User player, admin;

//     // Start is called before the first frame update
//     void Start()
//     {
//        Enjin.SDK.Core.Enjin.StartPlatform(PLATFORM_URL, APP_ID, APP_SECRET);

//     //    GraphApi.Query AuthApp = enjin.GetQueryByName("RetrieveAppAccessToken", GraphApi.Query.Type.Query);
//     //    AuthApp.SetArgs(new{id = APP_ID, secret = APP_SECRET});
//     //    UnityWebRequest request = await enjin.Post(AuthApp); 

      
//     //    enjin.SetAuthToken(request.downloadHandler.text);

        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     // public Identity GetId(User user)
//     // {
//     //     foreach (Identity identity in user.identities)
//     //     {
//     //         if (identity.app.id == APP_ID)
//     //         {
//     //             return identity;
//     //         }
//     //         else
//     //         {
//     //             Debug.Log("No Id for this game found!");
//     //             return null;
//     //         }
//     //     }
//     //     return null;
//     // }

//     public async void Login()
//     {
//         string email = emailInput.text;
//         string password = passwordInput.text;

//         GraphApi.Query loginUser = enjin.GetQueryByName("Login", GraphApi.Query.Type.Query);
//         loginUser.SetArgs(new{email = email, password = password});

//         UnityWebRequest request = await enjin.Post(loginUser);

        

//         //print(HttpHandler.FormatJson(request.downloadHandler.text));


//         //-Enjin.SDK.Core.Enjin.VerifyLogin(email, password);
        
//         if (Enjin.SDK.Core.Enjin.LoginState == LoginState.VALID)
//         {  
//             Debug.Log("Successfully Logged In!");

//             //Start Game

//             StartGame();
//         }
        
//     }

//     private void StartGame()
//     {
//         SceneManager.LoadScene(1);
//     }
// }
