# Custom Agent Context: MySampleAgentApp
- Application Framework: .NET Web API
- When resolving Jira issues or user requests:
  1. Follow standard C# and .NET coding conventions.
  2. Keep changes scoped strictly to the ticket requirements.
  3. Ensure new endpoints maintain proper REST structure and return JSON responses.
  4. Always write clean, maintainable code with clear comments.

# Repository Instructions for Copilot Agent

## Architecture
- This is a .NET 8 Web API project.
- Data models reside in files named `*Model.cs` or `Program.cs`.

## Coding Rules
- When asked to add properties to models, update any relevant request/response payloads and endpoints accordingly.
- Ensure all builds pass using `dotnet build`.