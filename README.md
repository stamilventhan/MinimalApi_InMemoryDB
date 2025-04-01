**Setup install below packages using Package Manager console:**

1. Install-Package Microsoft.AspNetCore.OpenApi -Version 8.0.14
2. Install-Package Microsoft.EntityFrameworkCore -Version 9.0.3
3. Install-Package Microsoft.EntityFrameworkCore.InMemory -Version 9.0.3

--> Run the local Docker Desktop

**Enter below commands in Developer powershell:**
1.  docker build -t minimalapi-inmemory .
2.  docker run -it -p 8080:8080 --rm --name minimalapi-container minimalapi-inmemory
