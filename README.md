## Setup
* Ensure you have Docker Desktop installed and it is running in Windows mode
* Save the Sitecore license to c:\license\licence.xml
* Run /build/dev/Installer.ps1 from a powershell terminal
* Navigate to /build/dev/ and run docker-compose up
* Run  docker exec liontrustcm ipconfig to get the ip for the CM server
* Run  docker exec liontrustcd ipconfig to get the ip for the CM server