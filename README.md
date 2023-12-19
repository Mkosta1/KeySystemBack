

## Key system

Run the application as http or https version. When running MVC controller page will open, it can be closed because frontend is using that localhost address.
To add database to docker, run docker-compose.yml file. Access database, then add the connection to DBBeaver or other db management system.
Want to add new rules to database then add them to ApplicationDbContext.cs.



# Code below is to generator or update migration, create new API controllers whenever new table is added to db. When adding new tables, check if its added to mapping, DAL layer and has its repository.
# Generate db migration

~~~bash
# install or update
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

# create migration
dotnet ef migrations add Initial --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext 
dotnet ef migrations add Token --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext 

# apply migrationdotnet ef migrations add Initial --project DAL.EF.APP --startup-project SportSchool --context ApplicationDbContext

dotnet ef database update --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext 
~~~


# generate rest controllers

Add nuget packages
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Microsoft.EntityFrameworkCore.SqlServer
- 
~~~bash
# install tooling
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool update --global dotnet-aspnet-codegenerator

cd WebApp

# Rest API
dotnet aspnet-codegenerator controller -m Domain.App.Key -name KeyController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
dotnet aspnet-codegenerator controller -m Domain.App.Site -name SiteController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
dotnet aspnet-codegenerator controller -m Domain.App.Worker -name WorkerController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
dotnet aspnet-codegenerator controller -m Domain.App.WorkerAtSite -name WorkerAtSiteController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
~~~


Generate Identity UI

~~~bash
cd WebApp
dotnet aspnet-codegenerator identity -dc DAL.EF.App.ApplicationDbContext --userClass AppUser -f 
~~~
