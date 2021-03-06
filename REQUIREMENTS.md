# Requirements

## .NET SDK

Before we can build and run .NET apps, we need to 
[download the .NET SDK](https://dotnet.microsoft.com/download) from Microsoft's 
website. For this project we are using .NET 5.0, so make sure to select the 
correct installer for your specific operating system. 

Once the download completes, run the installer and follow the steps. 
When done, open a new command prompt and run the `dotnet --version` command, 
this should display the installed version.

## Pick an editor

There are many options for writing .NET applications. However, I am listing the 
three most common:

### Visual Studio

Perhaps the most popular IDE and easiest way to get started. Download the 
community version for free from Microsoft's [website](https://visualstudio.microsoft.com/). 

To install, please follow Microsoft's [step by step instructions](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2019). 
For our use case, select the following workloads: 
- ASPNET and web development.
- .NET desktop development.

### Visual Studio Code

Free, lightweight, and extensible. VS Code is becoming more common among .NET 
developers. You would use VS Code in conjuction with the .NET CLI for creating 
projects, adding references, and managing packages. 

### Jetbrains Rider

Personally my favorite option. Rider is a crossplatform IDE created by Jetbrains. 
Although most Jetbrains product are paid, you can apply for a free 
[educational license](https://www.jetbrains.com/community/education/#students) 
by signing up with your student email.

## Docker

Docker is a tool that allow developers to package applications into **images** 
and run them as **containers** on any platform that can run Docker. 

For this project, we will run SQL Server (and later our API) as a container, 
since it is crossplatform and easy to set up and deploy. For that purpose, you 
will first need to install [Docker desktop](https://docs.docker.com/get-docker/) 
in your machine.

You can verify Docker was installed by running the command `docker` in your 
terminal, which should display a list of optional parameters.