### Introduction
The goal of this code assignment is to see how you approach a problem. Hence, the assignment will not stand alone in our evaluation; we’ll also invite you for a technical talk to hear about your thoughts and ideas.

We’ve intentionally left out a lot of design requirements, to leave you with the freedom to showcase how you would
approach the described task.

We do not expect a fully functioning application. It is ok to stub parts of the application in order to focus on other aspects that you think show of your skillset better.

We expect you to spend 3-5 hours on the assignment. Feel free to reach out if you have any questions or comments.

### Assignment description

#### Supply Chain

[Stock](#stock) is the supply of finished goods (LEGO boxes) available to sell to the end customer. A shortage of stock could be LDC receiving a [sales order (SO)](#sales-order) that cause the [safety stock](#safety-stock) to go below the safety stock threshold. In this situation, [local distribution center (LDC)](#local-distribution-center) creates a [stock transport order (STO)](#stock-transport-order-sto) which is afterwards being picked by a [regional distribution center (RDC)](#regional-distribution-center). 
When the RDC has successfully picked the STO and the stock has been received in the LDC a new goods receipt (GR) is being created.

![alt text](supply-chain.PNG?raw=true)


### Rules for creating a sales order
- A SalesOrder is created by a customer to order finished goods to a distribution center. 

### Rules for creating stock transport order
When a sales order is created, and if LDC does not have enough finished goods (stock), it can use the safety stock.
If the LDC's safety stock goes below the threshold, then LDC will create a new StockTransportOrder with status open ready to be picked by the RDC.

### Rules for picking a stock transport order
- RDC picks the STO if STO's quantity is less than RDC's FinishedGoodsStockQuantity and update it's status to picked
- RDC rejects the STO if STO's quantity is greater than RDC's FinishedGoodsStockQuantity

### Rules for receiving goods
- LDC receives the finished goods from RDC (after a stock transport order) and posts a GoodsReceipt which lead to stock quantity increase based on the current values of SafetyStockQuantity and SafetyStockThreshold. SafetyStockQuantity should always have the same value as SafetyStockThreshold when the quantity increase is finished.

### Your task
Extend the Web API based solution, which allows users to:

1. Query all open stock transport orders for a given LDC
2. Create a sales order
3. Trigger picking/handling a stock transport order in a RDC for a given LDC

### Your solution
- Feel free to include a suite of unit tests.
- Feel free to include logging functionality.
- Be either a fully functional solution or a sample solution as long as it can compile, run and solve the described task.
- For simplicity, the solution must be self-contained. So in essence, no external infrastructure (e.g. databse etc.) should be needed to run your solution.

### Supported IDEs
 - [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
 - [Visual Studio Code](https://code.visualstudio.com/) (You might need some extensions installed like [C#](https://marketplace.visualstudio.com/VSCode)
    - Some useful commands working with C# using Visual Studio Code:

    ```
        dotnet build
        dotnet run
        dotnet restore
    ```

**Pre-requisites:** To build and run the solution .NET6 needs to installed

### Glossary

##### Stock
Stock is the supply of finished goods available to sell to the end customer. Number of LEGO boxes in a location.

##### Regional Distribution Center
A Regional Distribution Center (RDC) is a collection center for finished goods to be distributed to one or more local distribution centers. 

##### Local Distribution Center
It receives and stores goods before they are shipped and distributed to wholesalers, retailers, factories or other warehouses. 

##### Safety stock
It is an extra quantity of a product which is stored in the warehouse to prevent an out-of-stock situation. Consider this like a threshold.

##### Sales Order
Customers create a salesOrder for the distribution center when they need LEGO boxes.

##### Stock Transport Order (STO)
It enables to move stock from one plant to another or between different storage locations of distribution centers
