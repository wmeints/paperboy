# Building blocks view

This section covers the various containers, and components that make up the paperboy system.

## System context

```mermaid
C4Context
    title System Context Diagram for Paperboy System

    Person(admin, "Admin Group", "Group of administrators that manage the Paperboy system")
    Person(researchers, "Researchers Group", "Group of researchers that use the Paperboy system")
    
    System(paperboy, "Paperboy System", "Core system that distributes papers")
    System_Ext(arxiv, "Arxiv", "External system providing research papers")

    Rel(admin, paperboy, "Administers")
    Rel(researchers, paperboy, "Submit papers")
    Rel(paperboy, arxiv, "Fetches papers from")
```

## Paperboy system

The paperboy system is made out of a number of microservices, and a frontend `dashboard`.
The structure of the paperboy system is shown below.

```mermaid
C4Container
    title "Paperboy system diagram"
    
    Person(admin, "Admin", "Administers the application")

    Container_Boundary(paperboy, "Paperboy") {
        Container(dashboard, "Dashboard", "Provides a user interface to manage papers")
        Container(orchestrator, "Orchestrator API", "Controls the content process")
        Container(contentstore, "Contentstore API", "Stores the papers")
        Container(contentprocessor, "Contentprocessor API", "Performs content transformations")
        
        Rel(orchestrator, contentstore, "Makes calls to", "HTTP/Json")
        Rel(orchestrator, contentprocessor, "Makes calls to", "HTTP/Json")
        Rel(dashboard, orchestrator, "Makes calls to", "HTTP/Json")
        Rel(admin, dashboard, "Uses", "HTTP")
    }

    System_Ext(arxiv, "Arxiv", "Provides access to published papers")

    Rel(contentstore, arxiv, "Downloads papers from", "HTTP")

UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

## Dashboard application

The dashboard application is a Blazor web application that's used by administrators to manage submitted papers.
Papers are initially submitted by sending a message to one of the administrators. The administrators then import
the paper using a form in the dashboard application.

The dashboard application structure is shown below:

```mermaid
C4Component
    Container_Boundary(dashboard, "Dashboard application") {
        
        Component(orchestrator_client, "Orchestrator client")
        Component(contentstore_client, "Content store client")
        Component(page, "Page", "Interaction with the user")
    }

    Container(orchestrator, "Orchestrator API", "Manages the workflow to process papers")
    Container(contentstore, "Content store API", "Manages information about papers")
    Container(dapr_sidecar,"Dapr Sidecar", "Proxy for interacting with other services")
    

    Rel_Down(page, orchestrator_client, "Uses")
    Rel_Down(page, contentstore_client, "Uses")
    Rel_U(orchestrator_client, dapr_sidecar, "Calls", "HTTP/Json")
    Rel_L(contentstore_client, dapr_sidecar, "Calls", "HTTP/Json")

    Rel_U(dapr_sidecar, contentstore, "Calls", "HTTP/Json")
    Rel_L(dapr_sidecar, orchestrator, "Calls", "HTTP/Json")

UpdateLayoutConfig($c4ShapeInRow="2", $c4BoundaryInRow="1")
```

The primary pattern in the dashboard application is through a set of pages. The logic to communicate
with the other microservices is done via the corresponding clients.

The clients use Dapr the method invocation building block in Dapr to communicate to the services. This interaction pattern provides us with automatic retries.

## Content processor API

The content processor API transforms content of papers into various shapes. The general structure of contentprocessor 
API is shown below:

```mermaid
C4Component
    Container(orchestrator, "Orchestrator API", "Manages content process")

    Container_Boundary(contentprocessor, "Contentprocessing API") {
        Component(endpoint, "API Endpoint", "Links HTTP to the application")
        Component(skill, "LLM Based Skill", "Provides a useful skill to transform content")
        Component(kernel, "Semantic Kernel", "Models the interaction with the LLM")

        Rel(skill, kernel, "Uses")
        Rel(kernel, azure_openai, "Calls")
        Rel(endpoint, skill, "Uses")
    }

    Container(azure_openai, "Azure OpenAI Service", "Hosts the language models")

    Rel(orchestrator, endpoint, "Calls", "HTTP/Json")
```

Each transformation in the API is defined in a Skill component. For example, 
[SummarizePageFunction](../apps/contentprocessor/PaperBoy.ContentProcessor/Skills/Summarization//SummarizePage/SummarizePageFunction.cs) 
is a skill that is part of the contentprocessor.

Skills use [Semantic Kernel](https://github.com/microsoft/semantic-kernel) to interact with Azure OpenAI Service.

## Content store API

The content store API manages information about submitted papers. The structure of the application is shown below:

```mermaid
C4Component
    Container(orchestrator, "Orchestrator API", "Controls the content process")

    Container_Boundary(contentstore, "Content store API") {
        Component(endpoint, "API Endpoint", "Links HTTP to the application")
        Component(commandhandler, "Command handler", "Handles the incoming command")
        Component(repository, "PaperRepository", "Loads/Saves events related to papers")
        Component(aggregate, "Paper", "Models the state of a paper")
        Component(store, "Document store", "Postgres database storing the data related to papers")       
        Component(projection, "Projection", "Projects state information for easy retrieval")

        Rel(endpoint, commandhandler, "Uses for mutations")
        Rel(endpoint, repository, "Uses for queries")
        Rel(repository, store, "Uses")
        Rel(commandhandler, aggregate, "Modifies")
        Rel(commandhandler, repository, "Uses")
        Rel(store, projection, "Runs for updates")
    }

    Rel(orchestrator, endpoint, "Calls")

UpdateLayoutConfig($c4ShapeInRow="2", $c4BoundaryInRow="1")
```

## Orchestrator API

The orchestrator API models the workflow that processes submitted papers. This service uses the Dapr workflow building block.
Using the workflow building block we can be sure that the workflow steps can be retried when one of the transformations fails.

The structure of the orchestrator is shown below:

```mermaid
C4Component
    Container(dashboard, "Dashboard application")

    Container_Boundary(orchestrator, "Orchestrator API", "Controls the content process") {
        Component(endpoint, "API Endpoint", "Links HTTP to the application")   
        
        Component(contentstore_client, "Content store client")
        Component(contentprocessor_client, "Content processor client")
        Component(workflow, "ProcessPaperWorkflow", "Workflow to process a submitted paper")
    }

    
    Container(contentstore, "Content store API", "Manages information about papers")
    Container(contentprocessor, "Content processor API", "Transforms content")
    Container(dapr_sidecar, "Dapr Sidecar", "Manages the workflow runtime")

    Rel_R(dashboard, endpoint, "Calls", "HTTP/Json")

    Rel(endpoint, dapr_sidecar, "Uses")
    Rel(dapr_sidecar, workflow, "Controls")
    Rel(workflow, contentstore_client, "Uses")
    Rel(workflow, contentprocessor_client, "Uses")

    Rel(contentstore_client, dapr_sidecar, "Calls")
    Rel(contentprocessor_client, dapr_sidecar, "Calls")
    Rel(dapr_sidecar, contentstore, "Calls")
    Rel(dapr_sidecar, contentprocessor, "Calls")

UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

The content processing workflow is made out of the main workflow class `ProcessPaperWorkflow`. Each step
in the workflow is modeled as an activity class in the `PaperBoy.Orchestrator.Activities` namespace.

The workflow itself stores no external state. It passes output from activities into follow-up activities.
Information about papers is stored in the content store.