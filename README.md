# BankApp

Application:
Banking Test application

Github Url:
https://github.com/ganeshbojanapu333/BankApp

Technologies used:
.Net 6  (For Apis)
XUnit Test (For Testing)

Architecture used:
Onion Architecture, It provides loose coupling between layers

Architecture Layers in the application:
1.Domain Layer  (BankDomainLayer)
2.Api Layer     (BankApp)
3.Service Layer (BankServiceLayer)
4.Data Access Layer (DataAccessLayer)
5.XUnit Test Layer (BankXUnitTest)

Functionality:
I have built this application just as a test application with basic functionalities of Banking Domain such as create Customers or users, Add accounts to the users and make deposit and withdrawl transactions

Roles Involved:
Admin
As an Admin you can create Customers or users and Add accounts to the users. I didn't implement authentication and authorization because it's just for test purpose

How to run locally?
1. Clone the solution from below link
   https://github.com/ganeshbojanapu333/BankApp.git
2.Open cloned solution in visual studio
3.Run the solution from visual studio

How to Test locally?
1.After running solution it will redirect you to swagger UI API where it lists all the APis imlemented. You can try the swagger Api endpoints and test it.
2.You can run the test cases aswell which are written with the help of XUnit test framework.

