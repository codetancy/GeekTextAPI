# Getting Started

***Note:** Prior to cloning the repository make sure to have Docker, Git, the 
.NET SDK, and the IDE or text editor of your choice installed in your machine. 
For a detailed guide on how to set up your development environment, read the
[Requirements](REQUIREMENTS.md).*


## Cloning the Repository 

To clone the repository in your machine open a new terminal instance and run
the following command:

```
$ git clone https://github.com/codetancy/GeekTextAPI.git
```

## Openning the Project 

### Visual Studio

1. Start Visual Studio.
2. Go to `File/Open` and click `Project/Solution`.
3. In the file explorer, navigate to the project's root directory and open the 
   `.sln` file.

### JetBrains Rider

1. Start JetBrains Rider.
2. In the startup window, click `Open`.
3. In the file explorer, navigate to the project's root directory and open the 
   `.sln` file.

### Visual Studio Code

***Note:** If using Visual Studio Code, it is highly recommended to have the 
C#, Docker, and GitLens extensions installed.*

1. Start Visual Studio Code.
2. Go to `File/Open Folder`.
3. In the file explorer, navigate to and open the project's root directory.
4. Open the Command Palette (`ctrl+shift+p`), search for 
   `OmniSharp: Select Project` and select it.
5. In the prompt select the `.sln` file.

## Running the Container

***Note:** Keep in mind that most IDE's and text editors have a built in 
terminal from which you can run commands.*

The `docker-compose.yml` file in the project's root directory contains 
the instructions to build and run a network, which is a group of containers 
that run together. Currently, our network only contains the 
[SQL Server image](https://hub.docker.com/_/microsoft-mssql-server); however, 
in the future we will containerize our API and run it as part of the network.

To run the network, enter the command `docker-compose up -d` in the same 
directory of the docker-compose file. You can verify it is running, by entering 
the command `docker ps`, which should list all the running containers. Finally, 
you can stop the network with `docker-compose stop`.

For more information on Docker commands, refer to the 
[Docker CLI documentation](https://docs.docker.com/engine/reference/commandline/cli/).

## Running the API

### Visual Studio and JetBrains Rider

Running the application in your IDE also starts the API. This will open a new 
web browser instance pointing to Swagger's endpoint. Although the use of Swagger
is optional, it is highly recommended. Finally, to stop the API from running, 
simply close the web browser instance.

### Visual Studio Code

In a new terminal instance, execute the `dotnet run` command pointing to the 
`.csproj` file of the project we wish to run. In our case, it is the Web 
project. Hence, assuming you are in the solution's root directory, the command 
should read as follows:

```
dotnet run --project .\src\Web\Web.csproj
```
To stop the API, press `ctrl+c` in the same terminal instance used to execute 
the `dotnet run` command.

 ## Reaching the API

You can reach the API with any HTTP client. Some common clients are 
[Postman](https://www.postman.com/) and [Insomnia](https://insomnia.rest/). 
Nevertheless, we highly encourage the use of Swagger. To use Swagger, navigate 
to `https://localhost:5001/swagger` in your web browser.