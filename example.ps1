#> CLEANUP 
	Write-Host "Clean up environment" -ForegroundColor yellow -BackgroundColor black
	Write-Host "Kill oracle service"
	docker kill workshop-oracle
	Write-Host "remove network"
	docker network rm "workshop-network"
#< CLEANUP

##-----------------------------------------------------------------------------------------------------------------------------------------------

#> DOCKER NETWORK
	Write-Host "Create docker common network (workshop-network)" -ForegroundColor yellow -BackgroundColor black
	docker network create workshop-network
	sleep 1
#< DOCKER NETWORK

##-----------------------------------------------------------------------------------------------------------------------------------------------


#> BUILD AND RUN ORACLE
	Write-Host "Oracle database - build and run..." -ForegroundColor yellow -BackgroundColor black
	docker-compose up --force-recreate --build -d
#< BUILD AND RUN ORACLE

##-----------------------------------------------------------------------------------------------------------------------------------------------

#> BUILD .NET SOLUTION
	Write-Host "Build solution" -ForegroundColor yellow -BackgroundColor black
    .\build.ps1
#< BUILD .NET SOLUTION	
	
##-----------------------------------------------------------------------------------------------------------------------------------------------
	
#> WAIT FOR ORACLE
	Write-Host "Wait for oracle to spin up " -nonewline
    
	Add-Type -Path "utils\Oracle.ManagedDataAccess.dll"
	$disconnected = 1;
	while ($disconnected) {
		try {
			$conn= New-Object Oracle.ManagedDataAccess.Client.OracleConnection("User Id=WORKSHOPUSR;Password=development;Data Source=localhost")
			$conn.Open()
			$conn.Close()
			$disconnected = 0;
		}
		catch {
		   Sleep 2
		   Write-Host "." -nonewline
		}
	}
	
	Write-Host " done" -ForegroundColor darkgreen
	
#< WAIT FOR ORACLE

##-----------------------------------------------------------------------------------------------------------------------------------------------


#> INVOKE LAMBDA WITH SAM
Write-Host "Call lambda function" -ForegroundColor yellow -BackgroundColor black
sam local invoke HelloLambdaFunction --template cloudformation.yaml --event requestSamples/insert.json --docker-network workshop-network
#< INVOKE LAMBDA WITH SAM




