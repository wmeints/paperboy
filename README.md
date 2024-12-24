# PaperBoy - State of the Art Blog Engine

This is the next iteration of my personal blog engine. Feel free to copy this and modify
it to your personal needs.

## Features

- Draft a new blog post based on notes and a topic using generative AI.
- Iterate and review content by chatting to the review agent.

## System requirements

- [Node 22 or higher](https://nodejs.org/en)
- Access to Azure OpenAI

## Getting started

The steps in this section help you set up the blog engine on your local machine.
Please check out the documentation for deployment instructions.

### Setting up the repository

Clone the repository to disk and run the following commands in the root of the repository:

```bash
npm install
```

### Configuring the environment

After cloning the repository, execute the following command to set a secret for
the local environment:

```bash
npx auth secret
```

When the secret is generated, modify the `.env.local` file to include the database URL
that points to a local postgres database server:

```plaintext
DATABASE_URL=postgresql://postgres:postgres@localhost:5432/paperboy
AUTH_GITHUB_ID=<your github client id>
AUTH_GITHUB_SECRET=<your github oauth secret>
```

**Note:** Make sure to set up [a Github OAuth application](https://github.com/settings/developers) 
to obtain the information needed to login.

Finally, run the following commands to start a new instance of postgres in
docker and migrate the database:

```bash
docker compose up -d
npm run db:migrate
```

### Running the application

Now that you have everything configured, run the application using the following command:

```bash
npm run dev
```

## Documentation

TODO: Describe how this application was designed.