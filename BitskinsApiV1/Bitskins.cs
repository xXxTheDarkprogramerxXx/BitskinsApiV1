using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using OtpSharp;
using System.Net;
using System.IO;

namespace BitskinsApiV1
{
    public class Bitskins
    {
        //this is used for 2 Factor Authentication
        public static bool FARunnig = false;
        public static string FACode = string.Empty;

        /// <summary>
        /// Api Key From Bitskins
        /// </summary>
        public static string Api_Key = "";

        /// <summary>
        /// Secret From Bitskins For 2 Factor Authentication
        /// </summary>
        public static string SECRET_FROM_BITSKINS = "";

        /// <summary>
        /// AppID Please See AppID Enum Table (AppID_Enum Class)
        /// (Defualt is 730 CSGO)
        /// </summary>
        public static int AppID = 730;

        public static class AppID_Enum
        {
            public const int CSGO = 730;
            public const int PUBG = 578080;
            public const int DotA2 = 570;
            public const int TF2 = 440;
            public const int PD2 = 218620;
            public const int RUST = 252490;
            public const int KF2 = 232090;
        }

        #region << 2 Factor Authentication >>

        static System.Timers.Timer aTimer = new System.Timers.Timer();
        bool firtstart = true;

        public static void FA()
        {

            if (FARunnig == false)
            {

                aTimer = new System.Timers.Timer(TimeSpan.FromSeconds(30).TotalMilliseconds); //refresh every 30 seconds
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                aTimer.Start();

                var secret = SECRET_FROM_BITSKINS;

                // create the ToTP object with the Secret
                var totpgen = new Totp(Base32.Base32Encoder.Decode(secret));

                // generate current ToTP code
                Console.WriteLine("Secure Access Code: " + totpgen.ComputeTotp());
                FACode = totpgen.ComputeTotp().ToString();
            }
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //Do the stuff you want to be done every hour;
            // we try and update all our cases prices from the db

            var secret = SECRET_FROM_BITSKINS;

            // create the ToTP object with the Secret
            var totpgen = new Totp(Base32.Base32Encoder.Decode(secret));

            // generate current ToTP code
            Console.WriteLine("Secure Access Code: " + totpgen.ComputeTotp());
            FACode = totpgen.ComputeTotp().ToString();


        }

        #endregion << 2 Factor Authentication >>

        #region << Bitskins Objects >>

        #region << Wallet Object >>
        public class WalletData
        {
            public string available_balance { get; set; }
            public string pending_withdrawals { get; set; }
            public string withdrawable_balance { get; set; }
            public string couponable_balance { get; set; }
        }

        public class WalletObject
        {
            public string status { get; set; }
            public WalletData data { get; set; }
        }

        #endregion << Wallet Object >>

        #region << GetAllItemPrices >>

        public class GetAllItemPricesPrice
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public string market_hash_name { get; set; }
            public string price { get; set; }
            public string pricing_mode { get; set; }
            public string skewness { get; set; }
            public int created_at { get; set; }
            public string icon_url { get; set; }
            public string name_color { get; set; }
            public string quality_color { get; set; }
            public object rarity_color { get; set; }
        }

        public class GetAllItemPricesObject
        {
            public string status { get; set; }
            public List<GetAllItemPricesPrice> prices { get; set; }
        }

        #endregion << Get all Item Prices>>

        #region << Get Market Data >>

        public class GetMarketRecentSalesInfo
        {
            public string hours { get; set; }
            public string average_price { get; set; }
        }

        public class GetMarketItem
        {
            public string market_hash_name { get; set; }
            public int total_items { get; set; }
            public string lowest_price { get; set; }
            public string highest_price { get; set; }
            public string cumulative_price { get; set; }
            public GetMarketRecentSalesInfo recent_sales_info { get; set; }
            public int? updated_at { get; set; }
        }

        public class GetMarketData
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public List<GetMarketItem> items { get; set; }
        }

        public class GetMarketObject
        {
            public string status { get; set; }
            public GetMarketData data { get; set; }
        }

        #endregion << Get Market Data >>

        #region << Get Account Inventory >>

        public class AccountInvenotryTags
        {
            public string type { get; set; }
            public string weapon { get; set; }
            public string collection { get; set; }
            public string category { get; set; }
            public string quality { get; set; }
            public string exterior { get; set; }
        }

        public class AccountInvenotryRecentSalesInfo
        {
            public string hours { get; set; }
            public string average_price { get; set; }
        }

        public class AccountInvenotryItem
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public int number_of_items { get; set; }
            public List<string> item_ids { get; set; }
            public string class_id { get; set; }
            public string instance_id { get; set; }
            public string market_hash_name { get; set; }
            public string suggested_price { get; set; }
            public string item_type { get; set; }
            public object item_class { get; set; }
            public object item_rarity { get; set; }
            public string item_weapon { get; set; }
            public string item_quality { get; set; }
            public string item_itemset { get; set; }
            public string image { get; set; }
            public bool inspectable { get; set; }
            public string inspect_link { get; set; }
            public AccountInvenotryTags tags { get; set; }
            public bool has_buy_orders { get; set; }
            public AccountInvenotryRecentSalesInfo recent_sales_info { get; set; }
            public object stickers { get; set; }
            public List<List<object>> fraud_warnings { get; set; }
            public bool is_listing_allowed { get; set; }
        }

        public class SteamInventory
        {
            public string status { get; set; }
            public string fresh_or_cached { get; set; }
            public int total_items { get; set; }
            public List<AccountInvenotryItem> items { get; set; }
        }


        public class Tags
        {
            public string type { get; set; }
            public string itemset { get; set; }
            public string quality { get; set; }
            public string rarity { get; set; }
            public string spraycolorcategory { get; set; }
        }

        public class RecentSalesInfo
        {
            public string hours { get; set; }
            public string average_price { get; set; }
        }

        public class Item
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public int number_of_items { get; set; }
            public List<string> item_ids { get; set; }
            public string class_id { get; set; }
            public string instance_id { get; set; }
            public string market_hash_name { get; set; }
            public string item_type { get; set; }
            public object item_class { get; set; }
            public string item_rarity { get; set; }
            public object item_weapon { get; set; }
            public string item_quality { get; set; }
            public string item_itemset { get; set; }
            public string image { get; set; }
            public bool inspectable { get; set; }
            public string inspect_link { get; set; }
            public List<string> prices { get; set; }
            public List<bool> is_featured { get; set; }
            public List<string> float_values { get; set; }
            public string suggested_price { get; set; }
            public Tags tags { get; set; }
            public List<int> created_at { get; set; }
            public List<int> updated_at { get; set; }
            public bool has_buy_orders { get; set; }
            public object stickers { get; set; }
            public List<List<object>> fraud_warnings { get; set; }
            public RecentSalesInfo recent_sales_info { get; set; }
        }

        public class BitskinsInventory
        {
            public string status { get; set; }
            public int total_items { get; set; }
            public string total_price { get; set; }
            public List<Item> items { get; set; }
            public int page { get; set; }
            public int items_per_page { get; set; }
        }

        public class PendingWithdrawalFromBitskins
        {
            public string status { get; set; }
            public int total_items { get; set; }
            public List<object> items { get; set; }
        }

        public class AccountInvenotryData
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public SteamInventory steam_inventory { get; set; }
            public BitskinsInventory bitskins_inventory { get; set; }
            public PendingWithdrawalFromBitskins pending_withdrawal_from_bitskins { get; set; }

        }

        public class AccountInvenotryObject
        {
            public string status { get; set; }
            public AccountInvenotryData data { get; set; }
        }

        #endregion << Get Acocunt Inventory >>

        #region << Get_Inventory_On_Sale >>

        public class InventoryOnSalePatternInfo
        {
            public int paintindex { get; set; }
            public string paintseed { get; set; }
            public int rarity { get; set; }
            public int quality { get; set; }
            public string paintwear { get; set; }
        }

        public class InventoryOnSaleTags
        {
            public string type { get; set; }
            public string weapon { get; set; }
            public string itemset { get; set; }
            public string quality { get; set; }
            public string rarity { get; set; }
            public string exterior { get; set; }
        }

        public class InventoryOnSaleItem
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public string item_id { get; set; }
            public string class_id { get; set; }
            public string instance_id { get; set; }
            public string market_hash_name { get; set; }
            public string item_type { get; set; }
            public object item_class { get; set; }
            public string item_rarity { get; set; }
            public string item_weapon { get; set; }
            public string item_quality { get; set; }
            public string image { get; set; }
            public bool inspectable { get; set; }
            public string inspect_link { get; set; }
            public string price { get; set; }
            public string suggested_price { get; set; }
            public bool is_featured { get; set; }
            public string float_value { get; set; }
            public InventoryOnSalePatternInfo pattern_info { get; set; }
            public bool is_mine { get; set; }
            public InventoryOnSaleTags tags { get; set; }
            public List<object> fraud_warnings { get; set; }
            public object stickers { get; set; }
            public int updated_at { get; set; }
        }

        public class InventoryOnSaleData
        {
            public List<InventoryOnSaleItem> items { get; set; }
            public int page { get; set; }
            public int cache_expires_at { get; set; }
            public double rendered_in_seconds { get; set; }
        }

        public class InventoryOnSaleObject
        {
            public string status { get; set; }
            public InventoryOnSaleData data { get; set; }
        }


        #endregion << Get_Inventory_On_Sale >>

        #region << Get Spesific Item On Sale >>


        public class SpesificSaleItemPatternInfo
        {
            public int paintindex { get; set; }
            public string paintseed { get; set; }
            public int rarity { get; set; }
            public int quality { get; set; }
            public string paintwear { get; set; }
        }

        public class SpesificSaleItemTags
        {
            public string type { get; set; }
            public string weapon { get; set; }
            public string itemset { get; set; }
            public string quality { get; set; }
            public string rarity { get; set; }
            public string exterior { get; set; }
            public string tournament { get; set; }
            public string tournamentteam { get; set; }
        }

        public class SpesificSaleItemSticker
        {
            public string name { get; set; }
            public string url { get; set; }
            public string wear_value { get; set; }
        }

        public class SpesificSaleItemItemsOnSale
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public string item_id { get; set; }
            public string class_id { get; set; }
            public string instance_id { get; set; }
            public string market_hash_name { get; set; }
            public string item_type { get; set; }
            public object item_class { get; set; }
            public string item_rarity { get; set; }
            public string item_weapon { get; set; }
            public string item_quality { get; set; }
            public string image { get; set; }
            public bool inspectable { get; set; }
            public string inspect_link { get; set; }
            public string price { get; set; }
            public string suggested_price { get; set; }
            public bool is_featured { get; set; }
            public string float_value { get; set; }
            public SpesificSaleItemPatternInfo pattern_info { get; set; }
            public bool is_mine { get; set; }
            public SpesificSaleItemTags tags { get; set; }
            public List<object> fraud_warnings { get; set; }
            public List<SpesificSaleItemSticker> stickers { get; set; }
            public int updated_at { get; set; }
        }

        public class SpesificSaleItemData
        {
            public List<SpesificSaleItemItemsOnSale> items_on_sale { get; set; }
            public List<object> items_not_on_sale { get; set; }
        }

        public class SpesificSaleItemObject
        {
            public string status { get; set; }
            public SpesificSaleItemData data { get; set; }
        }

        #endregion << Get Spesific Item On Sale >>

        #region << Money Events >>

        public class MoneyEventsEvent
        {
            public string type { get; set; }
            public object medium { get; set; }
            public string price { get; set; }
            public int time { get; set; }
            public int withdrawn { get; set; }
            public string amount { get; set; }
            public bool? pending { get; set; }
            public string description { get; set; }
        }

        public class MoneyEventsData
        {
            public List<MoneyEventsEvent> events { get; set; }
            public int page { get; set; }
        }

        public class MoneyEventsObject
        {
            public string status { get; set; }
            public MoneyEventsData data { get; set; }
        }

        #endregion << Money Events >>

        #region << Withdraw Request >>

        public class MoneyReuqestObject
        {
            public string status { get; set; }
        }

        #endregion << Withdraw Request >>

        #region << Buy Item <<

        public class BuyItem
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public string item_id { get; set; }
            public string class_id { get; set; }
            public string instance_id { get; set; }
            public string market_hash_name { get; set; }
            public string price { get; set; }
        }

        public class BuyData
        {
            public List<BuyItem> items { get; set; }
            public List<string> trade_tokens { get; set; }
        }

        public class BuyObject
        {
            public string status { get; set; }
            public BuyData data { get; set; }
        }

        #endregion << Buy Item >>

        #region << Sell Item >>


        public class SellItemItem
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public string item_id { get; set; }
            public string class_id { get; set; }
            public string instance_id { get; set; }
        }

        public class SellItemData
        {
            public List<SellItemItem> items { get; set; }
            public List<string> trade_tokens { get; set; }
        }

        public class SellItemObject
        {
            public string status { get; set; }
            public SellItemData data { get; set; }
        }

        #endregion << Sell Item >>

        #region << Modify Sale >>

        public class ModifyItem
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public string item_id { get; set; }
            public string class_id { get; set; }
            public string instance_id { get; set; }
            public string market_hash_name { get; set; }
            public string image { get; set; }
            public string price { get; set; }
            public string discount { get; set; }
        }

        public class ModifyData
        {
            public List<ModifyItem> items { get; set; }
        }

        public class ModifyObject
        {
            public string status { get; set; }
            public ModifyData data { get; set; }
        }

        #endregion << Modify Sale >>

        #region << Withdraw Item >>

        public class WithdrawItem
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public string item_id { get; set; }
            public string class_id { get; set; }
            public string instance_id { get; set; }
        }

        public class WithdrawData
        {
            public List<WithdrawItem> items { get; set; }
            public List<string> trade_tokens { get; set; }
        }

        public class WithdrawObject
        {
            public string status { get; set; }
            public WithdrawData data { get; set; }
        }

        #endregion << Withdraw Item >>

        #region << Get Buy History >>

        public class BuyHistoryItem
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public string item_id { get; set; }
            public string class_id { get; set; }
            public string instance_id { get; set; }
            public string market_hash_name { get; set; }
            public string buy_price { get; set; }
            public bool withdrawn { get; set; }
            public int time { get; set; }
        }

        public class BuyHistoryData
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public List<BuyHistoryItem> items { get; set; }
            public int page { get; set; }
        }

        public class BuyHistoryObject
        {
            public string status { get; set; }
            public BuyHistoryData data { get; set; }
        }

        #endregion << Get Buy History >>

        #region << Get Sell History >>

        public class SellHistoryItem
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public string item_id { get; set; }
            public string class_id { get; set; }
            public string instance_id { get; set; }
            public string market_hash_name { get; set; }
            public string sale_price { get; set; }
            public int time { get; set; }
        }

        public class SellHistoryData
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public List<SellHistoryItem> items { get; set; }
            public int page { get; set; }
        }

        public class SellHistoryObject
        {
            public string status { get; set; }
            public SellHistoryData data { get; set; }
        }

        #endregion << Get Sell History >>

        #region << Get Item History >>

        public class ItemHistoryItem
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public string item_id { get; set; }
            public string market_hash_name { get; set; }
            public string price { get; set; }
            public int last_update_at { get; set; }
            public int listed_at { get; set; }
            public int? withdrawn_at { get; set; }
            public bool listed_by_me { get; set; }
            public bool on_hold { get; set; }
            public bool on_sale { get; set; }
            public int? sold_at { get; set; }
            public int? bought_at { get; set; }
        }

        public class ItemHistoryData
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public List<ItemHistoryItem> items { get; set; }
            public int page { get; set; }
        }

        public class ItemHistoryObject
        {
            public string status { get; set; }
            public ItemHistoryData data { get; set; }
        }

        #endregion << Get Item History >>

        #region << Get Trade Details >>

        public class TradeItemsRetrieved
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public string item_id { get; set; }
            public string class_id { get; set; }
            public string instance_id { get; set; }
        }
        public class TradePatternInfo
        {
            public int paintindex { get; set; }
            public string paintseed { get; set; }
            public int rarity { get; set; }
            public int quality { get; set; }
            public string paintwear { get; set; }
        }

        public class TradeTags
        {
            public string type { get; set; }
            public string weapon { get; set; }
            public string itemset { get; set; }
            public string quality { get; set; }
            public string rarity { get; set; }
            public string exterior { get; set; }
        }

        public class TradeItemsSent
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public string item_id { get; set; }
            public string class_id { get; set; }
            public string instance_id { get; set; }
            public string market_hash_name { get; set; }
            public string item_type { get; set; }
            public object item_class { get; set; }
            public string item_rarity { get; set; }
            public string item_weapon { get; set; }
            public string item_quality { get; set; }
            public string image { get; set; }
            public bool inspectable { get; set; }
            public string inspect_link { get; set; }
            public string price { get; set; }
            public string suggested_price { get; set; }
            public bool is_featured { get; set; }
            public string float_value { get; set; }
            public TradePatternInfo pattern_info { get; set; }
            public bool is_mine { get; set; }
            public TradeTags tags { get; set; }
            public List<object> fraud_warnings { get; set; }
            public object stickers { get; set; }
            public int updated_at { get; set; }
            public object settled_buy_order { get; set; }
            public object delivered_at { get; set; }
        }

        public class TradeData
        {
            public List<TradeItemsSent> items_sent { get; set; }
            public List<TradeItemsRetrieved> items_retrieved { get; set; }
            public string bot_uid { get; set; }
            public int created_at { get; set; }
        }

        public class TradeObject
        {
            public string status { get; set; }
            public TradeData data { get; set; }
        }

        #endregion << Get Trade Details >>

        #region << Get Recent Trade History >>

        public class RecentTradeOffer
        {
            public string steam_trade_offer_id { get; set; }
            public string steam_trade_offer_state { get; set; }
            public string sender_uid { get; set; }
            public string recipient_uid { get; set; }
            public string app_id { get; set; }
            public string context_id { get; set; }
            public int num_items_sent { get; set; }
            public int num_items_retrieved { get; set; }
            public string trade_message { get; set; }
            public int created_at { get; set; }
            public int updated_at { get; set; }
        }

        public class RecentTradeData
        {
            public List<RecentTradeOffer> offers { get; set; }
        }

        public class RecentTradeObject
        {
            public string status { get; set; }
            public RecentTradeData data { get; set; }
        }

        #endregion << Get Recent Trade History >>

        #region << Get Recent Sales Info >>
        public class RecentSalesSale
        {
            public string market_hash_name { get; set; }
            public string price { get; set; }
            public string wear_value { get; set; }
            public int sold_at { get; set; }
        }

        public class RecentSalesData
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public List<RecentSalesSale> sales { get; set; }
        }

        public class RecentSalesObject
        {
            public string status { get; set; }
            public RecentSalesData data { get; set; }
        }

        #endregion << Get Recent Sales Info >>

        #region << Steam Price Check >>

        public class RawDataPriceCheck
        {
            public int time { get; set; }
            public string price { get; set; }
            public int volume { get; set; }
        }

        public class DataPriceChekc
        {
            public string app_id { get; set; }
            public string context_id { get; set; }
            public string market_hash_name { get; set; }
            public List<RawDataPriceCheck> raw_data { get; set; }
            public int updated_at { get; set; }
        }

        public class RootObjectPriceCheck
        {
            public string status { get; set; }
            public DataPriceChekc data { get; set; }
        }

        #endregion << Steam Price Check >>


        #endregion << Bitskins Objects >>

        #region << Functions >>

        #region << CheckSettingsFunction >>

        /// <summary>
        /// we use this function to always check that an api key is set before we do anything else
        /// We also Run 2 Factor Authentication trough here
        /// </summary>
        private static void CheckSettings()
        {
            if(Api_Key == string.Empty || Api_Key == "")
            {
                throw new Exception("Api_Key Not Set Please set api key before running the application");
            }
            //and we run 2 Factor Authentication Here 
            FA();
        }

        #endregion << CheckSettingsFunction >>

        #region << Get Account Balance >>

        /// <summary>
        /// Allows you to retrieve your available and pending balance in all currencies supported by BitSkins.
        /// </summary>
        /// <returns></returns>
        public static WalletObject Get_Account_Balance()
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api
            string url = @"https://bitskins.com/api/v1/get_account_balance/?api_key=" + Api_Key + "&code=" + FACode;
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<WalletObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }
                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error ON Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

            return null;
        }

        #endregion << Get Account Balance >>

        #region << Get All Item Prices >>

        /// <summary>
        /// Allows you to retrieve the entire price database used at BitSkins.
        /// </summary>
        /// <returns></returns>
        public static GetAllItemPricesObject Get_All_Item_Prices()
        {
            CheckSettings();

            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api
            string url = @"https://bitskins.com/api/v1/get_all_item_prices/?api_key=" + Api_Key + "&code=" + FACode + "&app_id="+AppID.ToString();
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<GetAllItemPricesObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }
                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

            return null;

        }


        /// <summary>
        /// Allows you to retrieve the entire price database used at BitSkins.
        /// (With Custom App Id)
        /// </summary>
        /// <returns></returns>
        public static GetAllItemPricesObject Get_All_Item_Prices(string appid)
        {
            CheckSettings();

            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api
            string url = @"https://bitskins.com/api/v1/get_all_item_prices/?api_key=" + Api_Key + "&code=" + FACode + "&app_id=" + appid;
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<GetAllItemPricesObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }
                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

            return null;

        }

        #endregion << Get All Item Prices >>

        #region << Get Market Data >>

        /// <summary>
        /// Allows you to retrieve basic price data for items currently on sale at BitSkins.
        /// </summary>
        /// <returns></returns>
        public static GetMarketObject Get_Price_Data_For_Items_On_Sale()
        {
            return Get_Price_Data_For_Items_On_Sale(AppID.ToString());
        }

        /// <summary>
        /// Allows you to retrieve basic price data for items currently on sale at BitSkins.
        /// (With Custom AppId)
        /// </summary>
        /// <returns></returns>
        public static GetMarketObject Get_Price_Data_For_Items_On_Sale(string appid)
        {
            CheckSettings();

            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api
            string url = @"https://bitskins.com/api/v1/get_price_data_for_items_on_sale/?api_key=" + Api_Key + "&code=" + FACode + "&app_id=" + appid;
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<GetMarketObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }
                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

            return null;
        }

        #endregion << Get Market Data >>

        #region << Get Account Inventory >>
        /// <summary>
        /// Allows you to retrieve your account's available inventory on Steam (items listable for sale), your BitSkins inventory (items currently on sale), and your pending withdrawal inventory (items you delisted or purchased). 
        ///As of January 20th 2016, only the newest 5,000 items are shown by default for the BitSkins inventory.Use page numbers to see all items.
        /// </summary>
        /// <returns></returns>
        public static AccountInvenotryObject Get_My_Inventory()
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api
            string url = @"https://bitskins.com/api/v1/get_my_inventory/?api_key=" + Api_Key + "&code=" + FACode + "&app_id=" + AppID.ToString();
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<AccountInvenotryObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }
                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

            return null;
        }

        /// <summary>
        /// Allows you to retrieve your account's available inventory on Steam (items listable for sale), your BitSkins inventory (items currently on sale), and your pending withdrawal inventory (items you delisted or purchased). 
        ///As of January 20th 2016, only the newest 5,000 items are shown by default for the BitSkins inventory.Use page numbers to see all items.
        /// </summary>
        /// <param name="appid">Custom App Id From Enum List</param>
        /// <returns></returns>
        public static AccountInvenotryObject Get_My_Inventory(string appid)
        {
            CheckSettings();

            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api
            string url = @"https://bitskins.com/api/v1/get_my_inventory/?api_key=" + Api_Key + "&code=" + FACode + "&app_id=" + appid;
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<AccountInvenotryObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }
                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

            return null;
        }

        #endregion << Get Account Inventory >>

        #region << Get Inventory On Sale >>

        /// <summary>
        /// Gets Market Hash Name 
        /// </summary>
        /// <param name="market_hash_name">Must be Full Item Name</param>
        /// <returns></returns>
        public static decimal GetSuggestedItemPrice(string market_hash_name)
        {
            CheckSettings();

            decimal rtndecimal = 0;


            string url = @"https://bitskins.com/api/v1/get_inventory_on_sale/?api_key=" + Api_Key + @"&page=1&app_id=730&market_hash_name=" + market_hash_name + "&code=" + FACode;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<InventoryOnSaleObject>(result);
                if (splashInfo.status != "failed")
                {
                    //we have some items yay
                    decimal pricetopay = 0;

                    //always return the suggested price
                    decimal.TryParse(splashInfo.data.items[0].suggested_price, out pricetopay);

                    rtndecimal = pricetopay;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error Please contact support");
            }

            return rtndecimal;
        }

        /// <summary>
        /// Allows you to retrieve the BitSkins inventory currently on sale. This includes items you cannot buy (i.e., items listed for sale by you). By default, upto 24 items per page, and optionally up to 480 items per page. This method allows you to search the inventory just as the search function on the website allows you to search inventory.
        /// 
        ///Multiple simultaneous calls to this method may result in 'Failed to acquire lock' errors.
        /// </summary>
        /// <param name="app_id">The app_id for the inventory's game (defaults to CS:GO if not specified). (optional)</param>
        /// <param name="Page">Page number. (optional)</param>
        /// <param name="Sort_by">{created_at|price}. CS:GO only: wear_value. (optional)</param>
        /// <param name="Order">{desc|asc} (optional)</param>
        /// <param name="Market_Hash_Name">Full or partial item name (optional)</param>
        /// <param name="Min_Price">Minimum price (optional)</param>
        /// <param name="Max_Price">Maximum price (optional)</param>
        /// <param name="Has_Stickers">{-1|0|1}. For CS:GO only. (optional)</param>
        /// <param name="Is_Stattrak">{-1|0|1}. For CS:GO only. (optional)</param>
        /// <param name="Is_Souvenir">{-1|0|1}. For CS:GO only. (optional)</param>
        /// <param name="Per_Page">Results per page. Must be either 24, or 480. (optional)</param>
        /// <returns></returns>
        public static InventoryOnSaleObject Get_Inventory_On_Sale(
            int app_id
            ,int Page = 0
            ,string Sort_by = ""
            ,string Order = ""
            ,string Market_Hash_Name = ""
            ,decimal Min_Price = -1
            ,decimal Max_Price = -1
            ,int Has_Stickers = 0
            ,int Is_Stattrak = 0
            ,int Is_Souvenir = 0
            ,int Per_Page = 0

            )
        {

            CheckSettings();

            //we start building the url here
            string url = @"https://bitskins.com/api/v1/get_inventory_on_sale/?api_key=" + Api_Key;

            #region << Page >>
            if (Page == 0)
            {
              // we asume the user doesnt want a page number
            }
            else
            {
                url += @"&page=" + Page;
            }
            #endregion << Page >>

            #region << App_ID >>

            if(app_id == 0)
            {
                //if not spesified defualts to csgo 
                app_id = AppID_Enum.CSGO;
            }

            url += @"&app_id=" + app_id;


            #endregion << App_ID >>

            #region << Sort BY >>

            if (Sort_by == "")
            {
                //there is no sort by
            }
            else
            {
                //valid colums only
                if(Sort_by == "price"
                    || Sort_by == "created_at"
                    || Sort_by == "bumped_at"
                    || Sort_by == "wear_value")
                {
                    url += @"&sort_by=" + Sort_by;
                }
                else
                {
                    throw new Exception("Can only sort by the following columns: price, created_at, bumped_at, wear_value");
                }
            }

            #endregion << Sort BY >>

            #region << Order >>

            if(Order == "")
            {
                //we dont do anything with it 
            }
            else if (Order != "desc" || Order != "asc" )
            {
                url += @"&order=" + Order;
            }
            else
            {
                throw new Exception("Please use \"desc\" or \"asc\" when specifying  an order by");
            }

            #endregion << Order >>

            #region << Market Hash Name >>

            if(Market_Hash_Name == "")
            {
                //dont process 
            }
            else
            {
                url += @"&market_hash_name=" + Market_Hash_Name;
            }

            #endregion << Market Hash Name >>

            #region << Min Price | Max Price >>


            if(Min_Price != -1)
            {
                //user spesified a Min price
                url += @"min_price=" + Min_Price;
            }
            if(Max_Price != -1)
            {
                url += @"&max_price==" + Max_Price;
            }
            #endregion << Min Price | Max Price>>

            #region << Last Optional Paramaters >>
            if (Has_Stickers != 0)
            {
                // has stickers has been applied
                url += @"&has_stickers=" + Has_Stickers;
            }
            if(Is_Stattrak != 0)
            {
                url += @"&is_stattrak=" + Is_Stattrak;
            }
            if(Is_Souvenir != 0)
            {
                url += @"&is_souvenir=" + Is_Souvenir;
            }

            if(Per_Page != 0)
            {
                url += @"&per_page=" + Per_Page;
            }



            #endregion << Last Optional Paramaters >>

            url += "&code=" + FACode;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<InventoryOnSaleObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }
                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly" + ex.Message);
            }

            return new InventoryOnSaleObject();

        }

        #endregion << Get Inventory On Sale >>

        #region << Get Spesific Items On Sale >>
        /// <summary>
        /// Allows you to retrieve data for specific Item IDs that are currently on sale. To gather Item IDs you wish to track/query, see the 'Get Inventory on Sale' API call for items currently on sale.
        /// </summary>
        /// <param name="Item_Ids">Upto 250 comma-delimited item IDs.</param>
        /// <returns></returns>
        public static SpesificSaleItemObject Get_Specific_Items_On_Sale(string Item_Ids)
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api
            string url = @"https://bitskins.com/api/v1/get_specific_items_on_sale/?api_key=" + Api_Key + "&item_ids="+Item_Ids + "&app_id=" + AppID.ToString() + "&code=" + FACode;
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<SpesificSaleItemObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }
                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

            return null;
        }

        #endregion << Get Spesific Items On Sale >>

        #region << Get Reset_Price Items>>

        //not in current build

        #endregion << Get Reset_Price Items >>

        #region << Get Money Events >>
        /// <summary>
        /// Allows you to retrieve historical events that caused changes in your balance. Upto 24 items per page.
        /// </summary>
        /// <param name="PageNumber">Page number. (optional)</param>
        /// <returns></returns>
        public static MoneyEventsObject Get_Money_Events(int PageNumber = -1)
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api



            string url = @"https://bitskins.com/api/v1/get_money_events/?api_key=" + Api_Key;

            if(PageNumber != -1)
            {
                url += "&page=" + PageNumber;
            }

            url +="&code=" + FACode;
            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<MoneyEventsObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }
                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

            return null;
        }

        #endregion << Get Money Events >>

        #region << Money Withdrawls >>
        /// <summary>
        /// Allows you to request withdrawal of available balance on your BitSkins account. All withdrawals are finalized 15 days after this request on a rolling basis.
        ///
        ///Multiple simultaneous calls to this method may result in 'Failed to acquire lock' errors.
        /// </summary>
        /// <param name="Ammount">Amount in USD to withdraw. Must be at most equal to available balance, and over $5.00 USD.</param>
        /// <param name="withdrawl_method">Can be bitcoin, paypal, or bank wire.</param>
        public static void Request_Withdrawal(decimal Ammount,string withdrawl_method)
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api



            string url = @"https://bitskins.com/api/v1/get_money_events/?api_key=" + Api_Key;
            url += "&amount=" + Ammount;
            url += "&withdrawal_method=" + withdrawl_method;
            url += "&code=" + FACode;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<MoneyReuqestObject>(result);
                if (splashInfo.status != "failed")
                {
                    
                }
                
                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

        }

        #endregion << Money Withdrawls >>

        #region << Buy Item >>
        /// <summary>
        /// Allows you to buy the item currently on sale on BitSkins. Item must not be currently be on sale to you. Requires 2FA (Secure Purchases) to be enabled on your account if not logged in.
        ///
        ///Multiple simultaneous calls to this method may result in 'Failed to acquire lock' errors.
        /// </summary>
        /// <param name="itemids">Comma-separated list of item IDs.</param>
        /// <param name="Ammount">Comma-separated list of item IDs.</param>
        /// <returns></returns>
        public static BuyObject Buy_Item(string itemids,decimal Ammount)
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api



            string url = @"https://bitskins.com/api/v1/buy_item/?api_key=" + Api_Key;
            url += "&item_ids=" + itemids;
            url += "&prices=" + Ammount;
            url += "&app_id=" + AppID;
            url += "&code=" + FACode;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<BuyObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }

                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

        }

        #endregion << Buy Item >>

        #region << Sell Item >>

        /// <summary>
        /// Allows you to list an item for sale. This item comes from your Steam inventory. If successful, our bots will ask you to trade in the item you want listed for sale.
        /// </summary>
        /// <param name="itemids">Comma-separated list of item IDs from your Steam inventory.</param>
        /// <param name="Ammount">Comma-separated list of prices for each item ID you want to list for sale (order is respective to order of item_ids).</param>
        /// <returns></returns>
        public static SellItemObject Sell_Item(string itemids, decimal Ammount)
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api



            string url = @"https://bitskins.com/api/v1/list_item_for_sale/?api_key=" + Api_Key;
            url += "&item_ids=" + itemids;
            url += "&prices=" + Ammount;
            url += "&app_id=" + AppID;
            url += "&code=" + FACode;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<SellItemObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }

                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

        }



        #endregion << Sell Item >>

        #region << Modify Sale >>

        /// <summary>
        /// Allows you to change the price on an item currently on sale.
        /// </summary>
        /// <param name="itemids">Item IDs to modify.</param>
        /// <param name="Ammount">New item prices, comma-delimited, in order of item_ids.</param>
        /// <returns></returns>
        public static ModifyObject Modify_Item(string itemids, decimal Ammount)
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api



            string url = @"https://bitskins.com/api/v1/modify_sale_item/?api_key=" + Api_Key;
            url += "&item_ids=" + itemids;
            url += "&prices=" + Ammount;
            url += "&app_id=" + AppID;
            url += "&code=" + FACode;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<ModifyObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }

                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

        }

        #endregion << Modify Sale>>

        #region << Withdraw Item >>

        /// <summary>
        /// Allows you to delist an active sale item and/or re-attempt an item pending withdrawal.
        /// </summary>
        /// <param name="itemids">13241919152</param>
        /// <returns></returns>
        public static WithdrawObject Withdraw_Item(string itemids)
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api



            string url = @"https://bitskins.com/api/v1/withdraw_item/?api_key=" + Api_Key;
            url += "&item_ids=" + itemids;
            url += "&app_id=" + AppID;
            url += "&code=" + FACode;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<WithdrawObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }

                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

        }


        #endregion << Withdraw Item >>

        #region << Bump Item >>

        // lot implemented 

        #endregion << Bump Item >>

        #region << Get Buy History >>
        /// <summary>
        /// Allows you to retrieve your history of bought items on BitSkins. Defaults to 24 items per page, with most recent appearing first.
        /// </summary>
        /// <param name="PageNumber">Page number. (optional)</param>
        /// <returns></returns>
        public static BuyHistoryObject Get_Buy_History(int PageNumber = -1)
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api

            string url = @"https://bitskins.com/api/v1/get_buy_history/?api_key=" + Api_Key;
            if (PageNumber != -1)
            {
                url += "&page=" + PageNumber;
            }
            url += "&app_id=" + AppID;
            url += "&code=" + FACode;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<BuyHistoryObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }

                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

        }



        #endregion << Get Buy History >>

        #region << Get Sell History >>
        /// <summary>
        ///Allows you to retrieve your history of sold items on BitSkins. Defaults to 24 items per page, with most recent appearing first.
        /// </summary>
        /// <param name="PageNumber">Page number. (optional)</param>
        /// <returns></returns>
        public static SellHistoryObject Get_Sell_History(int PageNumber = -1)
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api

            string url = @"https://bitskins.com/api/v1/get_sell_history/?api_key=" + Api_Key;
            if (PageNumber != -1)
            {
                url += "&page=" + PageNumber;
            }
            url += "&app_id=" + AppID;
            url += "&code=" + FACode;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<SellHistoryObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }

                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

        }



        #endregion << Get Sell History >>

        #region << Get Item History >>
        /// <summary>
        /// Allows you to retrieve bought/sold/listed item history. By default, upto 24 items per page, and optionally up to 480 items per page.
        /// </summary>
        /// <param name="PageNumber">Page number. (optional)</param>
        /// <param name="names">Delimited item names (optional)</param>
        /// <param name="deliminater">Can be , ; ;END; !END! (optional)</param>
        /// <param name="perpage">Results per page (between 24 and 480). (optional)</param>
        /// <returns></returns>
        public static ItemHistoryObject Get_Item_History(int PageNumber = -1,string names = "",string deliminater = "",int perpage = -1)
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api

            string url = @"https://bitskins.com/api/v1/get_item_history/?api_key=" + Api_Key;
            if (PageNumber != -1)
            {
                url += "&page=" + PageNumber;
            }
            if(names != "")
            {
                url += "&names=" + names;
            }
            if(deliminater != "")
            {
                url += "&delimiter=" + deliminater;
            }
            if(perpage != -1)
            {
                url += "&per_page=" + perpage;
            }
            url += "&app_id=" + AppID;
            url += "&code=" + FACode;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<ItemHistoryObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }

                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

        }


        #endregion << Get Item History >>

        #region << Trade Details >>
        /// <summary>
        /// Allows you to retrieve information about items requested/sent in a given trade from BitSkins. Trade details will be unretrievable 7 days after the initiation of the trade.
        /// </summary>
        /// <param name="TradeToken">The trade token in the Steam trade's message.</param>
        /// <param name="TradeId">The trade ID in the Steam trade's message.</param>
        /// <returns></returns>
        public static TradeObject Get_Trade_Details(string TradeToken , string TradeId)
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api

            string url = @"https://bitskins.com/api/v1/get_trade_details/?api_key=" + Api_Key;
            url += "&trade_token=" + TradeToken;
            url += "&trade_id=" + TradeId;
            url += "&app_id=" + AppID;
            url += "&code=" + FACode;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<TradeObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }

                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }

        }


        #endregion << Trade Details >>

        #region << Get Recent Trade History >>

        /// <summary>
        /// Allows you to retrieve information about 50 most recent trade offers sent by BitSkins. Response contains 'steam_trade_offer_state,' which is '2' if the only is currently active.
        /// </summary>
        /// <param name="Active_Only">Value is 'true' if you only need trade offers currently active. (optional)</param>
        /// <returns></returns>
        public static RecentTradeObject Get_Recent_Trade_Offers(bool Active_Only = false)
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api

            string url = @"https://bitskins.com/api/v1/get_recent_trade_offers/?api_key=" + Api_Key;
            if(Active_Only != false)
            {
                url += "&active_only=" + Active_Only;
            }
            url += "&app_id=" + AppID;
            url += "&code=" + FACode;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<RecentTradeObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }

                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }
        }

        #endregion << Get Recent Trade History >>

        #region << Get Recent Sales Info >>
        /// <summary>
        /// Allows you to retrieve upto 5 pages worth of recent sale data for a given item name. These are the recent sales for the given item at BitSkins, in descending order.
        /// </summary>
        /// <param name="Market_Hash_Name">The item's name.</param>
        /// <param name="PageNumber">The page number. (optional)</param>
        /// <returns></returns>
        public static RecentSalesObject Get_Sales_Info(string Market_Hash_Name,int PageNumber = -1 )
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api

            string url = @"https://bitskins.com/api/v1/get_recent_trade_offers/?api_key=" + Api_Key;

            url += "&market_hash_name=" + Market_Hash_Name;

            if (PageNumber != -1)
            {
                url += "&page=" + PageNumber;
            }
            url += "&app_id=" + AppID;
            url += "&code=" + FACode;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<RecentSalesObject>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }

                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }
        }


        #endregion << Get Recent Sales Info >>

        #region << Get Steam Price Data >>

        public static RootObjectPriceCheck Get_Steam_Price_Data(string Market_Hash_Name)
        {
            //we always check the settings first
            CheckSettings();
            //url for Bitskins Api

            string url = @"https://bitskins.com/api/v1/get_steam_price_data/?api_key=" + Api_Key;

            url += "&market_hash_name=" + Market_Hash_Name;

            url += "&app_id=" + AppID;
            url += "&code=" + FACode;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";
            try
            {
                var getResponse = (HttpWebResponse)getRequest.GetResponse();
                Stream newStream = getResponse.GetResponseStream();
                StreamReader sr = new StreamReader(newStream);
                var result = sr.ReadToEnd();
                var splashInfo = JsonConvert.DeserializeObject<RootObjectPriceCheck>(result);
                if (splashInfo.status != "failed")
                {
                    return splashInfo;
                }

                throw new Exception("Could Not DeserializeObject from Bitskins");
            }
            catch (Exception ex)
            {
                throw new Exception("Internal Error on Get Response please ensure your api is set and 2 factor authentication is running correctly");
            }
        }

        #endregion << Get Steam Price Data >>

        #endregion << Functions >>

    }
}
