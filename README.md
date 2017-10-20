ardoq-csharp-client
===================

Small C# wrapper for the [Ardoq](http://ardoq.com) [REST-api](https://shared.ardoq.com/presentation/shared/57b014d69f2a267f3b4c9574/slide/0/).

### Installation
Using NuGet:

`PM> Install-Package Ardoq`

### Usage
```csharp
var client = new ArdoqClient(new HttpClient(), "https://app.ardoq.com", "api-token", "your-org-label");
```

### Starting a small project
*NB:* Before using the client you have to generate an api token as described [here](https://app.ardoq.com/presentation?presentation=ardoqAPI)

```csharp
using Ardoq;
using Ardoq.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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
            var client = new ArdoqClient(new HttpClient(), "https://app.ardoq.com", "insert-your-token-here", "your-organization-label");
            var template = await client.ModelService.GetTemplateByName("Application Service");
            var workspace = await
                client.WorkspaceService.CreateWorkspace(new Workspace("demo-workspace", template.Id, "My demo workspace"));
            var model = await client.ModelService.GetModelById(workspace.ComponentModel);
            var webshop =
                await
                    client.ComponentService.CreateComponent(new Component("Webshop", workspace.Id, "This is the webshop", model.GetComponentTypeByName("Application")));
            var webshopCreateOrder =
                await
                    client.ComponentService.CreateComponent(new Component("Create order", workspace.Id,
                        "Creates an order based on the current shoppingcat", model.GetComponentTypeByName("Service"), webshop.Id));
            var erp =
                await
                    client.ComponentService.CreateComponent(new Component("ERP", workspace.Id, "This is the ERP system", model.GetComponentTypeByName("Application")));
            var erpCreateOrder =
                await
                    client.ComponentService.CreateComponent(new Component("Create order", workspace.Id,
                        "Creates an order", model.GetComponentTypeByName("Service"), erp.Id));

            var reference = await
                    client.ReferenceService.CreateReference(new Reference(workspace.Id, "Order from cart", webshopCreateOrder.Id, erpCreateOrder.Id,
                        model.GetReferenceTypeByName("Synchronous"))
                    { ReturnValue = "Created order" });
            await client.TagService.CreateTag(new Tag("Customer", workspace.Id, "",
                new List<string>() { webshopCreateOrder.Id, erpCreateOrder.Id }, new List<string>() { reference.Id }));
            return await client.WorkspaceService.GetWorkspaceById(workspace.Id);
        }
    }
}

```

Running this simple example let's Ardoq visualize the components and their relationships.

###### Component landscape

![Components](https://s3-eu-west-1.amazonaws.com/ardoq-resources/public/comps.png)  

###### Sequence diagram

![Sequence diagram](https://s3-eu-west-1.amazonaws.com/ardoq-resources/public/sequence_diagram.png)

###### Relationship diagram

![Relationships](https://s3-eu-west-1.amazonaws.com/ardoq-resources/public/rels.png)

#### Models
The model API is not stable yet, so you have to create your Model in the UI and refer to the id.
##Contributing
### Tests
In order to run the tests you need an Ardoq account. You also need to generate a security token and edit the
settings in `ArdoqTest.Helper.TestUtils` to fit your configuration.
### License

Copyright © 2017 Ardoq AS

Distributed under the Eclipse Public License either version 1.0 or (at your option) any later version.
