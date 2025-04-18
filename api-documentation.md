Base URL:
http://localhost:8080/api/EmployeeApi/

***************************************************
Endpoints
Create Employee
	POST /employee/create

	Creates a new employee using JSON payload.

	Request Body (JSON)
	{
	  "payroll_Number": "string",
	  "forenames": "string",
	  "surname": "string",
	  "date_of_Birth": "yyyy-mm-dd",
	  "telephone": "string",
	  "mobile": "string",
	  "address": "string",
	  "address_2": "string",
	  "postcode": "string",
	  "eMail_Home": "string",
	  "start_Date": "yyyy-mm-dd"
	}
	Response
	200 OK – Employee created successfully.

Create Employees via CSV Upload
	POST /employee/create/csv

	Uploads a CSV file containing multiple employee records.

	Request (multipart/form-data)
	CsvFile (binary, required): The CSV file containing employee data.

	Response
	200 OK – File processed successfully.

Update Employee
	POST /employee/update

	Updates an existing employee record.

	Request Body (JSON)
	{
	  "id": "uuid",
	  "payroll_Number": "string",
	  "forenames": "string",
	  "surname": "string",
	  "date_of_Birth": "yyyy-mm-dd",
	  "telephone": "string",
	  "mobile": "string",
	  "address": "string",
	  "address_2": "string",
	  "postcode": "string",
	  "eMail_Home": "string",
	  "start_Date": "yyyy-mm-dd"
	}
	Response
	200 OK – Employee updated successfully.

Delete Employee
	POST /employee/delete/{id}

	Deletes an employee by ID.

	Parameters
	id (UUID, required): The ID of the employee to delete.

	Response
	200 OK – Employee deleted successfully.

Get Employee by ID
	GET /employee/{id}

	Fetches a single employee by ID.

	Parameters
	id (UUID, required): The ID of the employee to fetch.

	Response
	200 OK – Returns employee data.

Get Employee List
	GET /employee-list

	Returns a paginated list of employees.

	Query Parameters
	Filter.SearchingText (string): Search text for filtering.

	Filter.Ascending (boolean): Sort order.

	PaginationParams.PageIndex (integer): Page index.

	PaginationParams.PageSize (integer): Page size.

	Response
	200 OK – Returns list of employees.