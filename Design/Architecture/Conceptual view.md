# Conceptual view
The following diagram captures a conceptual view of the solution
![Image](/Images/Conceptual.png)
## Data Ingetestor
* Purpose of this subsystem is to extract data from the external service and persist it in a local datastore in an optimized quarriable format.
## Serving Tier
* This sub-system will expose the Web APIs which will be consumed by the end user or other channels.
* Will host the business logic
## Channels
* A collections of alternate channels including CLIs and UIs which will expose the application functionality
* Channels will interact with the serving tier to request information. 