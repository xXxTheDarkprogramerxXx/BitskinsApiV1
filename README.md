# BitskinsApiV1

## This project was created to interact with bitskins api using a simple .net client all code is supplied and most of it is running as of the first commit 

### Nuget Packages are now avaialble 
https://www.nuget.org/packages/BitskinsApiV1/1.0.0#

Install-Package BitskinsApiV1 -Version 1.0.0


### Running the Project 

Running the project is quite simple link the BitskinsAPIv1.dll to your project and add the following lines at the start of your code 
```C#
            //Set the Bitskins Api Key and The Secret For 2 Factor Authentication
            BitskinsApiV1.Bitskins.Api_Key = "your own API Key you can get this at Bitskins.com";
            BitskinsApiV1.Bitskins.SECRET_FROM_BITSKINS = "2 Factor Authentication Secret From Bitskins.com";
            
            //we need to set the appid once for the api to function
            //this can also be changed torugh out the application set it once as its a global var
            BitskinsApiV1.Bitskins.AppID = BitskinsApiV1.Bitskins.AppID_Enum.CSGO;
 ```
 

### Calling Functions From The Api
Since Bitskins returns JSON Files i have converted then to c# objects for easy use 

2 Run A Function simply call it as the following 

``` c#
            //This will return the Bitskins Wallet Object
            BitskinsApiV1.Bitskins.WalletObject wallet = BitskinsApiV1.Bitskins.Get_Account_Balance();
            //once recieved you should be able to get funds availabe by calling the objects value
            lblAccountBalance.Text = "$" + wallet.data.available_balance;
 ```
 
 
 ### 2 Factor Authentication 
 
 Starting 2 Factor Authentication is reuired at the start of the project (on Form Load / WCF Startup)
 
 To start 2 Factor Authentication simply call this method 
 
 ```C#
            BitskinsApiV1.Bitskins.FA();
 ```
 
 To retrieve your 2 Factor Authentication Code ( 6 Diget Code )
 
 Simply Call this function 
 
 ```C#
            txt2FA.Text = BitskinsApiV1.Bitskins.FACode;
 ```
 
