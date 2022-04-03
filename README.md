# WitnessReportsFBI
REST API for witness reports on FBI cases

## Set up the project:

### 1. Restore database 
- Take a .bak file from DB folder and restore it in database management system

### 2. Steps to configure appSettings.json:
- Set up connection string and place it under **DefaultConnection**
- Set up ipify params([ipify](https://geo.ipify.org/) is service for IP Address validation - ), add following under **ipifyApiParams** section:
   - apiUrl
   - apiKey (used for authentication on ipify API)
   - testIpAddress (used when running project on local machine)
- Set up [FBI API](https://www.fbi.gov/wanted/api) params:
   - apiUrl



        
