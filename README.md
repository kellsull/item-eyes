# item-eyes
.NET Core Web API for inventory management

## Scope
This is a simple, non-production Web API project made to demonstrate .NET and Entity Framework Core.
The key value held during its development is commitment to clean and readable
design patterns, specifically those set by Microsoft's .NET Documentation.
All further commits will be made with this in mind.

## Output
The Web API interfaces a LocalDb Database using EF Core and performs basic CRUD operations
on inventory items and their locations.  Some specialized actions have been configured to
implement order storage input and output, as well as provide multiple query options.
The .NET Template for Web API is preconfigured to use Swagger UI to demo builds. It looks like
this:

![Swagger1](https://user-images.githubusercontent.com/27789610/116793732-1b98aa80-aa8e-11eb-834b-88738df76c48.png)

And this is how you can sandbox actions using the page:

![Swagger2](https://user-images.githubusercontent.com/27789610/116793733-1dfb0480-aa8e-11eb-9346-e6effa1a8024.png)


Of course, you can call the api directly from the browser:

![api](https://user-images.githubusercontent.com/27789610/116793565-17b85880-aa8d-11eb-96f7-b2517f29f388.gif)

## Development
On the pathway of learning ASP.NET Core following the [official documentation](https://docs.microsoft.com/en-us/dotnet/),
I was tasked with creating a Web API project of my choosing. After much diliberation,
I decided on an inventory tracker, as it would be simple enough to get started on, but still
complicated enough to have something to talk about here.

### Model Classes
The original draft included four data models:
- Inventory
- Items
- Orders
- Locations

I quickly realized that a one-to-many Inventory join table was redundant, as the rows of the Items table
would work just as easily on its own. I then considered the Orders table, which I originally thought
was key, as it would allow for multiple items of the same type while still separating the entries.  But
just like the Inventory table before, it too was found redundant and easily replaceable with a single
column in the Items table.

This left me with two tables:
- Items
- Locations

![EFCore](https://user-images.githubusercontent.com/27789610/116800336-d85a2e00-aac5-11eb-8bc6-0c2ac8dbb94d.png)

I was tempted at this point to consider whether or not the location data should just be
merged into the Items table as 2 to 3 more columns.  But I found that this would lead to
a significant amount of duplicate columns that would otherwise just be single id numbers.  So I
decided to stop worrying about the data models and move on to the next phase.

### Configuring EF Core
After creating the class files for the data models, I went about setting up my project for
EF Core.  This step includes installing the NuGet packages for EF Core, setting up the project files
to use a database (Sql Server Express LocalDb was used here), and creating a class for the
Entity Framework Database Context.  In addition, since this is a demo project, it was necessary to
supply demo data.  This was accomplished by creating an EF DbInitializer class.

### Scaffolding Controllers
Once setting up the database portion was complete, the next step was to create the controllers
for the two models that were established. Visual Studio makes this quick to start by generating
some boilerplate CRUD actions through scaffolding.  From there, the actions were added to and modified
to replicate inventory procedures.  Testing was now able to begin, and I discovered several key
aspects to Web API design that didn't overlap with the MVC experience I gained earlier.

The biggest difference I observed, outside of the lack of views, was that the HTTP actions in
the Web API were very particular with how they are called.  In order to overload these methods,
I had find ways that would cooperate with how the API worked.  There were a few ways to accomplish
this, with additional controllers or estabilishing routes in the startup file, but by far the
easiest and preferred way was to simply use the Routing Bracket Syntax to specify unique
URI's for the various actions.

Here's an example of how these actions work:

![SendItems](https://user-images.githubusercontent.com/27789610/116800228-ce83fb00-aac4-11eb-8d00-1215a662f6fd.png)

### Future Considerations
Things to consider going forward with this project might include developing more actions to
return more specific data sets from the database.  The URI patterns might benifit from some
revision, as well as the Models themselves could benefit from secondary consideration.

My personal interest in making a more interesting and useful product would be to expand
functionality.  I believe to achieve this, data models would need to be added to track invoices,
employees, customers, etc.

Part of the beauty of APIs is the modular way they provide functionality, and this project could benefit
the most by integration further software to create a more dynamic and sofisticated solution
to inventory tracking.  Possible pathways could include a frontend, a secondary api to add
functionality from a different department like accounting, or even tying in well-known outside
services like google maps or User authentication.  These things of course would be establishing
bigger projects, and exist outside of the realm of this particular one.
