# SagaPattern

## Introduction
This project provide an abstraction layer and implementations for Saga Design Pattern in microservices. 
I also wrote an article in the following address that completely cover **Saga Design Pattern**:


## Structure of soution
The solution contains one project which provide abstraction layer for saga pattern in **IActivity**, **SagaContext** and **Saga** and also it implement an example in **SampleSetup** and **Program**.

## Technology stack
- OS: Windows 10 Enterprise - 64 bits
- IDE: Visual Studio Enterprise 2022 (64 bits) - version 17.2.5
- Framework: .Net 6
- Language: C#

## How to run
The sample is a Console application and work as a Saga Orchestrator. To be able to have a better view of how Saga work in action, I provide two methods **InteractiveExecute** and **InteractiveCompensate** which let user determine the result of each action or compensation. 


