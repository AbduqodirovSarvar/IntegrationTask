Prerequisites
	Option 1:
		.NET SDK 8.0+ (recommended 9.0)
		[SQL Server / PostgreSQL] (if using a database)
		Git
	Option 2:
		Git
		Docker

Setup
	Option 1:
		git clone https://github.com/AbduqodirovSarvar/IntegrationTask.git
		cd IntegrationTask
		set your database configurations in Web/appsettings.json
		dotnet restore
		dotnet run --project Web

		for web page
		http://localhost:5131/

		for web api documentation page (swagger)
		http://localhost:5131/swagger/index.html

	Option 2:
		git clone https://github.com/AbduqodirovSarvar/IntegrationTask.git
		cd IntegrationTask
		Set your database configurations in .env file
		docker-compose build
		docker-compose up

		for web page
		http://localhost:8080/

		for web api documentation page (swagger)
		http://localhost:8080/swagger/index.html


