# Specification for Labb2Webb

## Endpoints

### Customers

| Path                                | Method | Request        | Response   | ResponseCodes |
| ----------------------------------- | ------ | -------------- | ---------- | ------------- |
| "/customers"                        | GET    | NONE           | Customer[] | 200           |
| "/customers/email/{Email}"          | GET    | string Email   | Customer   | 200, 404      |
| "/customers/{custId}"               | GET    | int custId     | Customer   | 200, 404      |
| "/customers"                        | POST   | Customer       | NONE       | 200, 400      |
| "/customers/{customerId}"           | PUT    | int customerId | NONE       | 200, 404      |
| "/customers/{customerId}"           | DELETE | int customerId | NONE       | 200, 404      |

### Products

| Path                                     | Method | Request                                  | Response  | ResponseCodes |
| ---------------------------------------- | ------ | ---------------------------------------- | --------- | ------------- |
| "/products"                              | GET    | NONE                                     | Product[] | 200           |
| "/products/{id}"                         | GET    | int id                                   | Product   | 200, 404      |
| "/products"                              | POST   | Product                                  | NONE      | 200, 400      |
| "/products/{productId}"                  | PUT    | int productId                            | NONE      | 200, 404      |
| "/products/{productId}"                  | DELETE | int productId                            | NONE      | 200, 404      |

### ProductCategories

| Path                 | Method | Request         | Response          | ResponseCodes |
| -------------------- | ------ | --------------- | ----------------- | ------------- |
| "/productCategories" | GET    | NONE            | ProductCategory[] | 200           |
| "/productCategories" | POST   | ProductCategory | NONE              | 200, 400      |

### CustomerOrders

| Path                           | Method | Request        | Response        | ResponseCodes |
| ------------------------------ | ------ | -------------- | --------------- | ------------- |
| "/customerOrders"              | GET    | NONE           | CustomerOrder[] | 200           |
| "/customerOrders/{customerId}" | GET    | int customerId | CustomerOrder[] | 200, 404      |
| "/customerOrders/{customerId}" | POST   | CustomerOrder  | NONE            | 200, 400, 404 |
| "/customerorders/{id}"         | DELETE | int id         | NONE            | 200, 404      |

## Data

### Product

| Property Name   | Data Type | Description                                                |
| --------------- | --------- | ---------------------------------------------------------- |
| ProductId       | int       | Product number PK for database                             |
| Name            | string    | Name of the product                                        |
| Description     | string    | Description of the product                                 |
| Price           | double    | Price of the product                                       |
| ProductCategory | Type      | Category of the product, e.g. Electronics, Hygiene         |
| OnStock         | bool      | Stock status of the product, on stock or out of stock      |
| InAssortment    | bool      | Status of the product, in assortment or discontinued       |
| CustomerOrders  | Order[]   | ICollection for all customers that have bought the product |

### Customer

| Property Name | Data Type | Description                     |
| ------------- | --------- | ------------------------------- |
| CustomerId    | int       | Customer number PK for database |
| FirstName     | string    | Firstname of the customer       |
| LastName      | string    | Lastname of the customer        |
| Email         | string    | Customer email                  |
| Phone         | string    | Customer phone number           |
| StreetAddress | string    | Customer street address         |
| PostalCode    | string    | Customer postal code            |
| City          | string    | Customer city                   |
| Country       | string    | Customer country                |
| Orders        | Order[]   | List of orders purchased        |

### ProductCategory

| Property Name | Data Type | Description                        |
| ------------- | --------- | ---------------------------------- |
| Id            | int       | Id of productcategory for database |
| CategoryName  | string    | Name of the category               |

### Order

| Property Name | Data Type  | Description                       |
| ------------- | ---------- | --------------------------------- |
| Id            | int        | Id of customer order for database |
| OrderDate     | DateTime   | Date of purchase                  |
| Customer      | Type       | Customer for the order            |
| Products      | Products[] | Products for the order            |
| OrderTotal    | double     | Total amount of the order         |
