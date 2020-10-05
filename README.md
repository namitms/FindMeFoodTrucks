# FindMeFoodTrucks

|![Image](/Images/Logo.png)| Web APIs to find a food truck proximate to a location. </br></br> Hosted at: [Find a Food Truck Web APIs ](https://findmefoodtrucks.azurewebsites.net/swagger/index.html)|
| --------  | ------------------- |


## The Problem

Our San Francisco team loves to eat. They are also a team that loves variety, so they also like to discover new places to eat.

In fact, we have a particular affection for food trucks. One of the great things about Food Trucks in San Francisco is that the city releases a list of them as open data.

Your assignment is to make it possible for us to find a food truck no matter where our work takes us in the city.

This is a freeform assignment. You can write a web API that returns a set of food trucks (our team is fluent in JSON). You can write a web frontend that visualizes the nearby food trucks. We also spend a lot of time in the shell, so a CLI that gives us a couple of local options would be great. And don't be constrained by these ideas if you have a better one!

The only requirement for the assignment is that it give us at least 5 food trucks to choose from a particular latitude and longitude.

:link: SFGov [Dataset](https://data.sfgov.org/resource/rqzj-sfat.json)

# Minimum viable product
The following assumption were made to scope the MVP (Minimum viable product) for the solution.
## Key requirements

### In-scope
1. Users will be able to query for food trucks proximate to a specific location (latitude and longitude).
    1. The system will return at least 5 results (location of trucks) matching the search criteria.
1. Users will have the ability to interact with the application using REST based APIs with JSON Payload.
    1. The access to the APIs will be secured

### Out of Scope
1. Integrating the application APIs with 3rd party identity providers
2. Geo-redundant deployment of the solution
3. Codifying the Infrastructure
1. UI testing 
1. Load testing
1. Performance testing
4. Disaster recovery testing 
1. Any items described described outside the 'In-Scope' section of this document

## Implementation
The services for finding the food truck is hosted as public Web APIs on Microsoft Azure. The API takes the following parameters as input:
* Longitude - The search location's longitude 
    * Datatype : Double
    * Range : 180 to -180
    * e.g. : -122.39
* Latitude - The search location's latitude
    * Datatype : Double
    * Range : 180 to -180
    * e.g. : 37.787
* Radius - The radius in meters to perform the search from the search location's coordinates
    * Datatype : Long
    * Range : 1 to 10000(soft limit)
    * e.g. : 37.787
* Search String - A free form search search string
    * Datatype : String
    * Range : 0 to 100 characters (soft limit)
    * e.g. : Chicken or Mexican

### Authentication
The service implements APIKey authentication. The service expects a API key to be passed as a parameter in the request header.

e.g. APIKey:MyKey

# Important links 
* :link: [Solution Architecture](https://github.com/namitms/FindMeFoodTrucks/blob/master/Design/Architecture/Solution%20Architecture.md)
* :link: [Project Kanban Board](https://github.com/namitms/FindMeFoodTrucks/projects/1)
* :link: [Known Issues](/KnownIssues.md)





