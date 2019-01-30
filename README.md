# Lambda Workshop
This repository contains a simple project to describe the use of AWS lambda in C# using an Oracle Database.

## Requirements

* AWS CLI already configured with at least PowerUser permission
* [Docker installed](https://www.docker.com/community-edition)
* [SAM Local installed](https://github.com/awslabs/aws-sam-cli)
* [DotNet Core installed](https://www.microsoft.com/net/download)
* [Microsoft Visual Studio installed](https://visualstudio.microsoft.com/downloads/)
* Coolblue nuget url/s configured in Visual Studio


# How to test with docker: 
Open powershell and type the following command: 
```
./example.ps1
```

The script will provide to: 
* Create a docker network.
* Retrieve and spin up an Oracle instance in a docker container
* Build the C# project and run the tests (if any present)
* Wait until Oracle is ready
* Invoke AWS sam to run the lambda function that will insert a sample of data into a database table.
