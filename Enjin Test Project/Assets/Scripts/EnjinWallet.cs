using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;

namespace Enjin.SDK.Core
{
    public class EnjinWallet : MonoBehaviour
    {
        public EnjinManager Manager;
        public Wallet Player_Wallet;
        public TextMeshProUGUI ethBalance, enjBalance;       

        int PLAYER_IDENTITY_ID;
        string APP_LINK_CODE;
        public string PLAYER_ADDRESS;

        public static EnjinWallet Instance;
        public CryptoCard CardPrefab;
        public List<CryptoCard> cryptoList = new List<CryptoCard>();

        public Wallet UserWallet;

        private void Awake()
        {
           if(Instance == null)
            Instance = this;
        }

        public void GetBalances()
        {
            UserWallet = Enjin.GetWalletBalances(PLAYER_ADDRESS);
            ethBalance.text = UserWallet.ethBalance.ToString();
            enjBalance.text = UserWallet.enjBalance.ToString();
            var balances = UserWallet.balances;

            foreach (var bal in balances)
            {
                var card = Instantiate(CardPrefab, transform);
                card.item = Enjin.GetToken(bal.id);                
                StartCoroutine(card.item.GetMetadata((response)=>
                {  
                    StartCoroutine(card.item.GetImage((response)=>
                    {
                        card.Icon.sprite = response.image;
                        card.Description.text = card.item.metadata.description;
                    })); 
                }));   
                card.quantity = bal.value;
                card.Title.text = card.item.name;
                card.gameObject.SetActive(true); 
                cryptoList.Add(card);
            }
        }
       

        IEnumerator MintItem(string itemName, int quantity)
        {
            string itemId = itemName;
            PLAYER_ADDRESS = Manager._currentEnjinUser.identities[0].wallet.ToString();
            print(Enjin.GetCryptoItemURI(itemId));

            Enjin.MintFungibleItem(Manager._adminAccount.id, new string[] { PLAYER_ADDRESS }, itemId, quantity,
                (requestData) =>
                {

                    print("Item Minted::" + itemName);

                }, true);

            yield return null;
        }


        IEnumerator SendItem(string itemName, int quantiy)
        {

            PLAYER_IDENTITY_ID = Manager._currentEnjinUser.identities[0].id;            
            string itemId = itemName;
            Enjin.SendCryptoItemRequest(PLAYER_IDENTITY_ID, itemName, Manager._adminAccount.id, quantiy, (requestData) =>
            {
                print("Item Sent::" + itemName);
            }, true);



            yield return null;
        }


        IEnumerator MeltItem(string itemName, int quantity)
        {
            PLAYER_IDENTITY_ID = Manager._currentEnjinUser.identities[0].id;
            string itemId = itemName;
            Enjin.MeltTokens(PLAYER_IDENTITY_ID, itemId, quantity, (requestData) =>
            {
                print("Item Melted::" + itemName);
            }, true);

            yield return null;
        }



        public void GetItem(string name)
        {
            print("Verifying transaction..");
            StartCoroutine(MintItem(name, 1));
        }

        public void ReturnItem(string name)
        {
            print("Verifying transaction..");
            StartCoroutine(MeltItem(name, 1));

        }

        public void SendItemTo(string name)
        {
            print("Verifying transaction..");
            StartCoroutine(SendItem(name, 1));
        }    

        

        // public void Display()
        // {
        //     var client = new WebClient();
        //     string _query = @"query {result:EnjinBalances(ethAddress: ""$ethAddress^""){id,index,value,token{name,id,metadata,itemURI}wallet{enjBalance,ethBalance}}}";
           
        //     GraphQuery.variable["ethAddress"] = PLAYER_ADDRESS;
        //     GraphQuery.POST(_query);

        //     var resultGQL = JSON.Parse(GraphQuery.queryReturn);
        //     var wallet = resultGQL["data"]["result"][0]["wallet"];
        //     ethBalance.text = wallet["ethBalance"].Value;
        //     enjBalance.text = wallet["enjBalance"].Value;
        //     var balances = resultGQL["data"]["result"].AsArray;

            
        //     for(int i = 0; i < balances.Count; i++)
        //     {
        //         //string itemURL, imageURL;
        //         string tokenID; // name, description, metadataText;
        //         tokenID = resultGQL["data"]["result"][i]["token"]["id"];
        //         if(resultGQL["data"]["result"][i]["token"]["metadata"].Value == "null")
        //         {                    
        //             itemURL = resultGQL["data"]["result"][i]["token"]["itemURI"].Value;
        //             var metadata = JSON.Parse(client.DownloadString(itemURL));
        //             name = metadata["name"].Value;
        //             imageURL = metadata["image"].Value;
        //             description = metadata["description"].Value;
        //             metadataText = metadata.ToString();
                    
        //         }else{
        //             var metadata = resultGQL["data"]["result"][i]["token"]["metadata"];
        //             name = metadata["name"].Value;
        //             imageURL = metadata["image"].Value;
        //             description = metadata["description"].Value;
        //             metadataText = metadata.Value;
        //         }               
                
        //         print("image URL: "+ imageURL);  
        //         StartCoroutine(GetImage(imageURL, i));
        //         var card = Instantiate(CardPrefab, transform);
        //         card.item = Enjin.GetToken(tokenID); 
        //         StartCoroutine(card.item.GetMetadata((response)=>
        //         {
        //             card.metadata = response.metadata.ToString();
        //             card.Description.text = response.metadata.description;
        //             //StartCoroutine(GetImage(response.metadata.image, i));                    

        //         }));    
        //         card.Title.text = card.item.name;
        //         card.gameObject.SetActive(true); 
        //         cryptoList.Add(card);
        //     }
        // }   

    }
}