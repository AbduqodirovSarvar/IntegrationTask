***************************************************************
Project Structure
	Domain
	Contains the core business logic, entities, and interfaces. It is independent of any other layer.

	Application
	Defines use cases (commands/queries), service contracts, and business rules. It depends only on the Domain layer.

	Infrastructure
	Contains implementations of external concerns such as database access, file storage, etc. It implements interfaces from the Application and Domain layers.

	Web
	The entry point of the application. It’s an ASP.NET Core project that handles HTTP requests and depends on Application and Infrastructure.

	Tests
Includes unit and integration tests for Application, Domain, Infrastructure, and overall system behavior.

****************************************************************
Benefits
	Clear separation of concerns
	Testable business logic
	Decoupled infrastructure
	Easier to extend and maintain