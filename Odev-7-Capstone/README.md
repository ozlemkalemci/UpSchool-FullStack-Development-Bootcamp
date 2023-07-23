<a name="readme-top"></a>
<h2 align="center">
Up School Full Stack Development Bootcamp 
</h3>
<h3 align="center">
Capstone Project
<br/>
<br/>
  <br/>
<br/>
  <br/>


https://github.com/ozlemkalemci/UpSchool-FullStack-Development-Bootcamp/assets/96883642/1fcf77ce-e21f-4952-9fac-efc4bf5d6ee8



# About the Project

This project is a crawl application that uses SignalR, Selenium, and Clean Architecture to meet customer requirements. The Crawler is located in the application layer and performs the scraping process using Selenium. An interface is used for sending log messages with SignalR. The necessary codes for sending emails has been written.Toaster is used for in-app notifications.When the scraping process is completed, the toaster appears on our Blazor page.
<br/>
<br/>
<br/>
![q0pqpkf](https://github.com/ozlemkalemci/UpSchool-FullStack-Development-Bootcamp/assets/96883642/1ef3a6e7-8a80-43f5-9872-d0ac14f16e43)

<br/>
<br/>
<br/>

# Google Login 


https://github.com/ozlemkalemci/UpSchool-FullStack-Development-Bootcamp/assets/96883642/432b5a35-4e1c-4834-8ccd-07da436bdfbf


Google Login is an authentication method that allows users to log in to your website using their Google accounts. Users can access your website with just a single click using their Google credentials. This method offers several advantages compared to other traditional methods used for member logins or user authentication on your site.


# Redux



https://github.com/ozlemkalemci/UpSchool-FullStack-Development-Bootcamp/assets/96883642/3efbf699-eb8c-4b32-986e-31bb1d1704c8

Redux is a library used for state management in JavaScript applications. It maintains the application's state in a central store and manages state-changing operations using actions and reducers. This state represents all the data held throughout the application and reflects its current state of operation. By employing Redux, it becomes easier to manage application state, ensuring a more consistent and comprehensible flow of data between components.

In my application, I use Redux to keep track of information like whether the user requested an email address or a download process. Additionally, I use Redux to maintain the notification count for displaying the notification icon within the application.


# Crawler & Live Logs




https://github.com/ozlemkalemci/UpSchool-FullStack-Development-Bootcamp/assets/96883642/6a79159f-0705-462f-a932-af64a15f9c74

We can download our orders as an Excel file. These tables are well-formatted Excel spreadsheets.

After the scanning process, information about whether a file will be downloaded or an email will be sent is communicated to our crawler page using Redux. Our scanning process continues based on this information. Once the scanning process starts, the user is directed to the live log screen. On this screen, the product crawling process is monitored in real-time. After the process is completed, we keep track of the notification count using Redux. If the process is completed, a badge with the number "1" appears on the avatar in the app bar.

# Table & Modal



https://github.com/ozlemkalemci/UpSchool-FullStack-Development-Bootcamp/assets/96883642/92cfacfe-7f2d-46f2-b0e9-cf8e2e0eda23

The user's orders are displayed in a table. The products and events related to these orders are shown on the screen using a modal form. The user can delete the orders if desired.


# Excel


https://github.com/ozlemkalemci/UpSchool-FullStack-Development-Bootcamp/assets/96883642/325f754d-1463-4855-96bd-fab450a93147

We can download our orders as an Excel file. These tables are well-formatted Excel spreadsheets.


# Design


https://github.com/ozlemkalemci/UpSchool-FullStack-Development-Bootcamp/assets/96883642/62a9d811-54d8-4cb8-aa2e-941097777667

Resource for animated background: https://codepen.io/anjeetsingh2775/pen/LYBjwKP

![crawlpage](https://github.com/ozlemkalemci/UpSchool-FullStack-Development-Bootcamp/assets/96883642/3f505593-6fd4-4594-9745-1432490c7a19)

Material UI has been used in the project. However, the Material UI appbar has been made transparent, and a custom shape has been designed to replace it. The asymmetrical triangle shape of the navbar has been created by me. To achieve a seamless match with the sharp triangle structure and gradient color of the animated landing page, the following resource has been utilized: https://www.shapedivider.app/
