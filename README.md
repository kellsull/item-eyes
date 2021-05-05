# ItemEyes
ASP.NET Core project for inventory management

## Contents
[API](https://github.com/kellivan/ItemEyes/tree/main/ItemEyes/Areas/api)

## Description
This is a simple ASP.NET project created following Microsoft Documentaion site
for ASP.NET Core.

The project started as a just Web API and has now been morphed to include MVC.
ASP.NET allows for both to coexist, but ideally they should be seperately hosted.
They're grouped together in this project for convience and demonstration.
It was accomplished by utilizing the Areas folder for MVC and updating the Startup
configuration.

## Models

### Item
Items represent anything regsitered into inventory of the same kind at the same time.
There were ways of splitting data into seperate entities, but as a simple solution,
this one model represents goods, their receipt, and stored location.

### Location
Locations represnt a place of storage and its contained items.
This is a group of items as well as contact info for their place of storage.


## Controllers/Views

### Index
This is the landing page for the project.

### Items / (Details, Create, Edit, Delete)
These are scaffolded pages for CRUD operations for Item data.

### Locations / (Details, Create, Edit, Delete)
These are scaffolded pages for CRUD operations for Location data.

### Areas / api / *
These are controllers for the WebAPI access to the data

## Future
Plans for this project include adding more documentation so it better serves as a sample project.
Also adding functionality via controllers and adding new and expanding existing models.
