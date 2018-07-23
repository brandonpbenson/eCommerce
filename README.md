# Mister Roboto Arigato Store
Code Fellows 401 C#/ASP.NET course e-commerce store that sells ROBOTS!
The shop lives [here](http://misterrobotoarigato.azurewebsites.net).

**Authors**: Jackie L, Earl Jay Caoile <br />
**Version**: 1.0.0

---
## Overview
This project is a full stack web application that serves as an e-commerce website 
selling robots. 

#### Registration, Login, and External Login (OAUTH)
The following tutorial was followed to add external logins: [Google](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-2.1&tabs=aspnetcore2x)
and [Microsoft](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/microsoft-logins?view=aspnetcore-2.1&tabs=aspnetcore2x)

After registration, they will receive a welcome email.  Users can still register 
and directly log in through the website rather than with an external party.

#### Claims
Upon registration, claims are taken of a user's email because that serves as their 
username.  The email claim is also set to their email which, connects the user to 
their basket and orders.  The other claims taken are of their first and last name 
because it is used to greet them when they land on the dashboard.  Additionally, 
every registered member is given a basket claim because we want to give an empty 
basket to every registered member.

#### Policies
The policies enforced are Admin and IsDoge.  Admin is enforced so that this admin 
can gain access to the admin dashboard and take care of inventory.  The other policy 
is IsDoge, which gives admin access to anyone with "Doge" in their login name; this 
policy was enforced because Doges have access to a secret page and Doges can get 
discounts. Doge is the best!!!

#### Shopping
From here, users are able to browse the store, and add whichever item they would like 
to their basket. They are able to add quantities of the item or remove an item form 
their basket. After checkout, users will receive an email of what they've purchased.

#### Checkout
After shopping, a user will be asked for their shipping information.  This will be 
stored in our database.  Users can have multiple orders.  

A special savings can be applied at checkout, but that's for you to discover!

#### Database Schema
![Database Schema]()

#### Database Schema Explanation of the Tables Used
_Product_ Products will have:
* an ID: to identify a product with a number
* Name: name of the product
* SKU: SKU of the product
* Price: how much a product costs
* Description: description about a product
* ImgUrl: a picture of the product

_Application User_ Users will have:
* First Name: the user's first name
* Last Name: the user's last name

_Basket_ Baskets will have:
* an ID: to identify every basket, so that we can grab its contents later
* CustomerEmail: baskets are given to users upon registration an is connected to 
  their email
* List of BasketItems, which is comprised of all the products that they purchased

_Basket Items_ Items in a basket will have:
* an ID: to identify every item associated with a basket
* Product ID: the product ID that is tied to the basket
* Product Name: the name of the product tied to the basket
* Customer Email: the email of the customer buying the product, so we can associate 
  the customer with the product they're buying
* Quantity: the number of times of the product they're buying
* ImgUrl: the image tied to that product
* UnitPrice: how much that product costs

_Order_ Orders will have:
* an ID: to identify each order
* UserID: to tied each customer to an order
* Shipping: the address of each customer
* List of OrderItems: ties this order to the item in the order, which is defined 
  in OrderItems
* Address ID: ID tied to an order
* Address: The address associated with the Address ID
* DiscountName: name of the discount because we have 2 tiers
* DiscountPercent: percentage of the discount because we have 2 tiers

_OrderItems_ Order Items are the items on an Order:
* an ID: to identiyy each order
* OrderID: to identify each item on an order
* ProductID: to identify each product on an order
* UserID: to connect the user who is buying that product
* ProductName: the name of the product being bough
* Quantitiy: the number of items being bought
* ImgUrl: the picture of the item
* UnitPrice: how much that item costs

_Address_ Addressses will have:
* an ID: to identify each address
* FirstName
* LastName
* Street
* Street2
* City
* State
* Country
* Zip
* UserID: connects this address to a user

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