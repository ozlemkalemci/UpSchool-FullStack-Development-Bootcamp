<a name="readme-top"></a>
<h2 align="center">
Up School Full Stack Development Bootcamp 
</h3>
<h3 align="center">
BackEnd Project
<br/>
<br/>
  <br/>
<br/>
  <br/>
  
 
  
https://github.com/ozlemkalemci/UpSchool-FullStack-Development-Bootcamp/assets/96883642/6a60e71a-d203-4f76-be34-3247d3a87d7a



# About the Project

This project is a crawl application that uses SignalR, Selenium, and Clean Architecture to meet customer requirements. The Crawler is located in the application layer and performs the scraping process using Selenium. An interface is used for sending log messages with SignalR. The necessary codes for sending emails has been written.Toaster is used for in-app notifications.When the scraping process is completed, the toaster appears on our Blazor page.
<br/>
<br/>
<br/>
![q0pqpkf](https://github.com/ozlemkalemci/UpSchool-FullStack-Development-Bootcamp/assets/96883642/1ef3a6e7-8a80-43f5-9872-d0ac14f16e43)


<br/>
<br/>
<br/>

# Customer Requirements

We need an application that crawls products from our competitor's website, "https://finalproject.dotnet.gg/". The customer should be able to select the number of products to be crawled, the type of crawling, and whether they want to download an Excel file. Logs should be live-tracked on the screen during the crawling process. A notification should be displayed on the screen when the crawling process is completed.
<br/>
<br/>
<br/>


# SignalR

![Instantly-Update-a-Real-Time-Chart-with-SignalR-in-Blazor-Server-Side-App2-removebg-preview](https://github.com/ozlemkalemci/UpSchool-FullStack-Development-Bootcamp/assets/96883642/608501f7-e8b2-4fcb-b6fe-88b5a66c70a6)

SignalR is a Microsoft technology that enables the creation of real-time web applications. It facilitates communication between the client and the server, allowing for fast updates and interactions. SignalR supports the WebSocket protocol and provides a simplified programming model by abstracting away the complexities of WebSocket usage. This makes it easier to add features such as live chat, real-time updates, and notifications to applications.
<br/>
<br/>
<br/>

# Selenium
![sel-removebg-preview](https://github.com/ozlemkalemci/UpSchool-FullStack-Development-Bootcamp/assets/96883642/95a8b59f-e0ae-4f0f-a7d5-8e2b5b1196a4)

Selenium is a popular open-source tool used for automating web application testing and browser automation. It provides a set of APIs and libraries to interact with web browsers. Selenium is compatible with various programming languages and can perform a range of automation tasks such as controlling browsers, simulating user interactions, filling out web forms, and navigating between pages. It offers a powerful tool for developers and testing professionals to test and ensure the reliability of web applications.
<br/>
<br/>
<br/>
# Clean Architecture
![d-removebg-preview (1)](https://github.com/ozlemkalemci/UpSchool-FullStack-Development-Bootcamp/assets/96883642/3bd18fc4-fc92-410a-8fc4-bc251e83c46a)



The general structure of Clean Architecture consists of layers arranged from the innermost to the outermost. Each layer has a specific responsibility and interacts with other layers in a certain way. Below is information about the general layers of Clean Architecture and their responsibilities:

Domain Layer:
The Domain Layer is the core layer that includes the business logic and business rules. It represents the core functionality of the application and is independent of other layers. This layer typically includes entity classes, value objects, and business rules. The Domain Layer, being independent of other layers, ensures the resilience of the application's core functionality against changes.

Application Layer:
The Application Layer receives requests from the user interface (UI) and performs the business logic operations by passing these requests to the Domain Layer. This layer includes the application's business processes, user session management, authorization, authentication, and other functions. The Application Layer verifies the accuracy of data received from users and manages appropriate errors.

Web API Layer:
The Web API Layer communicates with the outside world, including the user interface, other services, mobile applications, etc. This layer receives HTTP requests, performs necessary authentication, and passes the request to the Application Layer, returning the results. The Web API Layer correctly directs incoming requests to the Application Layer and returns responses to the outside world.

Infrastructure Layer:
The Infrastructure Layer provides access to external resources such as databases, file systems, external services, etc. This layer includes the data access layer, code that manages connections to external resources, service calls, and other details related to external resources. The Infrastructure Layer includes components that form the application's infrastructure and serves other layers.

Presentation Layer (Blazor):
If you are using Clean Architecture with Blazor, Blazor is used as the Presentation Layer. This layer includes components that create the user interface and manage user interactions. Blazor is a browser-based, server-side .NET web framework. When creating the UI layer with Blazor, you can access the business logic through the Application Layer or Web API Layer without directly accessing other layers.

Clean Architecture is based on the principle of inverting dependencies between these layers. In other words, inner layers do not depend on outer layers, while outer layers can depend on inner layers. This way, changes in one layer do not affect other layers, and each layer can maintain its own responsibility.
