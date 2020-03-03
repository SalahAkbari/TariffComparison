# Tariff Comparison
Compare products based on their annual costs with Microservices architecture and .NET Core  
 
The application is build based on the microservices architecture. The high level design of Back-end architecture is: 
* Identity Microservice - Authenticates user based on username, password and issues a JWT Bearer token which contains Claims-based identity information in it. 
* Tariff Microservice - Retrieve the products via GET
* API Gateway - Acts as a center point of entry to the back-end application, Provides data aggregation and communication path to microservices. 

*An API gateway takes all API calls from clients, then routes them to the appropriate microservice with request routing, composition, and protocol translation. Typically, it handles a request by invoking multiple microservices and aggregating the results, to determine the best path. It can translate between web protocols and web. 
 
Security : JWT Token based Authentication: JWT Token based authentication is implementated to secure the WebApi services. Identity Microservice acts as a Auth server and issues a valid token after validating the user credentitals. The API Gateway sends the token to the client. The client app uses the token for the subsequent request. 
Technologies 
    C#.NET 
    ASP.NET WEB API Core 
    SQL Server 
Opensource Tools Used 
    Automapper (For object-to-object mapping) 
    Swashbucke (For API Documentation) 
    XUnit (For Unit test case) 
    Ocelot (For API Gateway Aggregation) 
 
End-points configured and accessible through API Gateway 
 
    Route: "/user/authenticate" [HttpPost] - To authenticate user and issue a token 
    Route: "/tariff/GetProducts/{usage}" [HttpGet] - To retrieve the products 
 
End-points implemented at the Microservice level 
 
    Route: "/api/user/authenticate" [HttpPost]- To authenticate user and issue a token 
    Route: "/api/tariff/GetProducts/{usage}" [HttpGet]- To retrieve the products
    
Solution Structure 
* Tariff.Identity  
o Handles the authentication part using username, password as input parameter and issues a JWT Bearer token with Claims-Identity info in it.
* Tariff.Comparison  
o Supports an http methods 'GetProducts'. Receives http request for this method. 

o Handles exception through a middleware 

o Calls the appropriate function in the 'Tariff' framework 

o Returns the tariff response result back to the client
* Tariff.Framework  o Defines the business exceptions and domain model validation o Defines the required data types for the framework 'Struct', 'Enum', 'Consants' o Implements the business logic o Performs the task of mapping the domain model to entity model and vice versa o Registers the Interfaces and its Implementation in to Service Collection through dependency injection
* Tariff.Gateway  o Validates the incoming Http request by checking for authorized JWT token in it. o Reroute the Http request to a downstream service. 
* Tariff.Client o A console client app that connects to Api Gateway, can be used to login with username, password and perform the tariff request to get the products. 
Exception Handling 
A Middleware is written to handle the exceptions and it is registered in the startup to run as part of http request. Every http request, passes through this exception handling middleware and then executes the Web API controller action method. 
* If the action method is successful then the success response is send back to the client.
* If any exception is thrown by the action method, then the exception is caught and handled by the Middleware and appropriate response is sent back to the client. 
Swagger: API Documentation 
Swashbuckle Nuget package added to the "Tariff.Comparison Microservice" and Swagger Middleware configured in the startup.cs for API documentation. when running the WebApi service, the swagger UI can be accessed through the swagger endpoint "/swagger". 
To run the application  
*  Open the solution (.sln) in Visual Studio 2019    
*  It is not necessary, but if you would like you can configure the AppInsights Instrumentation Key in Tariff.Comparison -> Appsettings.json file. I comment out the AppInsight related code in Startup.cs file as I didn’t have a key and didn't require log.  

Check the Tariff.Identity -> UserService.cs file for Identity info. User details are hard coded for few accounts in Identity service which can be used to run the app. For instance, you can use the following credential to log in as a client: 
 
Account Number  Currency   Username  Password 1234567 USD johnsmith john1234 
 
* Run the following projects in the solution  
* Tariff.Identity 
* Tariff.Comparison 
* tariff.Gateway 
* Tariff.Client 
* Gateway host and port should be configured correctly in the  Tariff.Client 
* Tariff.Idenity and Tariff.Comparison service host and port should be configured correctly in the gateway -> configuration.json 
