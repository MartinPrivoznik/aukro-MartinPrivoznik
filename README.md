# aukro-app
 - Mobile bazaar app with API data management using Visual studio 2k19
 - Application was tested on **Android version 4.4 (Kitkat)** but works either for latest   versions of Android or even for IOS
 - Reason for making this project is testing Entity framework with ASP.NET Core
 
 ### FYI
 - Mobile application is programmed in Xamarin Forms using Repository design pattern
 - REST API runs on ASP.NET
 - Data management is provided by entity framework (SQL)

### Basics and needs
- In case you want to run this application :
```
          1) Download project
          2) Make sure your local server is online
          3) Migrate AukroContext.cs in DataManagement project
          4) Navigate to folder **Data Management** in cmd
          5) Type 'Dotnet run' (This deploys API server-side application to your local server)
          6) In Data module in folder Models\Constants\Universe.cs change URL to http://YOUR-IPV4-ADDRESS:5001/
          7) Now you are free to deploy AukroApp1.Android to your mobile device and start testing (Make sure that your mobile device is at the same network)
```