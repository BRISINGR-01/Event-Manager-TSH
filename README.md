# TSH - Event manager

## About the project
This is my assignment for the second semester at [Fontys University of Applied Sciences](https://fontys.edu/).

During my stay at [The Social Hub](https://www.thesocialhub.co/) I helped with the organization of events and participated in them regularly.
This allowed me to see how they were created, managed and promoted. The organizer used a couple of platforms and this caused problems, which I expirienced first hand. That inspired me to create this managment tool which would combine all of the features and more at one place and would have a more user-friendly design.

## Run
You need to setup a local mysql database and create tables accordingly to the db.sql. Then put the connection string in the app.config in the Web and Desktop solutions (whichever you want to run). The connection string must contain the following: "OldGuids=True;charset=utf8;"
ex:
`<?xml version='1.0' encoding='utf-8'?>
<configuration>
	<connectionStrings>
		<clear />
		<add name="Local"
		 providerName="System.Data.ProviderName"
		 connectionString="<connection details>;OldGuids=True;charset=utf8;"/>
		<add name="Online"
		providerName="System.Data.ProviderName"
		connectionString="<connection details>;OldGuids=True;charset=utf8"/>
	</connectionStrings>
</configuration>`

The following nuget packages in the specified solutions are required:
MySql.Data - Infrastructure
SixLabors.ImageSharp - Infrastructure
bootstrap - Web
