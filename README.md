# Paperboy

Welcome to my paper of the week application called Paperboy. I publish a link to an interesting paper every week in
the Global AI community. It's something I do with a lot of love and manual labor. I have a nice set of papers by now
that I completely forgot about, because I don't keep track of things I publish.

This application is changing that. I use this web application for a number of goals.

- :rocket: Automatically assess the quality and interestingness of papers I find and receive from others.
- :rocket: Track papers that I've seen with associated metadata in a knowledge graph.
- :rocket: Provide a nice interface for people to search through papers I've published about in the weekly newsletter.

To be honest, this application is also to show off just what you can do with tools like 
[Semantic Kernel](https://github.com/microsoft/semantic-kernel), and [Aspire](https://github.com/dotnet/aspire). So
it's a _little_ over-engineered.

## :computer: System requirements

- [Docker Desktop](https://docs.docker.com/desktop/install/windows-install/)
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Dapr](https://docs.dapr.io/getting-started/install-dapr-cli/)

## :wrench: Getting started

You can run this solution locally. Clone the repository and run the following command:

```bash
dotnet run --project ./hosting/PaperBoy.AppHost/PaperBoy.AppHost.csproj
```

The application will self-provision the database and other resources it needs.

## :book: Documentation

The documentation is a work in progress. I've documented some of the things I'm working on in the application.
I'm following the [Arc42](https://docs.arc42.org/home) documentation style.

- [Introduction and goals](docs/01-introduction-and-goals.md)
- [Constraints](docs/02-constraints.md)
- [Context and scope](docs/03-context-and-scope.md)
- [Solution strategy](docs/04-solution-strategy.md)
- [Building block view](docs/05-building-block-view.md)
- [Runtime view](docs/06-runtime-view.md)
- [Deployment view](docs/07-deployment-view.md)
- [Crosscutting concepts](docs/08-crosscutting-concepts.md)
- [Architecture decisions](docs/decisions/README.md)
- [Quality](docs/quality/README.md)
