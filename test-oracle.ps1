Add-Type -Path "utils\Oracle.ManagedDataAccess.dll"
$disconnected = 1;
while ($disconnected) {
	try {
		$conn= New-Object Oracle.ManagedDataAccess.Client.OracleConnection("User Id=WORKSHOPUSR;Password=development;Data Source=localhost")
		$conn.Open()
		$conn.Close()
		$disconnected = 0;
		Write-Host "Oracle connected!"
	}
	catch {
	   Write-Host "Oracle not ready yet"
	}
}