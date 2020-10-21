# Credit Solution
A Simple Microservices Example ![alt text](https://github.com/MesutAtasoy/CreditSolution/workflows/Docker%20Compose%20CI/badge.svg)

![alt text](https://github.com/MesutAtasoy/CreditSolution/blob/master/images/overview.png)

The application evaluates the credit application based on the credit score and monthly income. After evaluation, sends a informational message to the user as a result of the evaluation.

## Project Overview
  - Gatewap API : It's responsible for routing requests to microservices. (http://localhost:6010)
  - Credit Score API : It's responsible for user's credit score. All user credit scores are stored by it. (http://localhost:6002)
  - Credit API : It's responsible for evaluating credit requests. It depends on Credit Score API. It gets user's credit score from Credit Score API (http://localhost:6001)
  - SMS API : It's responsible for sending messages. It consumes published messages for sending message and stores message logs. (http://localhost:6003)
  - Service Discovery : It's responsible for health check of microservices. (http://localhost:8500)
  

- Front End Side : It's coded by Angular.

## Tech Stack
 All service projects are written with .Net Core 3.1 framework. Front end project (https://github.com/MesutAtasoy/CreditSolutionUI) with written by Angular. Some design patterns are implemented such as Decorator pattern, Repository pattern, Command Query Responsibility Segregation. An Unit test is added for Credit API project. It tests evaluation of credit with parameters.
 
  - Database : PostgreSQL
  - ORM : EntityFramework Core 3.1
  - Message Queue : RabbitMQ
  - Cache : Redis
  - Service Discovery : Consul 
  - Gateway : Ocelot
  - Documentation : Swagger

## Project Setup 
1. Clone the repository 
`git clone https://github.com/MesutAtasoy/CreditSolution.git` 

2. Create Docker Network
`docker network create credit-network` 

3. Build the containers
`docker-compose build` 

4. Run the containers
`docker-compose up -d` 

## API Documentations
  - Credit Score API : http://localhost:6002/swagger/index.html
  - Credit API : http://localhost:6001/swagger/index.html

Navigate to http://localhost:8500/ to ensure the containers are healthly using Consul (Service Discovery)


