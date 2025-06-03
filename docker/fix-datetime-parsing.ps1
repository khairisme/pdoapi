$filePath = "..\src\Services\HR.ExternalAPI\Controllers\EmployeesController.cs"
$content = Get-Content $filePath -Raw

# Replace DateTime.Parse with null-safe version in GetEmployee method (around line 120-121)
$content = $content -replace 'DateOfBirth = DateTime\.Parse\(response\.Employee\.DateOfBirth\),', 'DateOfBirth = !string.IsNullOrEmpty(response.Employee.DateOfBirth) ? DateTime.Parse(response.Employee.DateOfBirth) : null,'
$content = $content -replace 'JoiningDate = DateTime\.Parse\(response\.Employee\.JoiningDate\),', 'JoiningDate = !string.IsNullOrEmpty(response.Employee.JoiningDate) ? DateTime.Parse(response.Employee.JoiningDate) : null,'

# Replace DateTime.Parse with null-safe version in CreateEmployee method (around line 180-181)
$content = $content -replace 'DateOfBirth = DateTime\.Parse\(response\.Employee\.DateOfBirth\),', 'DateOfBirth = !string.IsNullOrEmpty(response.Employee.DateOfBirth) ? DateTime.Parse(response.Employee.DateOfBirth) : null,'
$content = $content -replace 'JoiningDate = DateTime\.Parse\(response\.Employee\.JoiningDate\),', 'JoiningDate = !string.IsNullOrEmpty(response.Employee.JoiningDate) ? DateTime.Parse(response.Employee.JoiningDate) : null,'

# Replace DateTime.Parse with null-safe version in UpdateEmployee method (around line 237-238)
$content = $content -replace 'DateOfBirth = DateTime\.Parse\(response\.Employee\.DateOfBirth\),', 'DateOfBirth = !string.IsNullOrEmpty(response.Employee.DateOfBirth) ? DateTime.Parse(response.Employee.DateOfBirth) : null,'
$content = $content -replace 'JoiningDate = DateTime\.Parse\(response\.Employee\.JoiningDate\),', 'JoiningDate = !string.IsNullOrEmpty(response.Employee.JoiningDate) ? DateTime.Parse(response.Employee.JoiningDate) : null,'

# Also fix the ToString in CreateEmployee and UpdateEmployee methods to handle null dates
$content = $content -replace 'DateOfBirth = employeeDto\.DateOfBirth\.ToString\("o"\),', 'DateOfBirth = employeeDto.DateOfBirth?.ToString("o") ?? string.Empty,'
$content = $content -replace 'JoiningDate = employeeDto\.JoiningDate\.ToString\("o"\),', 'JoiningDate = employeeDto.JoiningDate?.ToString("o") ?? string.Empty,'

# Save the changes back to the file
Set-Content -Path $filePath -Value $content

Write-Host "DateTime parsing fixes applied to $filePath"
