# Credit Solution
A Simple Microservices Example

![alt text](https://github.com/MesutAtasoy/CreditSolution/blob/master/images/overview.png)

The application evaluates the credit application based on the credit score and monthly income. After evaluation, sends a informational message to the user as a result of the evaluation.

## Project Overview
  - Gatewap API : It's responsible for routing requests to microservices
  - Credit Score API : It's responsible for user's credit score. All user credit scores are stored by it.
  - Credit API : It's responsible for evaluating credit requests. It depends on Credit Score API. It gets user's credit score from Credit Score API
  - SMS API : It's responsible for sending messages. It consumes published messages for sending message and stores message logs. 
  - Service Discovery : It's responsible for health check of microservices.
  

- Front End Side : It's coded by Angular.

## Tech Stack
 All service projects are written by .Net Core 3.1 framework. Front end project is written by Angular. Some design patterns 
 are implemented such as Decorator pattern, Repository pattern, Command Query Responsibility Segregation. 
  - Database : PostgreSQL
  - ORM : EntityFramework Core 3.1
  - Message Queue : RabbitMQ
  - Cache : Redis
  - Service Discovery : Consul 
  - Gateway : Ocelot

## Project Setup 
1. Clone the repository 
`git clone https://github.com/MesutAtasoy/CreditSolution.git` 

2. Create Docker Network
`docker network create credit-network` 

3. Build the containers
`docker-compose build` 

4. Run the containers
`docker-compose up -d` 
