# Project 3 - SpacePark

![Spacepark](spacepark.jpg)

For the procedures used in this project see : [Procedures](Procedures.md)

For the documentation by the team see: [Documentation/readme.md](Documentation/readme.md)

# SpacePark

When traveling across space you once in a while need to take a break, and park you spaceship, you do that at a spaceport. The thing is just that spaceports are just like any parking lot on earth controlled by an evil parking company, the biggest of these is SpacePark!

## Your assignment

You are a development team at SpacePark and your assignment is to develop an application which register parking's and close the sparceport when it's full (and open when there is room, and only for spaceships which fits in).

All parking's should be registered in a database, which is created using Entity Framework Core and code first. All queries to the database should be done using Entity Frameworks fluent API.

The twist to this is that this is an **exclusive** spaceport and ONLY famous space travelers which have been part of a Starwars movie can use the spaceport. The travlers should identify them self when arriving, be able to pay before they can leave the parking lot and get an invoice in the end. 

You can test if the user has been part of Starwars by using the Starwars Web API: [swapi.dev](https://swapi.dev/), you are not allowed to cache the data from the API, which means that you will need to request the API each time you need to fetch data. These requests should be made asynchronously.

A recommendation (but no requirement) is to use the Nuget package [RestSharp](https://restsharp.dev/). You will need to implement the classes to be used by the build-in ORM in RestSharp.

```c#
var client = new RestClient("https://swapi.dev/api/");
var request = new RestRequest("people/", DataFormat.Json);
// NOTE: The Swreponse is a custom class which represents the data returned by the API, RestClient have buildin ORM which maps the data from the reponse into a given type of object
var peopleResponse = await client.GetAsync<Swreponse>(request);

Console.WriteLine(peopleResponse.Data.Count);
foreach (var p in peopleResponse.Data.Results)
{
    Console.WriteLine(p.Name);
}
```

The travlers only use starships which have been part of a Starwars movie (see the endpoint /starships). They should be able to select their starship on arrival in the application.

All request to the Starwars API should (unlike the example above) be made asynchronous. 

And remember to create unit tests, where possible.

## Your code

Develop a console application which can be the interface for the application.

The application should save the parkings, payments etc to the a database defined using [Entity Framework](https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli) och code first.

## Given

Some files are given in this repository.

**Documentation**

The folder initially only contains one file called *readme.md*, this file is more or less empty.

In this folder should you place digital representations of all documentation you do. Screenshots, photos (of CRC cards, mindmaps, diagrams).

Please make a link and descriptive text in the *readme.md* using the [markdown](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet) notation:

```markdown
# Our train project
What we have done can be explained by this mindmap.
![Mindmap of spaceport](mindmap.jpg)
Bla bla bla bla
```

**Source**

The source folder is more or less empty, it just contains one file a Visual Studio solution file, called SpacePark.sln.

New code projects should be added to this file.

# Getting started

These are **suggestions** on how you can get started on this project:

1. Discuss the assignment (and document this in the *documentation*-file), so you have a common picture of what you are building
2. Discuss (and document this in the *documentation*-file) the [user experience (UX)](https://bootcamp.uxdesign.cc/ui-ux-design-explained-794fd110a0d) the users should have when using the console application for the parking system:
   1. What should be in the screen
   2. Language of the application
   3. How to interact with the application? mouse, keyboard, other
   4. Is there a menu? Which options should it have
3. Discuss (and document this in the *documentation*-file) which data the system generate, and try to draw an [ER-diagram](https://www.youtube.com/watch?v=QpdhBUYk7Kk) of the data
4. Open the given solution-file in Visual Studio and add a new console application
5. Implement a **very simple** flow of a scenario and unit tests which can confirm that it works, the flow could consist of:
   1. The user entering his name
   2. Making a simple request against the *Starwars Web API* to all persons
   3. Check the name the user entered against the persons from the API request
   4. Creating a simple table in the database using Entity Framework
   5. Store in the database that the user have registered himself 
   6. List in the UI all registrations which have been done so far
6. Extend the application functionality and a very simple menu (if you have decided to make a menu).
