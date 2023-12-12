Before starting the project, run the following commands in root directory:

```bash
sqllocaldb create mssqllocaldb
dotnet ef database update --project .\Exchanger.Infrastructure  --startup-project .\Exchanger
```

The Postman collection can be found in the 'solution-items' folder.