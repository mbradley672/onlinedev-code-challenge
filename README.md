# Online Course API

## Technology Stack
- C#.NET Core
- Entity Framework Core
- SQL Server

## Setup Instructions

### Prerequisites
- .NET Core SDK
- SQL Server

### Configuration
1. Clone the repository:
    ```bash
    git clone <repository_url>
    cd <repository_name>
    ```

2. Update the connection string in `appsettings.json`:
    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=<server_name>;Database=<database_name>;User Id=<user_id>;Password=<password>;"
    }
    ```

3. Apply migrations and create the database:
    ```bash
    dotnet ef database update
    ```

### Running the Application
1. Run the application:
    ```bash
    dotnet run
    ```

2. The API will be available at `https://localhost:5001/`.

### Authentication
Use the following tool to generate JWT tokens: [JWT Builder](http://jwtbuilder.jamiekurtz.com/). The `id` of `user.id` should be used to find the user who is calling the API.

### Endpoints
- `GET /lesson/{id}`: Fetch lesson details by lesson ID.
- `POST /watchLog/{lessonId}?pw={percentageWatched}`: Log the watched lesson with the percentage watched.

### Seed Data
The database is seeded with the following test data:
- One user
- One course with two sections
- Four lessons
- Two watch logs

## Testing
Use the provided Postman collection to test the API endpoints.

## Performance
Ensure the `GET /lesson/{id}` endpoint returns a response within 200 milliseconds. Test with a course of more than 20 lessons to measure the performance.
There are also tests to see the response time less than 25ms

> **_NOTE:_** These tests will only pass in production

