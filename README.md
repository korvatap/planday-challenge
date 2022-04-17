# The Planday car factory

![A very fast looking car](planborgini.png)

With renowned brands like the PlanfaRomeo and the Voksday, the Planday car factory is the birthplace for many world-class cars.

Due to some manufacturing problems and an increasing number of orders, we need you to help us with a few tasks here in the factory.

## The scenario

Next month we have a big order of cars with different specs:
|Brand|Doors|Paint|Speakers|Quantity
|---|---|---|---|---|
|PlanfaRomeo|5 doors|Blue base with Orange stripes|1 subwoofer and 2 standard|75|
|Planborgini|3 doors|Pink base with red dots|10 subwoofers and 20 standard|15|
|Volksday|5 doors|Red base with black stripes|4 standard speakers|20|
|PlandayMotorWorks|3 doors|Black base with yellow dots|1 subwoofer 2 speakers|40|
|Plandrover|5 doors|Green base with gold stripes|4 standard speakers|20|

The factory is currently in a broken state and won't even compile. Your job is to make it compile and start producing cars again.

### Tasks
Please finish as many of these tasks as you can, but if you feel like making changes to other areas of the code, please feel free to do so.
1. In the CarFactory folder, you will find the solution file, CarFactory.sln. Open this
2. The start-up project should be CarFactory. Fix the compile errors, and run this project. This should open up a Swagger page on https://localhost:44329/index.html
3. Use the POST /Car endpoint to request the cars mentioned in the above section and make sure the code is working, fixing any errors you come across
4. Bill from quality insurance has been complaining we don’t have any tests to cover these new specs.
Write some unit tests to cover these different car specs where you find it appropriate.
5. Customer support get a lot of requests regarding paintjobs because peoples' phones changes the "type" for paintjob to have an uppercased letter, e.g. "Stripe", and our system doesn't allow that. Extend the system to support mixed casings, and write appropriate unit tests
6. Sales have been complaining we can’t make cars quick enough. They says the quicker we can make these cars the more money we can make! 
Spend the remainder of time optimising the code to make the endpoint faster. 
     
**When you are done coding, please zip the code and share with us**

### Rules
- You can’t edit the subcontractor 
- You can optimise code but don’t change any validation or business rules 
- In Painter.cs, please only edit FindPaintPassword 
- In CarAssembler.cs, please don't edit logic in the constructor or the CalibrateLocks function. You can add to the constructor, but the existing logic should stay the same.

