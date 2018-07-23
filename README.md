# Mister Roboto Arigato Store
Code Fellows 401 C#/ASP.NET course e-commerce project.

**Authors**: Jackie L, Earl Jay Caoile <br />
**Version**: 1.0.0

---
## Overview
This project is a full stack web application that serves as an e-commerce website. 

#### Registration and Login
New users can register an account.  After registration, they will receive a welcome 
email.  Previous users can login back to an existing account. Returning users will 
also receive an email notification that they've logged back into the site.  

#### External Login
External login has been enabled.  Users can choose to either log in with a Google or 
Microsoft account.

#### Shopping
From here, users are able to browse the store, and add whichever item they would like 
to their basket. They are able to add quantities of the item or remove an item form 
their basket. After checkout, users will receive an email of what they've purchased.

#### Checkout
After shopping, a user will be asked for their shipping information.  This will be 
stored in our database.  Users can have multiple orders.  

A special savings can be applied at checkout, but that's for you to discover!

#### Admin
Admins can access an admin portal that allows them to perform 
full CRUD operations on all products in the database.

---
## Getting Started
The following is required to run the program.
1. Visual Studio 2017 Community or Enterprise edition.
2. The .NET desktop development workload enabled
3. ASP.NET NuGet packages
4. SendGrid, installed as a NuGet package

## Build
To run this locally, install the [.NET SDK package](https://www.microsoft.com/net/download/dotnet-core/2.1).
Clone this repo through a bash terminal and type the following commands:
```
cd MisterRobotoArigato
dotnet restore
dotnet bulid
dotnet run
```

#### Setup the databases
The local database tables for the store **products** needs to be created.  
Type the following commands:
```
Add-Migration chooseANameForTheMigration -Context RobotoDbContext
Update-Database -context RobotoDbContext
```

The local database tables for the **users** needs to be created.
Type the following commands:
```
Add-Migration chooseANameForTheMigration -Context ApplicationDbContext
Update-Database -context ApplicationDbContext
```

#### Create External Login Client ID and Secrets
Navigate to the following tutorials to set up a Google and Microsoft 3rd 
party OAUTH Client ID and Secrets:
[Google](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-2.1&tabs=aspnetcore2x)
[Microsoft](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/microsoft-logins?view=aspnetcore-2.1&tabs=aspnetcore2x)

Store these keys in either the secrets.json or appsettings.json.

#### Setup SendGrid Email Delivery
SendGrid, available through Azure, is required for emails to be sent to 
users.  On the Azure portal, click on ```Create a Resoure``` and 
search for ```SendGrid```.  Select ```SendGrid Email Delivery``` and 
selecet ```Create```.  Fill in the necesasry information and save your 
info.  

Head over to [SendGrid](https://app.sendgrid.com). Navigate to 'Settings' 
and create an API Key.  Store this in either the projects secrets.json or 
appsettings.json.

The SendGrid Nuget Package is needed within Visual Studio.
Type the following commands:
```
Install-Package SendGrid
```

---
## Architecture
This application is created using ASP.NET Core 2.1 Web Application <br />
*Languages*: C#, HTML, CSS, SCSS <br />
*Tools*: Azure, Visual Studio Team Services, SQL Database, SendGrid Bootstrap <br />
*Type of Application*: Web Application <br />

---
## Walk Through Sprint 2
Mister Roboto Arigato now offers a third party login option:

![Third Party Login Options](Screenshots/week2/externalLogin.png)

Select either and the user will be redirected to the third party to login:

![Login With Microsoft](Screenshots/week2/msLogin.png)

After the third party login, users will be redirected back to Mister Arigato 
Roboto where they will be prompted to "Register":

![Third Party Redirect Back](Screenshots/week2/msLoginRedirect.png)

Instead of a third party login, users can register for a completely new 
account:

![New Registration](Screenshots/week2/newRegistrationInfo.png)

This step will prompt SendGrid to send a welcome email:

![Welcome Email](Screenshots/week2/welcomeEmail.png)

If the user were to logout and then log back in, they will receive another 
email welcoming them back:

![Welcome Back Email](Screenshots/week2/returnUserEmail.png)

This is the new shop view after the login process:

![Third Party Login Shop View](Screenshots/week2/memberViewAfterExternalLogin.png)

Let's go shopping.  Click 'Shop' from the navbar and let's buy an Iron Giant. 
Click "Add to Cart" 

![Add To Shopping Cart](Screenshots/week2/shopChoicesWithAddButton.png)

The basket content page, a view component displaying items in the basket, 
persists on a product details page, which can be viewed if "View Basket Contents" 
is selected.  For now, let's navigate up to "My Basket" in the nav bar.

![My Basket View](Screenshots/week2/myBasketView.png)

From here, users can update the quantity of an item.  We will be selecting 
3 and then "Update" for this example:

![Update Quantity](Screenshots/week2/myBasketUpdateQuantity.png)

Select "Shop" from the navbar and let's continue shopping:

![Continue Shopping](Screenshots/week2/continueShopping.png)

The basket content view component persists on the shop page as well as a 
product details page, let's have a look at the contents of the basket:

![basket content view component](Screenshots/week2/viewBasketComponent.png)

Click the X and close that view.  Scroll up on the shop page and view the 
details of any other item.  The Ramen Vending Robot looks good.  Click 
"Details" and then "Add to Cart":

![Buy Another Product](Screenshots/week2/buyAnotherProduct.png)

Now go back to "My Basket" from the navbar.  You know, we've spent too much 
money so far.  Let's delete the Ramen Vending Robot from our basket:

![Basket With More Products](Screenshots/week2/myBasketWithMoreProducts.png)

This updated basket information persists to the checkout page.  Click on 
"Checkout" and we can now see the updated basket and total, without the 
Ramen Vending Robot.  From the checkout page, users will need to put in their 
shipping information:

![Checkout View](Screenshots/week2/checkOut.png)

Click "Checkout".  The confirm page will show a summary of the order:

![Confirm Page](Screenshots/week2/confirm.png)

When a user is on this page, an email receipt is on its way to their inbox:

![Recipt Email](Screenshots/week2/receiptEmail.png)

---
## Walk Through Sprint 1

_Landing Page_

![MRA Store Visual 1](Screenshots/MRA-SS1.jpg)

_Register An Account_

From here, anyone can register for an account.

![Register Account](Screenshots/register.png)

_Non Admin/Regular Member View_

![Registered Member View](Screenshots/memberView.png)

_Member Shop_

Clicking _Shop_ in the navbar will take members to the shop.

![Member Shop](Screenshots/shop.png)

_Shop Details_

In the shop, clicking on the _Details_ link will take users 
to the product landing page where more details are shown.

![Product Details](Screenshots/details.png)

_Admin Portal_

Any member who has an email with "doge" in it, will be an admin.

![MRA Store Visual 2](Screenshots/MRA-SS2.jpg)

_Admin Dashboard_

Admins can view the entire inventory or add a product.
![Admin Dashboard](Screenshots/adminDashboard.png)

_Admin View All_

From View All, admins can edit the product description or delete the product.
![Admin View All](Screenshots/AdminViewAll.png)

_Admin Edit Product Info_

Let's edit a product info.
![Admin Edit View](Screenshots/adminEdit.png)

_Admin Edit Results_

The new edits will appear on the view all page.
![Admin Edit Result](Screenshots/adminEditAfter.png)

_Admin Delete_

Another option is to delete a product from inventory. 
Let's delete the bee.

![Admin Delete](Screenshots/deleteBee.png)

_Admin Delete Results_

The bee is now gone!

![Delete Results](Screenshots/deleteBeeAfter.png)

_Admin Add A Product_

From the admin dashboard, admins can add a product. 
Let's add the bee that was deleted.

![Add a Product](Screenshots/addProduct.png)

_Admin Add Product Result_

The newly added bee is back on the inventory list!

![Added Product](Screenshots/addProductAfter.png)