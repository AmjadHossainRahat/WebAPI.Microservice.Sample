# Microservice Sample Project

This project is a microservice sample built with **.NET 6**, showcasing various best practices, patterns, and technologies for developing scalable and maintainable applications.

## Technologies Used

- .NET 6
- ASP.NET Web API
- XUnit (for unit testing)
- Redis (for API-level caching)
- Docker (for containerization)

## Project Overview

The microservice sample project demonstrates the implementation of the following:

- **CQRS Pattern**: Command Query Responsibility Segregation (CQRS) is used to separate read and write operations. This allows for better scalability, performance, and separation of concerns.
- **Mediator Pattern**: The Mediator pattern is employed to decouple communication between components, enabling loose coupling and better testability.
- **Repository Pattern**: The Repository pattern is used to abstract data access operations, providing a consistent and maintainable way to interact with the underlying data store.
- **SOLID Principles**: The project adheres to SOLID principles, promoting modular, maintainable, and extensible code.
- **API-level Caching with Redis**: Redis is utilized for caching frequently accessed API responses, reducing response times and improving performance.
- **Containerization**: The application is containerized using Docker, allowing for easy deployment and scalability.

## Getting Started

### Prerequisites

- .NET 6 SDK
- Docker (optional for containerization)
- Docker Compose (optional for containerization)

### Installation

1. Clone the repository:

   ```shell
   git clone https://github.com/AmjadHossainRahat/WebAPI.Microservice.Sample.git
   ```
2. Navigate to the project directory:
	```shell
	cd WebAPI.Microservice.Sample
	```
3. Restore dependencies and build the project:
	```shell
	dotnet build
	```

### Running the Application
1. Run the application locally:

	```shell
	dotnet run --project src\Web
	```
2. The application will start and be accessible at http://localhost:5232
3. API documentation is accessible at http://localhost:5232/swagger

### Running Unit Tests
To run the unit tests using XUnit, use the following command:
```shell
dotnet test
```

### Containerization
The project can be containerized using Docker for easy deployment. To build a Docker image, use the following command:
```shell
docker build -t microservice-sample .
```

To run a container from the Docker image, use the following command:
```shell
docker run -d -p 5000:80 microservice-sample
```
The application will be accessible at http://localhost:5000.

OR

you can simply run the following command if you have docker compose installed

```shell
docker-compose up
```

## Contributing
Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

## License
This project is licensed under the [MIT License](https://www.mit.edu/~amini/LICENSE.md).