# AzureSearch-Office365

Visual Studio solutions with all my demos done in my session in the last SharePoint Saturday in Madrid

You will find my complete presentation in the next link</br> 
http://es.slideshare.net/JoseCarlosRodriguezA/using-azure-search-to-build-office-365-search-driven-solutions

# Content of this repository

A complete Visual Studio solution with all the projects used during my session in the different demos done.

<ul>
  <ol><strong><u>AzureSearchIndexApplication:</u></strong> Console Application Project with the code to create a new index in Azure Search. </ol>
  <ol><strong><u>AzureSearchPopulationApplication:</u></strong> Console Application Project with code to populate Office 365 content in Azure Search using the Microsfot Graph API. </ol>
  <ol><strong><u>AzureSearchSPS:</u></strong> Web Application Project which shows how to consume the information stored in Azure Search through the Azure SDK.NET  </ol>
</ul>

# AzureSearchIndexApplication

In order to get ready this project, you should replace two params in the appSetting section in the app.config file

<ul>
  <ol><strong><u>apikey:</u></strong> The Key given by Azure to get access to this service</ol>
  <ol><strong><u>servicename:</u></strong> The name used to create this service in Azure</ol>
</ul>

Also, don´t forget to add this package into your project

Install-Package Microsoft.Azure.Search -Pre 


# AzureSearchPopulationApplication

In order to get ready this project, you should replace two params in the appSetting section in the app.config file

<ul>
  <ol><strong><u>apikey:</u></strong> The Key given by Azure to get access to this service</ol>
  <ol><strong><u>servicename:</u></strong> The name used to create this service in Azure</ol>
  <ol><strong><u>ClientId:</u></strong> The client id obtained when configure a new AAD application</ol>
  <ol><strong><u>Thumbprint:</u></strong> The thumbprint obtained when a .pfx certificate is uploaded in an Azure Web Site</ol>
</ul>

Also, don´t forget to add this package into your project

Install-Package Microsoft.Azure.Search -Pre</br>
Install-Package RestSharp

And add via nuget the Active Directory Authentication Library (ADAL)

# AzureSearchSPS

In order to get ready this project, you should replace two params in the appSetting section in the web.config file

<ul>
  <ol><strong><u>apikey:</u></strong> The Key given by Azure to get access to this service</ol>
  <ol><strong><u>servicename:</u></strong> The name used to create this service in Azure</ol>
</ul>

Also, don´t forget to add this package into your project

Install-Package Microsoft.Azure.Search -Pre

# References 

You could find more information about Azure Search and how to use it with Office 365 in the next links

https://elblogdelprogramador.wordpress.com/2016/01/18/primeros-pasos-con-azure-search-los-indices/
https://elblogdelprogramador.wordpress.com/2016/01/24/autenticacion-app-only-para-usar-la-api-microsoft-graph-en-webjobs-de-azure/
https://elblogdelprogramador.wordpress.com/2016/02/02/indexando-contenido-en-el-servicio-azure-search/
https://azure.microsoft.com/en-us/documentation/articles/search-what-is-azure-search/
http://www.eliostruyf.com/building-daemon-or-service-app-with-the-microsoft-graph-api/
https://azure.microsoft.com/en-us/documentation/articles/search-howto-dotnet-sdk/
http://graph.microsoft.io/en-us/docs




