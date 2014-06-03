ardoq-csharp-client
===================

Small C# wrapper for the [Ardoq](http://ardoq.com) REST-api.

###Install
Install the [NuGet package](https://www.nuget.org/packages/Ardoq/)

```
 PM> Install-Package Ardoq
```

###Usage
```csharp
var client = new ArdoqClient(new HttpClient(), "hotname", "api-token");
```
The client will operate on the default organization (Personal). To change this set the Org field on the client to the
appropriate organization.

###Starting a small project
```csharp
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Ardoq;
using Ardoq.Models;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
           var workspace = new Program().Run();
            workspace.Wait();
            Console.WriteLine(workspace.Result);
            Console.ReadLine();
        }

        public async Task<Workspace> Run()
        {
            var client = new ArdoqClient(new HttpClient(), "http://app.ardoq.com", "my-token");
            var model = await client.ModelService.GetModelByName("Application Service");
            var workspace = await
                client.WorkspaceService.CreateWorkspace(new Workspace("demo-workspace", model.Id, "My demo workspace"));
            var webshop =
                await
                    client.ComponentService.CreateComponent(new Component("Webshop", workspace.Id, "This is the webshop"));
            var webshopCreateOrder =
                await
                    client.ComponentService.CreateComponent(new Component("Create order", workspace.Id,
                        "Creates an order based on the current shoppingcat", model.GetComponentTypeByName("Service"), webshop.Id));
            var erp =
                await
                    client.ComponentService.CreateComponent(new Component("ERP", workspace.Id, "This is the ERP system"));
            var erpCreateOrder =
                await
                    client.ComponentService.CreateComponent(new Component("Create order", workspace.Id,
                        "Creates an order", model.GetComponentTypeByName("Service"), erp.Id));

            var reference = await
                    client.ReferenceService.CreateReference(new Reference(workspace.Id, "Order from cart", webshopCreateOrder.Id, erpCreateOrder.Id,
                        model.GetReferenceTypeByName("Synchronous")) {ReturnValue = "Created order"});
            await client.TagService.CreateTag(new Tag("Customer", workspace.Id, "",
                new List<string>() {webshopCreateOrder.Id, erpCreateOrder.Id}, new List<string>() {reference.Id}));
            return await client.WorkspaceService.GetWorkspaceById(workspace.Id);
        }
    }
}

```

Running this simple example let's Ardoq visualize the components and their relationships.

![Components](https://s3-eu-west-1.amazonaws.com/ardoq-resources/public/comps.png)  
######*Component landscape*
![Sequence diagram](https://s3-eu-west-1.amazonaws.com/ardoq-resources/public/sequence_diagram.png)
######*Sequence diagram*
![Relationships](https://s3-eu-west-1.amazonaws.com/ardoq-resources/public/rels.png)
######*Relationship diagram*
####Models
The model API is not stable yet, so you have to create your Model in the UI and refer to the id.
####More examples
The api is pretty straight forward. For more examples, please refer to the [tests](https://github.com/ardoq/ardoq-java-client/tree/master/src/test/java/com/ardoq/service).

###License

Copyright © 2014 Ardoq AS

Distributed under the Eclipse Public License either version 1.0 or (at your option) any later version.
