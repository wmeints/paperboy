# Solution strategy

This section covers the overall solution strategy for Paperboy.

## Content processing

We're using a content processing pipeline to:

- Assess paper quality and interestingness to the target audience
- Extract knowledge from papers into a knowledge graph
- Implement an approval process for submitted papers
- Write descriptions for interesting papers for the weekly Global AI community newsletter and internal channels

### Content processing workflow

The content processing pipeline is implemented using Dapr workflow. We track progress for papers on the papers themselves.
This makes it easier to see on a per-paper basis how far along we are with processing. In addition to this, we provide
detailed information about the workflow, so we can quickly debug problems with processing.

### Worker pattern

The workflow itself doesn't contain any interaction with language models or external services. We implement processing
logic in a separate content processing worker service. This makes it easier for us to scale out if we have need for it.
It also makes the workflow less vulnerable to crashes.

### Content storage

The content processed in the content pipeline is stored separately so we have detailed information and status information
at hand without looking at the workflow instances that were used to process papers. 

We're using event sourcing to more easily track what operations happened on a paper. We're not building our own event
sourcing solution. Instead we're using [Marten](https://martendb.io) and Postgresql.

## Language model integration

### Hosting language models

We use NLP to automatically assess the quality and interestingness of papers. This saves a lot of time during the week.
We've tested with GPT-4, and that seems to provide the best quality results so far. We're using Azure OpenAI to host
the GPT-4 model in the application.

### Framework for interacting with the LLM

We're using Semantic Kernel to interact with the LLM. Semantic Kernel is currently the only solution in C# that makes
working with models like GPT-4 comfortable. It also features some very powerful methods to build complicated
LLM pipelines that we may need in the near future.

### Verifying LLM interactions

We collect information about LLM interactions to verify quality of these interactions. We don't collect personal
information in those interactions. We collect the following information:

- System prompt
- User prompt
- Generated output

We're using [RAGAS](https://github.com/explodinggradients/ragas) to measure the quality of the LLM interactions.

### Monitoring LLM interactions in production

To track LLM interactions in production, we're using open-telemetry with additional metadata. We may need to switch
to a more specific solution in the future.

## Local development

We're using .NET [Aspire](https://github.com/dotnet/aspire) to make it easier to develop the solution locally. 
We're using Aspire for:

- Orchestrating the various moving parts of the solution on a local workstation
- The service defaults that make things more uniform across components
- The dashboard to more easily debug interactions between various components

Aspire also provides some basic deployment utilities. We're not using the deployment features, as we prefer 
to use Bicep.

## Deployment strategy

### Use Azure Container Apps

This application benefits from all the cloud native features that Kubernetes brings. However, we don't want to spend
a lot of money on AKS hosting. It's much more cost-effective to host on Azure Container Apps. Therefore we're using
Azure Container Apps as the hosting platform.

### Infrastructure as code

We've been experimenting with various infra-as-code tools to deploy solutions to Azure. For this solution we choose to
deploy using Bicep templates. There are two layers to the Bicep templates:

1. Platform templates - These templates deploy the components needed to run applications.
2. Application templates - These templates deploy the containers to the Azure container apps environment.

We've split the deployment, because the platform changes less frequently. Application templates change more frequently.
Over the last year we've also learned that deploying an app to Azure Container Apps without the docker image present in
the container registry fails the deployment. To work around this problem, we use this order of operations:

1. First, we deploy the platform templates to AZure
2. Then, we build, and deploy the docker images to the container registry
3. Finally, we update the templates for the application layer, and deploy them.

## Distributed application runtime

To make it easier to build and maintain the solution we're using [Dapr](https://dapr.io/). It provides us with:

- Robust service-to-service authentication/authorization
- Resiliency for HTTP calls with automatic retries
- Workflow capabilities for a robust content processing pipeline

We specifically need the workflow capabilities for processing papers, as this is a process that easily breaks, because:

- We need to communicate with arxiv to download the paper, which could be down
- We need to use Azure OpenAI for NLP operations, which could run out of capacity.

The Dapr workflow capabilities also make it easier for us to implement a content process with approvals.

