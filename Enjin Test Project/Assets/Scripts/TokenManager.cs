// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Enjin.SDK.Core;
// using Enjin.SDK.DataTypes;
// using Enjin.SDK.GraphQL;
// using Enjin.SDK.Template;
// using Enjin.SDK.Utility;

// public class TokenManager : MonoBehaviour
// {
//     public AccountManager accounts;
//     private Identity playerId, adminId; 

//     // Start is called before the first frame update
//     void Start()
//     {
//         adminId = accounts.GetId(accounts.admin);
//         playerId = accounts.GetId(accounts.player);
//     }

    
//     public void Mint(string tokenId)
//     {
//         CryptoItem item = Enjin.GetCryptoItem(tokenId);
//         string reserveCount = item.reserve;
//         int developerBalance = Enjin.GetCryptoItemBalance(adminId.id, tokenId);
//         if (!reserveCount.Equals("0"))
//         {
//             //pending += 1;
//             Enjin.MintFungibleItem(adminId.id, new string[] {playerId.ethereum_address} , tokenId, 1,
//             (requestEvent) =>
//             {
//                 if (requestEvent.event_type.Equals("tx_executed"))
//                 {
//                     // pending -= 1;
//                     // count += 1;
//                     // pendingActions.Add(() =>
//                     // {
//                     //     inventory.text = (tokenName + "\nYou have: " + count + "\nCurrently pending: " + pending);
//                     // });
//                     Debug.Log("Token has been sent");
//                 }
//             });
            
//             Debug.Log("Token is currently Pending");
//         }
//     }

//     public void Transfer()
//     {
       
//     }
// }
