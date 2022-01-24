[![Continuous Integration and Deployment](https://github.com/pboychev/LeagueManager/actions/workflows/ci-cd.yaml/badge.svg)](https://github.com/pboychev/LeagueManager/actions/workflows/ci-cd.yaml)


# LeagueManager
### Presiyan Boychev's final project for Telerik Academy Upskill DevOps course 2021/2022.

The idea behind this project is to create continuous integration and continuous delivery of an ASP.NET core application. The CI/CD process contains couple of steps to ensure the app is distributed the right way. 

The applications itself is an ASP.NET core application which makes requests to a public API to gather some data and format it in a table. 

<hr />

## Content

1. Used technologies and tools
2. Structure of the project
3. CI/CD
4. Future improvements
5. Credits

<hr />

## 1. Used technologies and tools

- ASP.NET core - used for creating the project 
- GitHub - used for source control
- GitHub Actions - used for the CI/CD
- SonarCloud - used for SAST
- Snyk - used for SAST
- Azure AKS - used as kubernetes cluster solution
- Docker Desktop - used for creating Docker image and testing on a local environment
- DockerHub - used as a repository for the Docker images

<hr />

## 2. Structure of the project

The application is stored inside LeagueManager folder. We have some UNIT tests inside LeagueManager.Tests folder. Inside aks folder we can find deploy.json and deployparams.json. deploy.json is an ARM template file for deploying the Azure AKS, deployparams.json is a file with custom parameters for the cloud, for example clusterName, dnsPrefix. Inside LeagueManager folder, there's another folder called k8s. In it we have 2 manifests for deploying the Docker image and a LoadBalancer service to expose a specific port, so we can access the application. We have the MVC structure in place with all the folders for Models, Views and Controllers. Inside wwwroot we have some styling and script files.

<hr />

## 3. CI/CD

Let's first review the following diagram and go trough each step: 


![Diagram](/assets/images/CI-CD.png)

The GitHub Actions workflow has 7 major steps - Pre-build information, SonaCloud Scan, Building the WebApp and run tests, Building and upload Docker image to DockerHub, Create AKS, Deploy Docker image, Build notification status. Upon the successful completion of each step a message is sent to a Slack channel.

Pre-build information step:
- Provide information how this pipeline was tigered
- Provide branch and repository information

SonaCloud Scan step:
- Build and analyze the project
 
Building the WebApp and run tests step:
- Restore dependencies
- Build the project
- Run UNIT tests
  
Building and upload Docker image to DockerHub step:
- Restore dependencies
- Build the project
- Build the docker image
- Login to DockerHub
- Tag the Docker image
- Push the Docker image to DockerHub
   
Create AKS step:
- Login to Azure
- Deploy the AKS using deploy.json and deployparams.json inside aks folder
- Setup kubectl

Deploy Docker image step:
- Set context
- Create a secret in Kubernetes cluster
- Deploy Docker image to Kubernetes cluster
     
Build notification status step: 
- Send notification to Slack channel to notify the build result

<hr />

## 4. Future improvements

- Fix vulnerabilities
- Think about a solution for storing results of the requests (maybe Redis Cache)
- Create more detailed notifications in Slack
- Implement new application functionalities
- Think about a solution for managing the Kubernetes cluster

<hr />

## 5. Credits

I would like to thank my brother (Simeon Boychev) for helping me with the application development. 

I would like to thank Andrey Sotirov @andreysotirov for refactoring the code. 



